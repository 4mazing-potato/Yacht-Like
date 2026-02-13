using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 점수표를 관리하는 클래스
/// 시니어 개발자 팁: 점수표는 게임의 핵심이니까 별도 클래스로 분리하는 게 좋아요!
/// </summary>
[System.Serializable]
public class ScoreSheet
{
    // 상위 항목 (Upper Section)
    public int ones = -1;      // -1은 아직 기록 안 됨, 0 이상은 기록됨
    public int twos = -1;
    public int threes = -1;
    public int fours = -1;
    public int fives = -1;
    public int sixes = -1;
    
    // 하위 항목 (Lower Section)
    public int threeOfAKind = -1;
    public int fourOfAKind = -1;
    public int fullHouse = -1;
    public int smallStraight = -1;
    public int largeStraight = -1;
    public int yacht = -1;
    public int chance = -1;
    
    /// <summary>
    /// 상위 항목의 합계를 계산합니다
    /// </summary>
    public int GetUpperSum()
    {
        int sum = 0;
        if (ones >= 0) sum += ones;
        if (twos >= 0) sum += twos;
        if (threes >= 0) sum += threes;
        if (fours >= 0) sum += fours;
        if (fives >= 0) sum += fives;
        if (sixes >= 0) sum += sixes;
        return sum;
    }
    
    /// <summary>
    /// 보너스 점수를 계산합니다 (상위 합계 63 이상이면 35점)
    /// </summary>
    public int GetBonus()
    {
        return GetUpperSum() >= 63 ? 35 : 0;
    }
    
    /// <summary>
    /// 총점을 계산합니다
    /// </summary>
    public int GetTotalScore()
    {
        int total = GetUpperSum() + GetBonus();
        
        // 하위 항목 합계
        if (threeOfAKind >= 0) total += threeOfAKind;
        if (fourOfAKind >= 0) total += fourOfAKind;
        if (fullHouse >= 0) total += fullHouse;
        if (smallStraight >= 0) total += smallStraight;
        if (largeStraight >= 0) total += largeStraight;
        if (yacht >= 0) total += yacht;
        if (chance >= 0) total += chance;
        
        return total;
    }
    
    /// <summary>
    /// 특정 항목에 점수를 기록합니다
    /// </summary>
    public bool RecordScore(string scoreType, int score)
    {
        switch (scoreType)
        {
            case "Ones":
                if (ones >= 0) return false;  // 이미 기록됨
                ones = score;
                return true;
            case "Twos":
                if (twos >= 0) return false;
                twos = score;
                return true;
            case "Threes":
                if (threes >= 0) return false;
                threes = score;
                return true;
            case "Fours":
                if (fours >= 0) return false;
                fours = score;
                return true;
            case "Fives":
                if (fives >= 0) return false;
                fives = score;
                return true;
            case "Sixes":
                if (sixes >= 0) return false;
                sixes = score;
                return true;
            case "ThreeOfAKind":
                if (threeOfAKind >= 0) return false;
                threeOfAKind = score;
                return true;
            case "FourOfAKind":
                if (fourOfAKind >= 0) return false;
                fourOfAKind = score;
                return true;
            case "FullHouse":
                if (fullHouse >= 0) return false;
                fullHouse = score;
                return true;
            case "SmallStraight":
                if (smallStraight >= 0) return false;
                smallStraight = score;
                return true;
            case "LargeStraight":
                if (largeStraight >= 0) return false;
                largeStraight = score;
                return true;
            case "Yacht":
                if (yacht >= 0) return false;
                yacht = score;
                return true;
            case "Chance":
                if (chance >= 0) return false;
                chance = score;
                return true;
            default:
                return false;
        }
    }
    
    /// <summary>
    /// 모든 항목이 기록되었는지 확인합니다 (13턴 완료 체크용)
    /// </summary>
    public bool IsScoreSheetComplete()
    {
        return ones >= 0 && twos >= 0 && threes >= 0 && fours >= 0 && 
               fives >= 0 && sixes >= 0 && threeOfAKind >= 0 && 
               fourOfAKind >= 0 && fullHouse >= 0 && smallStraight >= 0 && 
               largeStraight >= 0 && yacht >= 0 && chance >= 0;
    }
}