using UnityEngine;

// Temporary. Eventually the level loader will instantiate the cards with card.Initialize(action, uses, manager);

public sealed class ActionCardTestInitializer : MonoBehaviour
{
    [SerializeField] private ActionCard card;
    [SerializeField] private DigitAction action;
    [SerializeField, Min(0)] private int startingUses = 3;
    [SerializeField] private ActionSelectionManager selectionManager;

    private void Start()
    {
        if (
            card == null ||
            action == null ||
            selectionManager == null
        ) {
            Debug.LogError(
                "Action card test setup is missing a reference.",
                this
            );

            return;
        }

        card.Initialize(
            action,
            startingUses,
            selectionManager
        );
    }
}