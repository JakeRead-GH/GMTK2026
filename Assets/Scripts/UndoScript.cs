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
            string lockedPattern = stateSnapshot.LockedPatterns[digit];
            digit.SetPattern(digitPattern);
            digit.SetLockedPattern(lockedPattern);
        }

        ActionCard actionUsed = stateSnapshot.ActionUsed;
        actionUsed.IncrementRemainingUses();
        actionUsed.RefreshVisuals();
    }
}
