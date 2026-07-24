using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class UndoButton : MonoBehaviour
{
    [SerializeField] private UndoScript undoScript;
    [SerializeField] private Button button;

    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        button.onClick.AddListener(OnUndoClicked);
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnUndoClicked);
        }
    }

    private void OnUndoClicked()
    {
        undoScript.UndoState();
    }
}