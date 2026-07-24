using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class HomeButton : MonoBehaviour
{
    [SerializeField] private Button button;
    
    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        button.onClick.AddListener(OnHomeClicked);
    }

    private void OnHomeClicked()
    {
        SceneLoader.instance.LoadScene("Title Screen");
    }
}