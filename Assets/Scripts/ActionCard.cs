using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class ActionCard : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button button;
    [SerializeField] private Image borderImage;
    [SerializeField] private RectTransform centreComponents;
    [SerializeField] private Vector2 selectedContentOffset = new Vector2(0f, -8f);
    [SerializeField] private Image actionIcon;
    [SerializeField] private SevenSegmentPreview segmentPreview;
    [SerializeField] private TMP_Text remainingUsesText;
    [SerializeField] private Image fillImage;
    [SerializeField] private CanvasGroup cardCanvasGroup;

    [Header("Card State Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite exhaustedSprite;
    
    [Header("Fill Colours")]
    [SerializeField] private Color normalFillColour = Color.white;
    [SerializeField] private Color selectedFillColour = Color.gray;

    [Header("Exhausted State")]
    [SerializeField, Range(0f, 1f)]
    private float exhaustedAlpha = 0.55f;

    private DigitAction action;
    private ActionSelectionManager selectionManager;

    private int remainingUses;
    private bool isSelected;
    private bool isInitialized;

    private Vector2 contentRestPosition;
    public DigitAction Action => action;
    public int RemainingUses => remainingUses;
    public bool HasUsesRemaining => remainingUses > 0;

    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (centreComponents != null)
        {
            contentRestPosition = centreComponents.anchoredPosition;
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
        bool pressed = usable && isSelected;

        if (button != null) {
            button.interactable = usable;
        }

        if (remainingUsesText != null) {
            remainingUsesText.text = remainingUses.ToString();
        }

        // Swap the outer border.
        if (borderImage != null) {
            if (!usable) {
                borderImage.sprite = exhaustedSprite != null
                    ? exhaustedSprite
                    : normalSprite;
            } else {
                borderImage.sprite = pressed
                    ? selectedSprite
                    : normalSprite;
            }
        }

        // Tint the same fill sprite darker while selected.
        if (fillImage != null)
        {
            fillImage.color = pressed
                ? selectedFillColour
                : normalFillColour;
        }

        // Move the icon, preview, and use count downward.
        if (centreComponents != null) {
            centreComponents.anchoredPosition =
                contentRestPosition +
                (pressed ? selectedContentOffset : Vector2.zero);
        }

        // Fade every visual when no uses remain.
        if (cardCanvasGroup != null) {
            cardCanvasGroup.alpha = usable
                ? 1f
                : exhaustedAlpha;

            cardCanvasGroup.interactable = usable;
            cardCanvasGroup.blocksRaycasts = usable;
        }
    }
}
