using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class ActionSelectionManager : MonoBehaviour
{

    [Header("Apply Action")]
    [SerializeField] private AudioClip actionClip;
    [SerializeField] private AudioClip deselectClip;
    [SerializeField] private AudioClip outOfMovesClip;

    private readonly List<Digit> selectedTargets = new();

    private ActionCard selectedCard;

    public void SelectCard(ActionCard card)
    {
        if (card == null || !card.HasUsesRemaining) {
            return;
        }

        if (selectedCard == card) {
            ClearSelection(card);
            SoundFXManager.instance.PlaySoundFXClip(deselectClip, transform, 1f);
            return;
        }

        selectedCard?.SetSelected(false);

        selectedCard = card;
        selectedCard.SetSelected(true);
        selectedTargets.Clear();

        DigitAction action = selectedCard.Action;

    }

    public ActionCard GetActionCard() { 
        return selectedCard;
    }

    public void SelectDigit(Digit digit)
    {
        if (selectedCard == null || digit == null) {
            SoundFXManager.instance.PlaySoundFXClip(outOfMovesClip, transform, 1f);
            return;
        }
        
        // Stops them using a multiselect action on the same digit twice
        if (selectedTargets.Contains(digit))
        {
            return;
        }

        selectedTargets.Add(digit);

        int requiredTargets = selectedCard.Action.RequiredTargetCount;

        if (selectedTargets.Count < requiredTargets) {
            return;
        }

        ActionCard cardBeingUsed = selectedCard;

        print(cardBeingUsed);
        StateManager.instance.SetLastUsedAction(cardBeingUsed);
        StateManager.instance.TakeSnapshot();
        bool actionSucceeded = cardBeingUsed.TryUse(selectedTargets);

        selectedTargets.Clear();

        if (!actionSucceeded) {
            Debug.Log("That action cannot be applied to those targets.");
            StateManager.instance.PopSnapshot();
        }
        SoundFXManager.instance.PlaySoundFXClip(actionClip, transform, 1f);

        GameManager.instance.HandleSucess();
    }

    public void ClearSelection(ActionCard requestingCard = null) {
        if (
            requestingCard != null &&
            selectedCard != requestingCard
        ) {
            return;
        }

        ActionCard previousCard = selectedCard;

        selectedCard = null;
        selectedTargets.Clear();

        previousCard?.SetSelected(false);

        Cursor.SetCursor(
            null,
            Vector2.zero,
            CursorMode.Auto
        );
    }

    private void OnDisable()
    {
        ClearSelection();
    }
}