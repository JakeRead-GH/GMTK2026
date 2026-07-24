using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{

    private static Dictionary<string, string> levelDisplayInitialStates = new Dictionary<string, string>
    {
        { "MainStage", "120" },
        { "Level_01", " 0NE" },
        { "Level_02", "22" },
        { "Level_03", "333" },
        { "Level_04", "4444444" },
        { "Level_05", "HELP" },
        { "Level_06", "0000" },
        { "Level_07", "0000" },
        { "Level_08", "0000" },
        { "Level_09", "0000" },
        { "Level_10", "0000" }
    };

    private static Dictionary<string, string> levelWinConditions = new Dictionary<string, string>
    {
        { "MainStage", "1200" },
        { "Level_01", "NE--" },
        { "Level_07", "-00-" }
    };

    [SerializeField] Digit[] digits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetInitialState();
    }

    void Update()
    {
        
    }

    public void SetInitialState() {
        string levelName = SceneManager.GetActiveScene().name;
        string pattern = levelDisplayInitialStates.GetValueOrDefault(levelName);
        if (pattern == null) { 
            return;
        }
        int digitIdx = 0;
        foreach (char ch in pattern) {
            if (digitIdx >= digits.Length) {
                break;
            }
            string digitValue = ch.ToString();
            digits[digitIdx].SetPatternFromValue(digitValue);
            digitIdx++;
        }
    }

    public bool CheckSuccess()
    {
        string levelName = SceneManager.GetActiveScene().name;
        string displayVal = string.Empty;
        foreach (Digit digit in digits) 
        {
            displayVal += digit.GetDigitNumValue();  
        }
        print(displayVal);
        string winPattern = levelWinConditions.GetValueOrDefault(levelName);
        if (displayVal == winPattern) 
        {
            return true;
        }
        return false;
    }
}
