using System;
using System.Collections.Generic;
using UnityEngine;

public class Digit : MonoBehaviour
{
    [SerializeField] DigitSegment[] digitSegments;
    [SerializeField] Color litColour;
    [SerializeField] int boundarySegments;

    private static Dictionary<string, string> displayToValMapping = new Dictionary<string, string> 
    { 
        { "012345", "0" }, 
        { "12", "1" }, 
        { "01346", "2" }, 
        { "01236", "3" }, 
        { "1256", "4" }, 
        { "02356", "5" }, 
        { "023456", "6" }, 
        { "012", "7" }, 
        { "0123456", "8" }, 
        { "012356" , "9" } 
    };
    private static Dictionary<string, string> valToDisplayMapping = new Dictionary<string, string>
    {
        { "0", "012345"},
        { "1", "12"},
        { "2", "01346" },
        { "3", "01236" },
        { "4", "1256" },
        { "5", "02356" },
        { "6", "023456" },
        { "7", "012" },
        { "8", "0123456" },
        { "9", "012356" }
    };

    //sorry for my dogshit coding. tired as fuck. also just shit
    
    private void Awake()
    {
        foreach (DigitSegment segment in digitSegments) {
            segment.SetLitColour(litColour);
        }
    }

    public DigitSegment GetSegment(int segmentNumber) { 
        return digitSegments[segmentNumber];
    }

    public string GetPattern()
    {
        string pattern = string.Empty;
        for (int i = 0; i < digitSegments.Length; i++)
        {
            DigitSegment currSegment = digitSegments[i];
            if (GetSegment(i).IsLit())
                pattern += i.ToString();
        }
        return pattern;
    }

    public void SetPattern(string pattern)
    {
        TurnOffDigit();
        foreach (char chIdx in pattern) { 
            int segIdx = CharToInt(chIdx);
            digitSegments[segIdx].ToggleOn();
        }
    }

    public void TurnOffDigit()
    {
        foreach (DigitSegment segment in digitSegments) {
            segment.ToggleOff();
        } 
    }

    public void ToggleAll()
    {
        foreach (DigitSegment segment in digitSegments)
        {
            if (segment.IsLit())
            {
                segment.ToggleOff();
            }
            else
            {
                segment.ToggleOn();
            }
        }
    }

    public void RotateBoundary(int steps, int direction)
    {
        string previousPattern = GetPattern();
        string newPattern = string.Empty;

        foreach (char character in previousPattern)
        {
            int previousIndex = CharToInt(character);

            int newIndex = previousIndex < boundarySegments
                ? Mod(previousIndex + steps * direction, boundarySegments)
                : previousIndex;

            newPattern += newIndex.ToString();
        }

        SetPattern(newPattern);
    }
    
    public void SwapWith(Digit other)
    {
        string tempPattern = GetPattern();
        SetPattern(other.GetPattern());
        other.SetPattern(tempPattern);
        
    }

    public void CopyFrom(Digit other)
    {
        SetPattern(other.GetPattern());
    }

    //Helper functions
    private static int CharToInt(char character)
    {
        return character - '0';
    }

    private static int Mod(int value, int modulus)
    {
        return (value % modulus + modulus) % modulus;
    }

    
}
