using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{

    [SerializeField] private Digit[] digits;

    [Header("References")]
    [SerializeField] private Button button;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image actionIcon;

    [Header("Card State Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite selectedSprite;
    public void UndoState()
    {
        print("clicked");
        StateSnapshot stateSnapshot = StateManager.Instance.PopSnapshot();
        foreach (Digit digit in digits)
        {
            string digitPattern = stateSnapshot.DigitPatterns[digit];
            digit.SetPattern(stateSnapshot.DigitPatterns[digit]);
        }
        stateSnapshot.ActionUsed.IncrementRemainingUses();
    }
}
