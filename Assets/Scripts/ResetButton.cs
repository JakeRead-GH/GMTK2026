using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class ResetButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        button.onClick.AddListener(OnResetClicked);
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnResetClicked);
        }
    }

    private void OnResetClicked()
    {
        GameManager.Instance.SetLevelInitialState();
    }
}
