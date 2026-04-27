using UnityEngine;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    [Header("Turn Settings")]
    [SerializeField] private int totalTurns = 12;
    [SerializeField] private int ReRollsPerTurn = 2; // 턴 당 리롤 횟수

    [Header("References")]
    [SerializeField] private DiceRoller diceRoller;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UIManager uiManager;

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
        uiManager.UpdateTurnInfoText(currentTurn, totalTurns, rerollsLeft);
        RefreshSummaryUI(false);
        StartCoroutine(diceRoller.StartNewTurnSequence());
    }

    void TryReRoll()
    {
        if (diceRoller.IsRolling) return;
        if (rerollsLeft <= 0) return;

        rerollsLeft--;
        uiManager.UpdateTurnInfoText(currentTurn, totalTurns, rerollsLeft);
        StartCoroutine(diceRoller.ReRollDiceSequence());
    }

    void EndTurn()
    {
        currentTurn++;

        if (currentTurn > totalTurns)
        {
            RefreshSummaryUI(true);
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
        
        uiManager.UpdateScoreSlot(scoretype, score);
        
        EndTurn();
    }

    private void RefreshSummaryUI(bool showTotalScore)
    {
        int upperSum = scoreSheet.GetUpperSum();
        int bonus = scoreSheet.GetBonus();
        int totalScore = scoreSheet.GetTotalScore();

        uiManager.UpdateSummaryText(upperSum, bonus, totalScore, showTotalScore);
    }
}