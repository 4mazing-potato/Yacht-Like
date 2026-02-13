using UnityEngine;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    [Header("Turn Settings")]
    [SerializeField] private int totalTurns = 13;
    [SerializeField] private int ReRollsPerTurn = 2; // 턴 당 리롤 횟수

    [Header("References")]
    [SerializeField] private DiceRoller diceRoller;
    [SerializeField] private ScoreManager scoreManager;

    private ScoreSheet scoreSheet = new ScoreSheet();
    private int currentTurn = 1;
    private int rerollsLeft; // 턴 당 남은 리롤 횟수

    void Start()
    {
        StartTurn();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryReRoll();
        }
    }

    void StartTurn()
    {
        rerollsLeft = ReRollsPerTurn;
        StartCoroutine(diceRoller.StartNewTurnSequence());
    }

    void TryReRoll()
    {
        if (diceRoller.IsRolling) return;
        if (rerollsLeft <= 0) return;

        rerollsLeft--;
        StartCoroutine(diceRoller.ReRollDiceSequence());
    }

    void EndTurn()
    {
        currentTurn++;

        if (currentTurn > totalTurns)
        {
            Debug.Log("Game Over!");
            return;
        }
        StartTurn();
    }

    public void ConfirmScore(string scoretype)
    {
        if (diceRoller.IsRolling) return;

        List<int> diceValues = diceRoller.GetCurrentDiceValues();
        int score = scoreManager.CalculateScore(diceValues, scoretype);

        bool recorded = scoreSheet.RecordScore(scoretype, score);
        if(!recorded) return;
        
        EndTurn();
    }

    
}