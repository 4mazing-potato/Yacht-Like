using UnityEngine;

/// <summary>
/// 주사위 하나를 제어하는 클래스
/// DiceRoller가 프리팹을 랜덤 생성하므로, 여기서는 잠금/해제만 담당
/// </summary>
public class DiceController : MonoBehaviour
{
    private DiceData diceData;           // 이 주사위의 데이터
    private SpriteRenderer spriteRenderer;  // 주사위 이미지
    
    void Start()
    {
        // 주사위 데이터 초기화
        diceData = new DiceData();
        
        // SpriteRenderer 찾기 (프리팹의 이미지 컴포넌트)
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // 프리팹 이름에서 주사위 값 추출
        ExtractValueFromPrefabName();
    }
    
    /// <summary>
    /// 프리팹 이름에서 주사위 값을 추출합니다
    /// 예: "dice_3" → value = 3
    /// </summary>
    void ExtractValueFromPrefabName()
    {
        string objectName = gameObject.name;
        
        // "dice_3 (Type: dice_1)" 같은 형태에서 추출
        if (objectName.Contains("Type: dice_"))
        {
            // "Type: dice_3)" 부분 찾기
            int startIndex = objectName.IndexOf("Type: dice_") + 11;
            int endIndex = objectName.IndexOf(")", startIndex);
            string numberString = objectName.Substring(startIndex, endIndex - startIndex);
            diceData.value = int.Parse(numberString);
        }
        // 단순히 "dice_3" 형태일 경우
        else if (objectName.Contains("dice_"))
        {
            string numberString = objectName.Replace("dice_", "").Replace("(Clone)", "").Trim();
            // 숫자만 추출
            numberString = System.Text.RegularExpressions.Regex.Match(numberString, @"\d+").Value;
            if (!string.IsNullOrEmpty(numberString))
            {
                diceData.value = int.Parse(numberString);
            }
        }
        
        Debug.Log($"{gameObject.name}의 value: {diceData.value}");
    }   
    
    /// <summary>
    /// 주사위를 잠그거나 잠금 해제합니다
    /// </summary>
    public void ToggleLock()
    {
        diceData.isLocked = !diceData.isLocked;
        Debug.Log($"주사위 {diceData.value} {(diceData.isLocked ? "잠금" : "잠금 해제")}");
    }
    
    /// <summary>
    /// 주사위를 클릭하면 잠금/해제
    /// </summary>
    void OnMouseDown()
    {
        ToggleLock();
    }
    
    /// <summary>
    /// 현재 주사위 값을 가져옵니다
    /// </summary>
    public int GetValue()
    {
        return diceData.value;
    }
    
    /// <summary>
    /// 주사위가 잠겨있는지 확인합니다
    /// </summary>
    public bool IsLocked()
    {
        return diceData.isLocked;
    }
    
    /// <summary>
    /// 주사위 잠금을 해제합니다
    /// </summary>
    public void Unlock()
    {
        if (diceData.isLocked)
        {
            diceData.isLocked = false;
        }
    }
}