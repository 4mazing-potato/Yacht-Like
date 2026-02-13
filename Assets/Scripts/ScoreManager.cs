using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 점수를 계산하고 관리하는 클래스
/// 시니어 개발자 팁: 점수 계산 로직을 별도 클래스로 분리하면 테스트하기 쉬워요!
/// </summary>
public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// 주사위 값들의 리스트를 받아서 점수를 계산합니다
    /// </summary>
    public int CalculateScore(List<int> diceValues, string scoreType)
    {
        // 주사위 값들을 정렬해서 계산하기 쉽게 만듦
        List<int> sortedValues = new List<int>(diceValues);
        sortedValues.Sort();
        
        switch (scoreType)
        {
            // 상위 항목 (Upper Section)
            case "Ones":
                return CountValue(sortedValues, 1);
            case "Twos":
                return CountValue(sortedValues, 2);
            case "Threes":
                return CountValue(sortedValues, 3);
            case "Fours":
                return CountValue(sortedValues, 4);
            case "Fives":
                return CountValue(sortedValues, 5);
            case "Sixes":
                return CountValue(sortedValues, 6);
            
            // 하위 항목 (Lower Section)
            case "ThreeOfAKind":
                return CalculateThreeOfAKind(sortedValues);
            case "FourOfAKind":
                return CalculateFourOfAKind(sortedValues);
            case "FullHouse":
                return CalculateFullHouse(sortedValues);
            case "SmallStraight":
                return CalculateSmallStraight(sortedValues);
            case "LargeStraight":
                return CalculateLargeStraight(sortedValues);
            case "Yacht":
                return CalculateYacht(sortedValues);
            case "Chance":
                return CalculateChance(sortedValues);
            
            default:
                return 0;
        }
    }
    
    /// <summary>
    /// 특정 값이 몇 개 있는지 세어서 점수로 반환
    /// 예: [1,1,1,2,3]에서 1을 찾으면 3점 (1+1+1)
    /// </summary>
    private int CountValue(List<int> diceValues, int targetValue)
    {
        int count = 0;
        foreach (int value in diceValues)
        {
            if (value == targetValue)
            {
                count += targetValue;  // 값 * 개수
            }
        }
        return count;
    }
    
    /// <summary>
    /// 같은 숫자가 3개 이상 있으면 모든 주사위 합계, 아니면 0
    /// </summary>
    private int CalculateThreeOfAKind(List<int> diceValues)
    {
        for (int i = 1; i <= 6; i++)
        {
            int count = diceValues.Count(x => x == i);
            if (count >= 3)
            {
                return diceValues.Sum();  // 모든 주사위 합계
            }
        }
        return 0;
    }
    
    /// <summary>
    /// 같은 숫자가 4개 이상 있으면 모든 주사위 합계, 아니면 0
    /// </summary>
    private int CalculateFourOfAKind(List<int> diceValues)
    {
        for (int i = 1; i <= 6; i++)
        {
            int count = diceValues.Count(x => x == i);
            if (count >= 4)
            {
                return diceValues.Sum();  // 모든 주사위 합계
            }
        }
        return 0;
    }
    
    /// <summary>
    /// 트리플(3개) + 원페어(2개) 조합이면 25점, 아니면 0
    /// </summary>
    private int CalculateFullHouse(List<int> diceValues)
    {
        // 주사위 값들의 개수를 세기
        Dictionary<int, int> counts = new Dictionary<int, int>();
        foreach (int value in diceValues)
        {
            if (counts.ContainsKey(value))
                counts[value]++;
            else
                counts[value] = 1;
        }
        
        bool hasThree = false;
        bool hasTwo = false;
        
        foreach (var count in counts.Values)
        {
            if (count == 3) hasThree = true;
            if (count == 2) hasTwo = true;
        }
        
        return (hasThree && hasTwo) ? 25 : 0;
    }
    
    /// <summary>
    /// 연속된 숫자 4개가 있으면 30점, 아니면 0
    /// 예: 1,2,3,4 또는 2,3,4,5 또는 3,4,5,6
    /// </summary>
    private int CalculateSmallStraight(List<int> diceValues)
    {
        // 중복 제거하고 정렬
        List<int> uniqueValues = diceValues.Distinct().OrderBy(x => x).ToList();
        
        // 연속된 4개가 있는지 확인
        for (int i = 0; i <= uniqueValues.Count - 4; i++)
        {
            if (uniqueValues[i + 3] == uniqueValues[i] + 3)
            {
                return 30;
            }
        }
        return 0;
    }
    
    /// <summary>
    /// 연속된 숫자 5개가 있으면 40점, 아니면 0
    /// 예: 1,2,3,4,5 또는 2,3,4,5,6
    /// </summary>
    private int CalculateLargeStraight(List<int> diceValues)
    {
        // 중복 제거하고 정렬
        List<int> uniqueValues = diceValues.Distinct().OrderBy(x => x).ToList();
        
        if (uniqueValues.Count == 5)
        {
            // 1,2,3,4,5 또는 2,3,4,5,6인지 확인
            if ((uniqueValues[0] == 1 && uniqueValues[4] == 5) ||
                (uniqueValues[0] == 2 && uniqueValues[4] == 6))
            {
                return 40;
            }
        }
        return 0;
    }
    
    /// <summary>
    /// 모든 주사위가 같은 숫자면 50점, 아니면 0
    /// </summary>
    private int CalculateYacht(List<int> diceValues)
    {
        int firstValue = diceValues[0];
        foreach (int value in diceValues)
        {
            if (value != firstValue)
            {
                return 0;  // 하나라도 다르면 0점
            }
        }
        return 50;  // 모두 같으면 50점
    }
    
    /// <summary>
    /// 모든 주사위의 합계 (항상 가능)
    /// </summary>
    private int CalculateChance(List<int> diceValues)
    {
        return diceValues.Sum();
    }
}