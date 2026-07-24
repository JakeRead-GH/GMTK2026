using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{

    [SerializeField] Digit[] digits;

    public void SetDisplayPattern(string displayPattern) {
        if (displayPattern == null) {
            displayPattern = "NULL";
        }
        int digitIdx = 0;
        foreach (char ch in displayPattern) {
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
        string displayVal = string.Empty;
        foreach (Digit digit in digits) 
        {
            displayVal += digit.GetDigitNumValue();  
        }
        print(displayVal);
        string winPattern = LevelInfoStore.instance.WinningDisplayPattern;
        if (displayVal == winPattern) 
        {
            return true;
        }
        return false;
    }
}
