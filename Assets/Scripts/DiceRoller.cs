using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 주사위를 소환하고 중앙으로 이동시키는 클래스
/// </summary>
public class DiceRoller : MonoBehaviour
{
    public GameManager gameManager;

    [Header("주사위 프리팹")]
    public GameObject[] dicePrefabs;  // Inspector에서 할당할 주사위 프리팹들 (dice_1 ~ dice_6)
    
    [Header("소환 설정")]
    public int diceCount = 5;  // 소환할 주사위 개수
    public Vector3 spawnPosition = new Vector3(0, 0, 0);  // 주사위가 생성될 위치 (화면 하단)
    
    [Header("이동 설정")]
    public Vector3 centerPosition = Vector3.zero;  // 주사위가 이동할 목표 위치 (화면 중앙)
    public float moveSpeed = 5f;  // 이동 속도
    public float spacing = 1.5f;  // 주사위 간 간격
    
    [Header("회전 설정")]
    public float rotationSpeed = 1080f;  // 회전 속도 (초당 각도)
    
    [Header("생성된 주사위")]
    public List<GameObject> spawnedDice = new List<GameObject>();  // 소환된 주사위들
    
    void Start()
    {
        // 테스트: 시작하자마자 주사위 소환
        // SpawnDice();
    }
    
    void Update()
    {
        // 테스트용: R 키를 누르면 주사위 소환
        if (Input.GetKeyDown(KeyCode.R) && gameManager.rollsRemaining >= 3)
        {
            SpawnDice();
        }
        else if (Input.GetKeyDown(KeyCode.R) && 0 < gameManager.rollsRemaining && gameManager.rollsRemaining < 3)
        { 
            RerollDice();
        }
    }
    
    /// <summary>
    /// 주사위를 소환하고 중앙으로 이동시킵니다
    /// </summary>
    public void SpawnDice()
    {
        StartCoroutine(SpawnDiceSequence());
    }
    
    /// <summary>
    /// 주사위를 순차적으로 소환하고 이동시킵니다 (0.3초 간격)
    /// </summary>
    IEnumerator SpawnDiceSequence()
    {
        // 기존 주사위가 있으면 제거
        ClearDice();
        
        // 프리팹 배열이 비어있는지 확인
        if (dicePrefabs == null || dicePrefabs.Length == 0)
        {
            Debug.LogError("주사위 프리팹이 할당되지 않았습니다!");
            yield break;
        }
        
        // 주사위 5개를 0.3초 간격으로 생성 및 이동
        for (int i = 0; i < diceCount; i++)
        {
            // 랜덤하게 주사위 프리팹 선택
            int randomIndex = Random.Range(0, dicePrefabs.Length);
            GameObject selectedPrefab = dicePrefabs[randomIndex];
            
            // 주사위 생성
            GameObject dice = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            dice.name = $"Dice_{i + 1} (Type: dice_{randomIndex + 1})";
            
            // 생성된 주사위를 리스트에 추가
            spawnedDice.Add(dice);
            
            // 목표 위치 계산 (중앙에서 일렬로 배치)
            Vector3 targetPosition = CalculateTargetPosition(i);
            
            // 주사위를 목표 위치로 이동시키기 (코루틴 사용)
            StartCoroutine(MoveDiceToTarget(dice, targetPosition));
            
            // 0.3초 대기 후 다음 주사위 생성
            yield return new WaitForSeconds(0.3f);
        }
        
        Debug.Log($"{diceCount}개의 주사위를 소환했습니다!");
    }
    
    /// <summary>
    /// 주사위의 목표 위치를 계산합니다 (중앙에 일렬로 배치)
    /// </summary>
    Vector3 CalculateTargetPosition(int index)
    {
        // 전체 너비 계산
        float totalWidth = (diceCount - 1) * spacing;
        
        // 시작 위치 (왼쪽 끝)
        float startX = centerPosition.x - (totalWidth / 2f);
        
        // 각 주사위의 X 위치
        float xPosition = startX + (index * spacing);
        
        return new Vector3(xPosition, centerPosition.y, centerPosition.z);
    }
    
    /// <summary>
    /// 주사위를 목표 위치로 이동시킵니다 (Z축 기준 회전하면서 부드러운 이동)
    /// </summary>
    IEnumerator MoveDiceToTarget(GameObject dice, Vector3 targetPosition)
    {
        if (dice == null) yield break;
        
        Vector3 startPosition = dice.transform.position;
        float elapsedTime = 0f;
        float duration = 1f / moveSpeed;  // 이동 시간
        
        // 이동하면서 회전
        while (elapsedTime < duration)
        {
            if (dice == null) yield break;
            
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            
            // 부드러운 이동 (Lerp)
            dice.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            
            // Z축 기준으로 회전
            float rotationAmount = rotationSpeed * Time.deltaTime;
            dice.transform.Rotate(0, 0, rotationAmount);
            
            yield return null;
        }
        
        // 최종 위치 정확히 맞추기
        if (dice != null)
        {
            dice.transform.position = targetPosition;
            
            // Z rotation을 0으로 정렬
            yield return StartCoroutine(AlignRotationToZero(dice));
        }
    }
    
    /// <summary>
    /// 주사위의 Z rotation을 0으로 정렬합니다 (같은 방향으로 계속 회전)
    /// </summary>
    IEnumerator AlignRotationToZero(GameObject dice)
    {
        if (dice == null) yield break;
        
        // 현재 회전값
        float currentZRotation = dice.transform.eulerAngles.z;
        
        // 0도에 가까운지 확인 (허용 오차 1도)
        if (Mathf.Abs(currentZRotation) < 1f || Mathf.Abs(currentZRotation - 360f) < 1f)
        {
            // 이미 0도에 가까우면 정확히 0으로 설정
            Vector3 finalRotation = dice.transform.eulerAngles;
            finalRotation.z = 0;
            dice.transform.eulerAngles = finalRotation;
            yield break;
        }
        
        // 같은 방향으로 계속 회전하면서 0도에 도달할 때까지
        float alignRotationSpeed = rotationSpeed * 1f;  // 정렬 시 회전 속도 (원래의 50%)
        
        while (true)
        {
            if (dice == null) yield break;
            
            // 같은 방향으로 회전 (양수 방향)
            float rotationAmount = alignRotationSpeed * Time.deltaTime;
            dice.transform.Rotate(0, 0, rotationAmount);
            
            // 현재 Z rotation 확인
            currentZRotation = dice.transform.eulerAngles.z;
            
            // 0도에 충분히 가까워지면 정확히 0으로 설정하고 종료
            if (currentZRotation < 10f || currentZRotation > 350f)
            {
                Vector3 finalRotation = dice.transform.eulerAngles;
                finalRotation.z = 0;
                dice.transform.eulerAngles = finalRotation;
                break;
            }
            
            yield return null;
        }
    }
    
    /// <summary>
    /// 소환된 주사위들을 모두 제거합니다
    /// </summary>
    public void ClearDice()
    {
        foreach (GameObject dice in spawnedDice)
        {
            if (dice != null)
            {
                Destroy(dice);
            }
        }
        
        spawnedDice.Clear();
        Debug.Log("주사위를 모두 제거했습니다.");
    }
    
    public void ClearUnlockedDice()
    {
        List<GameObject> unlockedDice = new List<GameObject>();
        foreach (GameObject dice in spawnedDice)
        {
            if (dice != null)
            {
                DiceController controller = dice.GetComponent<DiceController>();
            }
        }
        foreach (GameObject dice in unlockedDice)
        {
            if (dice != null)
            {
                Destroy(dice);
            }
        }
    }
    /// <summary>
    /// 주사위를 다시 굴립니다 (제거 후 재생성)
    /// </summary>
    public void RerollDice()
    {
        StartCoroutine(RerollSequence());
    }
    
    /// <summary>
    /// 재굴림 시퀀스 (주사위 제거 → 소환)
    /// </summary>
    IEnumerator RerollSequence()
{
    // 잠긴 주사위 개수 세기
    int lockedCount = 0;
    foreach (GameObject dice in spawnedDice)
    {
        if (dice != null)
        {
            DiceController controller = dice.GetComponent<DiceController>();
            if (controller != null && controller.IsLocked())
            {
                lockedCount++;
            }
        }
    }
    
    // 생성할 주사위 개수 = 전체 - 잠긴 개수
    int spawnCount = diceCount - lockedCount;
    
    Debug.Log($"잠긴 주사위: {lockedCount}개, 새로 생성할 주사위: {spawnCount}개");
    
    // 잠기지 않은 주사위만 제거 (여기가 핵심!)
    ClearUnlockedDice();  // ← ClearDice()가 아니라 ClearUnlockedDice()!
    
    yield return new WaitForSeconds(0.3f);
    
    // 부족한 개수만큼만 생성
    if (spawnCount > 0)
    {
        // SpawnDice() 대신 직접 생성
        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex = Random.Range(0, dicePrefabs.Length);
            GameObject selectedPrefab = dicePrefabs[randomIndex];
            GameObject dice = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            dice.name = $"Dice_{spawnedDice.Count + 1} (Type: dice_{randomIndex + 1})";
            spawnedDice.Add(dice);
            
            // 목표 위치 계산
            Vector3 targetPosition = CalculateTargetPosition(spawnedDice.Count - 1);
            StartCoroutine(MoveDiceToTarget(dice, targetPosition));
            
            yield return new WaitForSeconds(0.3f);
        }
    }
}
}

