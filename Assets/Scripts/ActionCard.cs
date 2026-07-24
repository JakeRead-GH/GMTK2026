using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class ActionCard : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button button;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image actionIcon;
    [SerializeField] private SevenSegmentPreview segmentPreview;
    [SerializeField] private TMP_Text remainingUsesText;

    [Header("Card State Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite exhaustedSprite;

    private DigitAction action;
    private ActionSelectionManager selectionManager;

    private int remainingUses;
    private bool isSelected;
    private bool isInitialized;

    public DigitAction Action => action;
    public int RemainingUses => remainingUses;
    public bool HasUsesRemaining => remainingUses > 0;

    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        button.onClick.AddListener(OnCardClicked);
        button.interactable = false;
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnCardClicked);
        }
    }

    public void IncrementRemainingUses() 
    { 
        remainingUses++;
    }

    public void Initialize(
        DigitAction newAction,
        int startingUses,
        ActionSelectionManager actionManager
    )
    {
        if (newAction == null || actionManager == null)
        {
            Debug.LogError(
                $"ActionCard {name} received invalid initialization data.",
                this
            );

            return;
        }

        action = newAction;
        remainingUses = Mathf.Max(0, startingUses);
        selectionManager = actionManager;
        isSelected = false;
        isInitialized = true;

        if (actionIcon != null)
        {
            actionIcon.sprite = action.CardIcon;
            actionIcon.enabled = action.CardIcon != null;
            actionIcon.preserveAspect = true;
        }

        if (segmentPreview != null)
        {
            segmentPreview.SetMask(action.AffectedSegments);
        }

        RefreshVisuals();
    }

    private void OnCardClicked()
    {
        if (!isInitialized || !HasUsesRemaining)
        {
            return;
        }

        selectionManager.SelectCard(this);
    }

    public bool TryUse(IReadOnlyList<Digit> targets)
    {
        if (!isInitialized || !HasUsesRemaining)
        {
            return false;
        }

        if (!action.TryApply(targets))
        {
            return false;
        }

        remainingUses--;

        if (!HasUsesRemaining)
        {
            selectionManager.ClearSelection(this);
        }

        RefreshVisuals();
        return true;
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        RefreshVisuals();
    }

    public void RefreshVisuals()
    {
        bool usable = isInitialized && HasUsesRemaining;

        if (button != null)
        {
            button.interactable = usable;
        }

        if (remainingUsesText != null)
        {
            remainingUsesText.text = remainingUses.ToString();
        }

        if (backgroundImage == null)
        {
            return;
        }

        if (!usable)
        {
            backgroundImage.sprite = exhaustedSprite != null
                ? exhaustedSprite
                : normalSprite;
        }
        else
        {
            backgroundImage.sprite = isSelected
                ? selectedSprite
                : normalSprite;
        }
    }
}
