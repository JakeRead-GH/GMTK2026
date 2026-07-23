using UnityEngine;

public class UndoManager : MonoBehaviour
{
    [SerializeField] private Digit[] digits;
    
    public void UndoState()
    {
        StateSnapshot stateSnapshot = StateManager.Instance.PopSnapshot();
        foreach(Digit digit in digits)
        {
            string digitPattern = stateSnapshot.DigitPatterns[digit];
            digit.SetPattern(stateSnapshot.DigitPatterns[digit]);
        }
        stateSnapshot.ActionUsed.IncrementRemainingUses();
    }
}
