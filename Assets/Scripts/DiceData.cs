using UnityEngine;
using System.Collections;

public class DiceData
{
    public int value; //주사위 눈
    public bool isLocked; // 리롤 잠금 = 1

    public DiceData()
    {
        value = Random.Range(1, 7);
        isLocked = false;
    }

    public DiceData(int diceValue)
    {
        value = diceValue;
        isLocked = false;
    }
}
