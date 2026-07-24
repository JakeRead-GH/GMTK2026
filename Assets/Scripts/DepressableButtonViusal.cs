using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Button))]
public sealed class DepressableButtonVisual :
    MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private Button button;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image borderImage;
    [SerializeField] private RectTransform pressableContent;

    [Header("Border Sprites")]
    [SerializeField] private Sprite normalBorderSprite;
    [SerializeField] private Sprite pressedBorderSprite;

    [Header("Fill Colours")]
    [SerializeField] private Color normalFillColour = Color.white;
    [SerializeField] private Color pressedFillColour =
        new Color(0.7f, 0.7f, 0.7f, 1f);

    [Header("Pressed Position")]
    [SerializeField] private Vector2 pressedOffset =
        new Vector2(0f, -4f);
    
    [Header("Keyboard Shortcut")]
    [SerializeField] private InputActionReference shortcutAction;

    private Vector2 contentRestPosition;

    private bool pointerHeld;
    private bool pointerInside;
    private bool shortcutHeld;

    private bool IsPressed =>
        button != null &&
        button.IsInteractable() && (
            (pointerHeld && pointerInside) ||
            shortcutHeld
        );
    
    public void SetShortcutPressed(bool pressed)
    {
        if (shortcutHeld == pressed)
        {
            return;
        }

        shortcutHeld = pressed;
        RefreshVisuals();
    }

    private void Awake()
    {
        if (button == null) {
            button = GetComponent<Button>();
        }

        if (pressableContent != null) {
            contentRestPosition = pressableContent.anchoredPosition;
        }

        RefreshVisuals();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (
            eventData.button != PointerEventData.InputButton.Left ||
            button == null ||
            !button.IsInteractable()
        ) {
            return;
        }

        pointerHeld = true;
        pointerInside = true;

        RefreshVisuals();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (
            eventData.button !=
            PointerEventData.InputButton.Left
        ) {
            return;
        }

        pointerHeld = false;
        RefreshVisuals();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerInside = true;
        RefreshVisuals();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerInside = false;
        RefreshVisuals();
    }
    
    private bool enabledShortcutHere;

    private void OnEnable()
    {
        if (shortcutAction == null || shortcutAction.action == null) {
            return;
        }

        InputAction action = shortcutAction.action;

        action.started += OnShortcutStarted;
        action.canceled += OnShortcutCanceled;

        // This allows the action to work even if no PlayerInput is enabling it.
        enabledShortcutHere = !action.enabled;

        if (enabledShortcutHere) {
            action.Enable();
        }
    }

    private void OnShortcutStarted(
        InputAction.CallbackContext context
    )
    {
        if (button == null || !button.IsInteractable()) {
            return;
        }

        shortcutHeld = true;
        RefreshVisuals();
    }

    private void OnShortcutCanceled(
        InputAction.CallbackContext context
    )
    {
        bool shouldClick =
            shortcutHeld &&
            button != null &&
            button.IsInteractable();

        shortcutHeld = false;
        RefreshVisuals();

        if (shouldClick)
        {
            button.onClick.Invoke();
        }
    }

    private void RefreshVisuals()
    {
        bool pressed = IsPressed;

        if (borderImage != null) {
            borderImage.sprite = pressed
                ? pressedBorderSprite
                : normalBorderSprite;
        }

        if (fillImage != null) {
            fillImage.color = pressed
                ? pressedFillColour
                : normalFillColour;
        }

        if (pressableContent != null) {
            pressableContent.anchoredPosition =
                contentRestPosition +
                (pressed
                    ? pressedOffset
                    : Vector2.zero);
        }
    }

    private void OnDisable()
    {
        if (shortcutAction != null && shortcutAction.action != null)
        {
            InputAction action = shortcutAction.action;

            action.started -= OnShortcutStarted;
            action.canceled -= OnShortcutCanceled;

            if (enabledShortcutHere)
            {
                action.Disable();
            }
        }

        pointerHeld = false;
        pointerInside = false;
        shortcutHeld = false;
        enabledShortcutHere = false;

        RefreshVisuals();
    }
}