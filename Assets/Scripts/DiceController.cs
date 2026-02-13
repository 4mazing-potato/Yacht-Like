using UnityEngine;

public class DiceController : MonoBehaviour
{
    public DiceData diceData;
    public int diceIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseDown()
    {
        ToggleLock();
    }

    public void ToggleLock()
    {
        diceData.isLocked = !diceData.isLocked;
    }
}
