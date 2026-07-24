using UnityEngine;

public class UndoScript : MonoBehaviour
{
    [SerializeField] private Digit[] digits;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip undoClip;

    public void UndoState()
    {
        SoundFXManager.instance.PlaySoundFXClip(undoClip, transform, 1f);

        StateSnapshot stateSnapshot = StateManager.instance.PopSnapshot();
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
