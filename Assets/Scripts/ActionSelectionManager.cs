using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class ActionSelectionManager : MonoBehaviour
{

    private readonly List<Digit> selectedTargets = new();

    private ActionCard selectedCard;

    public void SelectCard(ActionCard card)
    {
        if (card == null || !card.HasUsesRemaining) {
            return;
        }

        if (selectedCard == card) {
            ClearSelection(card);
            return;
        }

        selectedCard?.SetSelected(false);

        selectedCard = card;
        selectedCard.SetSelected(true);
        selectedTargets.Clear();

        DigitAction action = selectedCard.Action;

        Cursor.SetCursor(
            action.CursorTexture,
            action.CursorHotspot,
            CursorMode.Auto
        );
    }

    public void SelectDigit(Digit digit)
    {
        if (selectedCard == null || digit == null) {
            return;
        }

        selectedTargets.Add(digit);

        int requiredTargets = selectedCard.Action.RequiredTargetCount;

        if (selectedTargets.Count < requiredTargets) {
            return;
        }

        ActionCard cardBeingUsed = selectedCard;

        print(cardBeingUsed);
        StateManager.Instance.SetLastUsedAction(cardBeingUsed);
        StateManager.Instance.TakeSnapshot();
        bool actionSucceeded = cardBeingUsed.TryUse(selectedTargets);

        selectedTargets.Clear();

        if (!actionSucceeded) {
            Debug.Log("That action cannot be applied to those targets.");
            StateManager.Instance.PopSnapshot();
        }
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