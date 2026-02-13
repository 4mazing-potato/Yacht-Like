using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiceRoll : MonoBehaviour
{
    [Header("Dice Prefab")]
    [SerializeField] private GameObject[] dicePrefab;

    private List<DiceData> diceDataList = new List<DiceData>();
    private List<GameObject> diceGameObjects = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator RollDiceSequence()
    {
        int diceCount = 5;
        Vector3 centerPosition = new Vector3(0, -3.5f, 0);

        for (int i = 0 ; i < diceCount; i++)
        {
            DiceData diceData = new DiceData();
            diceDataList.Add(diceData);

            GameObject prefab = dicePrefab[diceData.value - 1];

            GameObject newDice = Instantiate(prefab, new Vector3(0, -7f, 0), Quaternion.identity);
            newDice.name = $"Dice_{i + 1} (Value: {diceData.value})";

            DiceController diceController = newDice.GetComponent<DiceController>();
            diceController.diceData = diceData;
            diceController.diceIndex = i;

            diceGameObjects.Add(newDice);

            float totalWidth = (diceCount - 1) * 1.5f;
            float startX = centerPosition.x - (totalWidth / 2f);
            float xPosition = startX + (i * 1.5f);

            Vector3 targetPosition = new Vector3(xPosition, centerPosition.y, 0);
            
            StartCoroutine(MoveDiceToTarget(newDice, targetPosition));

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator MoveDiceToTarget(GameObject dice, Vector3 targetPosition)
    {
        Vector3 startPosition = dice.transform.position;
        float duration = 0.25f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            dice.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        dice.transform.position = targetPosition;
    }

    public IEnumerator ReRollDiceSequence()
    {
        for (int i = 0; i < diceDataList.Count; i++)
        {
            // 잠기지 않은 주사위만 리롤
            if (!diceDataList[i].isLocked)
            {
                // 1. 새로운 랜덤 값 생성
                diceDataList[i].value = Random.Range(1, 7);
                
                // 2. 기존 GameObject 제거
                Destroy(diceGameObjects[i]);
                
                // 3. 새 프리팹 선택
                GameObject newPrefab = dicePrefab[diceDataList[i].value - 1];
                
                // 4. 같은 위치에 새 GameObject 생성
                Vector3 currentPosition = CalculateTargetPosition(i);
                GameObject newDice = Instantiate(newPrefab, currentPosition, Quaternion.identity);
                newDice.name = $"Dice_{i + 1} (Value: {diceDataList[i].value})";
                
                DiceController diceController = newDice.GetComponent<DiceController>();
                diceController.diceData = diceDataList[i];
                diceController.diceIndex = i;
                
                // 5. GameObject 리스트 업데이트
                diceGameObjects[i] = newDice;
                
                yield return new WaitForSeconds(0.1f);  // 살짝 딜레이
            }
        }
    }

    Vector3 CalculateTargetPosition(int index)
    {
        int diceCount = diceDataList.Count;
        Vector3 centerPosition = new Vector3(0, -3.5f, 0);
        float totalWidth = (diceCount - 1) * 1.5f;
        float startX = centerPosition.x - (totalWidth / 2f);
        float xPosition = startX + (index * 1.5f);
        return new Vector3(xPosition, centerPosition.y, 0);
    }
}
