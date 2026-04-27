using UnityEngine;

public class DiceController : MonoBehaviour
{
    public DiceData diceData;
    public int diceIndex;

    [SerializeField] private SpriteRenderer spriteRenderer;

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
        UpdateLockColor();
    }

    private void UpdateLockColor()
    {
        spriteRenderer.color = diceData.isLocked ? Color.white : Color.gray;
    }

}
