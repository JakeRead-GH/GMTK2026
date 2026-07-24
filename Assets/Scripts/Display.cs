using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{

    private static Dictionary<string, string> levelDisplayInitialStates = new Dictionary<string, string>
    {
        { "Level_01", " 0NE" },
        { "Level_02", "22" },
        { "Level_03", "333" },
        { "Level_04", "4444444" },
        { "Level_05", "HELP" }
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

    void SetInitialState() {
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

}
