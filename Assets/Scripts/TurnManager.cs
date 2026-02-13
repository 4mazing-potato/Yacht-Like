using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [Header("Turn Settings")]
    [SerializeField] private int totalTurns = 13;
    [SerializeField] private int ReRollsPerTurn = 2;

    [Header("References")]
    [SerializeField] private DiceRoll diceRoller;
    [SerializeField] private ScoreManager scoreManager;

    private int currentTurn = 1;
    private int rerollsLeft;

    void Start()
    {
        StartTurn();
        StartCoroutine(diceRoller.RollDiceSequence());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryReRoll();
        }
    }

    void TryReRoll()
    {
        if (rerollsLeft <= 0) return;

        StartCoroutine(diceRoller.ReRollDiceSequence());
        rerollsLeft--;
    }

    public void StartTurn()
    {
        rerollsLeft = ReRollsPerTurn;
    }

}