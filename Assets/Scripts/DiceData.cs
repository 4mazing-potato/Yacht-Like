using UnityEngine;

/// <summary>
/// 주사위 하나의 데이터를 담는 클래스
/// 시니어 개발자 팁: 데이터와 로직을 분리하면 나중에 수정하기 쉬워요!
/// </summary>
[System.Serializable]
public class DiceData
{
    public int value;        // 주사위 값 (1~6)
    public bool isLocked;    // 주사위가 잠겨있는지 (야추에서는 선택한 주사위를 잠글 수 있어요)
    
    public DiceData()
    {
        value = 1;
        isLocked = false;
    }
}

/// <summary>
/// 게임의 전체 상태를 관리하는 데이터 클래스
/// </summary>
[System.Serializable]
public class GameState
{
    public int currentRound;     // 현재 라운드 (야추는 보통 12라운드)
    public int currentTurn;      // 현재 턴
    public bool isGameOver;      // 게임 종료 여부
    
    public GameState()
    {
        currentRound = 1;
        currentTurn = 1;
        isGameOver = false;
    }
}

