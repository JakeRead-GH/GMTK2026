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

    void Start()
    {
        //digits[0].SetPattern("12");
        //digits[1].SetPattern("0123456");

        //digits[0].SwapWith(digits[1]);
        //digits[0].CopyFrom(digits[1]);
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
        /*for (int i = 0; i < digits.Length; i++) { 
            pattern = levelDisplayInitialStates.GetValueOrDefault(levelName);
            if (pattern == null) { 
                continue;
            }
            digits[i].SetPatternFromValue(levelDisplayInitialStates.GetValueOrDefault(levelName));
        }*/
    }

}
