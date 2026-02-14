using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Score Slot Texts")]
    [SerializeField] private TMP_Text onesText;
    [SerializeField] private TMP_Text twosText;
    [SerializeField] private TMP_Text threesText;
    [SerializeField] private TMP_Text foursText;
    [SerializeField] private TMP_Text fivesText;
    [SerializeField] private TMP_Text sixesText;
    [SerializeField] private TMP_Text fourOfAKindText;
    [SerializeField] private TMP_Text fullHouseText;
    [SerializeField] private TMP_Text smallStraightText;
    [SerializeField] private TMP_Text largeStraightText;
    [SerializeField] private TMP_Text yachtText;
    [SerializeField] private TMP_Text chanceText;

    [Header("Summary Texts")]
    [SerializeField] private TMP_Text upperSumText;
    [SerializeField] private TMP_Text bonusText;
    [SerializeField] private TMP_Text totalScoreText;

    [Header("Turn Info Texts")]
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private TMP_Text rerollsLeftText;
    
    public void UpdateScoreSlot(string scoreType, int score)
    {
        TMP_Text target = GetScoreSlotText(scoreType);
        if (target != null)
        {
            target.text = score.ToString();
        }
    }

    private TMP_Text GetScoreSlotText(string scoreType)
    {
        switch (scoreType)
        {
            case "Ones": return onesText;
            case "Twos": return twosText;
            case "Threes": return threesText;
            case "Fours": return foursText;
            case "Fives": return fivesText;
            case "Sixes": return sixesText;
            case "FourOfAKind": return fourOfAKindText;
            case "FullHouse": return fullHouseText;
            case "SmallStraight": return smallStraightText;
            case "LargeStraight": return largeStraightText;
            case "Yacht": return yachtText;
            case "Chance": return chanceText;
            default: return null;
        }
    }
}
