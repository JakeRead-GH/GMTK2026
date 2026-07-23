using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public sealed class DigitClickTarget :
    MonoBehaviour,
    IPointerClickHandler
{
    [SerializeField] private Digit digit;
    [SerializeField] private ActionSelectionManager selectionManager;

    private void Reset()
    {
        digit = GetComponent<Digit>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) {
            return;
        }

        if (digit == null || selectionManager == null) {
            Debug.LogError(
                $"{name} is missing its digit or selection manager.",
                this
            );

            return;
        }

        selectionManager.SelectDigit(digit);
        eventData.Use();
    }
}