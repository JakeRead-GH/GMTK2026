using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{

    [SerializeField] private Display display;

    [Header("References")]
    [SerializeField] private Button button;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image actionIcon;

    [Header("Undo State Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite selectedSprite;

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
    public void OnResetClicked()
    {
        display.SetInitialState();
    }

}
