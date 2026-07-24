using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour
{

    private static string[] levelNames = new string[] { "Level_01", "Level_02", "Level_03", "Level_04", "Level_05", "Level_06", "Level_07", "Level_08", "Level_09", "Level_10" };

    public static SceneLoader instance;

    public ActionCard[] cardList;

    void Start()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else { 
            Destroy(this);
        }
    }

    void Update()
    {
        //key presses for testing. Can remove UnityEngine.InputSystem when done with these
        if (Keyboard.current.digit1Key.wasReleasedThisFrame) {
            LoadScene("Level_01");
        }
        if (Keyboard.current.escapeKey.wasReleasedThisFrame) {
            LoadScene("Title Screen");
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame) {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel() { 
        int currentIndx = Array.IndexOf(levelNames, SceneManager.GetActiveScene().name);
        if (currentIndx == -1) { 
            return;
        }
        currentIndx++;
        if (currentIndx < levelNames.Length) { 
            LoadScene(levelNames[currentIndx]);
        }
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
