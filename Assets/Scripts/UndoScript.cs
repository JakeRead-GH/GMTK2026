using UnityEngine;

public class UndoScript : MonoBehaviour
{
    [SerializeField] private Digit[] digits;
    public void UndoState()
    {
        StateSnapshot stateSnapshot = StateManager.Instance.PopSnapshot();
        if (stateSnapshot == null)
        {
            return;
        }

        foreach (Digit digit in digits)
        {
            string digitPattern = stateSnapshot.DigitPatterns[digit];
            digit.SetPattern(stateSnapshot.DigitPatterns[digit]);
        }

        ActionCard actionUsed = stateSnapshot.ActionUsed;
        actionUsed.IncrementRemainingUses();
        actionUsed.RefreshVisuals();
    }
}
