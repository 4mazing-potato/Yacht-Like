using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 게임의 전체 흐름을 관리하는 메인 클래스
/// 시니어 개발자 팁: GameManager는 다른 시스템들을 조율하는 역할만 해요!
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("게임 구성 요소")]
    public List<DiceController> diceControllers;  // 주사위들 (5개)
    public DiceRoller diceRoller;
    public ScoreManager scoreManager;             // 점수 관리자
    
    [Header("게임 상태")]
    private GameState gameState;
    [Header("턴 관리")]
    public int rollsRemaining = 2;  // 한 턴에 주사위를 굴릴 수 있는 횟수 (야추는 보통 3번)

    void Start()
    {
        // 게임 상태 초기화
        gameState = new GameState();
        diceRoller.SpawnDice();
        Debug.Log("게임 시작! 야추 게임에 오신 것을 환영합니다!");
        Debug.Log($"현재 라운드: {gameState.currentRound}, 남은 굴림 횟수: {rollsRemaining}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rollsRemaining--;
            if (rollsRemaining == 0)
            {
                NextTurn();
            }
        }
    }
    
    /// <summary>
    /// 다음 턴으로 넘어갑니다
    /// </summary>
    public void NextTurn()
    {
        gameState.currentTurn++;
        diceRoller.SpawnDice();
        rollsRemaining = 2;  // 굴림 횟수 초기화
        
        // 모든 주사위 잠금 해제
        foreach (DiceController dice in diceControllers)
        {
            dice.Unlock();
        }
        
        Debug.Log($"다음 턴으로 이동! 턴: {gameState.currentTurn}");
    }
    
    /// <summary>
    /// 현재 주사위 값들을 리스트로 가져옵니다
    /// </summary>
    public List<int> GetDiceValues()
    {
        List<int> values = new List<int>();
        foreach (DiceController dice in diceControllers)
        {
            values.Add(dice.GetValue());
        }
        return values;
    }
}
