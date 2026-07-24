using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardListener : MonoBehaviour
{
    [SerializeField] private UndoScript undoScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnUndoKeyDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            undoScript.UndoState();
        }
    }

    public void OnResetKeyDown(InputAction.CallbackContext context) {
        if (context.performed)
        {
            GameManager.Instance.SetLevelInitialState();
        }
    }
}
