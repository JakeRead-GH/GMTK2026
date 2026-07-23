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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < digitSegments.Length; i++) { 
            InitialiseSegment(digitSegments[i]);
        }

        setPattern(valToDisplayMapping["7"]);

        print(-1 % 6);
        
        print(GetPattern());

        rotateBoundary(1, 1);

        //toggleAll();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitialiseSegment(DigitSegment segment) { 
        segment.SetLitColour(litColour);
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

    public void setPattern(string pattern)
    {
        turnOffDisplay();
        foreach (char chIdx in pattern) { 
            int segIdx = charToInt(chIdx);
            digitSegments[segIdx].ToggleOn();
        }
    }

    public void turnOffDisplay()
    {
        foreach (DigitSegment segment in digitSegments) {
            segment.ToggleOff();
        } 
    }

    public void toggleAll()
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

    public void rotateBoundary(int steps, int dir)
    {
        string newPattern = string.Empty;
        string prevPattern = GetPattern();
        int prevIdx;
        int newIdx;

        foreach (char chIdx in prevPattern)
        {
            prevIdx = charToInt(chIdx);


            newIdx = mod((prevIdx + steps * dir), boundarySegments);
            
            newPattern += newIdx.ToString();
        }

        setPattern(newPattern);

    }

    //Helper functions
    int charToInt(char ch)
    {
        return ch - '0';
    }

    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
