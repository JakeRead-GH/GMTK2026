using System;
using System.Collections.Generic;
using UnityEngine;

public class Digit : MonoBehaviour
{
    [SerializeField] DigitSegment[] digitSegments;
    [SerializeField] Color litColour;

    private static Dictionary<string, string> digitToValMapping = new Dictionary<string, string> 
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
        { "012356" , "9" },
        
        //Alternate value displays

        //Letters
        {"0345" , "C" },
        {"03456" , "E" },
        {"0456" , "F" },
        {"12456" , "H" },
        {"1234" , "J" },
        {"345" , "L" },
        {"01245" , "N" },
        {"01456" , "P" },
        {"045" , "R" },
        {"12345" , "U" },

        //Misc
        { "", " " },
        {"1245" , "11" },

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
        { "9", "012356" },
                
        //Alternate value displays

        //Letters
        { "C", "0345" },
        { "E", "03456" },
        { "F", "0456" },
        { "H", "12456" },
        { "J", "1234" },
        { "L", "345" },
        { "N", "01245" },
        { "P", "01456" },
        { "R", "045" },
        { "U", "12345" },

        //Misc
        { " ", "" },
        { "11", "1245" },
        { "-", "" }
    };

    //sorry for my dogshit coding. tired as fuck. also just shit
    
    private void Start()
    {
        foreach (DigitSegment segment in digitSegments) {
            segment.SetLitColour(litColour);
        }
    }

    public DigitSegment GetSegment(int segmentNumber) { 
        return digitSegments[segmentNumber];
    }

    public int GetNumOfSegments()
    {
        return digitSegments.Length;
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

    public void SetPatternFromValue(string value) {
        //takes a single character and displays it as a digit
        string pattern = valToDisplayMapping.GetValueOrDefault(value);
        if (pattern == null) { 
            return;
        }
        SetPattern(pattern);
    }

    public string GetDigitNumValue()
    {
        string segPattern = GetPattern();
        if (segPattern == "") {
            return "-";
        }
        
        string patternVal = digitToValMapping.GetValueOrDefault(GetPattern());        
        if (patternVal != null) {
            return patternVal;
        }

        return "?";
        
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

    private static readonly int[] BoundaryCycle =
    {
        0, 1, 2, 3, 4, 5
    };

    public bool RotateBoundary(int steps, int direction)
    {
        return RotateSegments(
            BoundaryCycle,
            steps * direction
        );
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
        
    public int SegmentCount => digitSegments.Length;

    public SegmentMask GetLitMask()
    {
        SegmentMask result = SegmentMask.None;

        int count = Mathf.Min(digitSegments.Length, 7);

        for (int index = 0; index < count; index++)
        {
            if (digitSegments[index].IsLit())
            {
                result |= (SegmentMask)(1 << index);
            }
        }

        return result;
    }

    public void SetLitMask(SegmentMask mask)
    {
        int count = Mathf.Min(digitSegments.Length, 7);

        for (int index = 0; index < count; index++)
        {
            SegmentMask flag = (SegmentMask)(1 << index);
            digitSegments[index].SetLit((mask & flag) != 0);
        }
    }

    public bool ToggleSegments(SegmentMask mask)
    {
        bool changedAny = false;

        int count = Mathf.Min(digitSegments.Length, 7);

        for (int index = 0; index < count; index++)
        {
            SegmentMask flag = (SegmentMask)(1 << index);

            if ((mask & flag) == 0)
            {
                continue;
            }

            digitSegments[index].Toggle();
            changedAny = true;
        }

        return changedAny;
    }

    public bool SwapSegmentsWith(Digit other, SegmentMask mask)
    {
        if (
            other == null ||
            other == this ||
            other.digitSegments.Length != digitSegments.Length
        )
        {
            return false;
        }

        bool swappedAny = false;

        int count = Mathf.Min(digitSegments.Length, 7);

        for (int index = 0; index < count; index++)
        {
            SegmentMask flag = (SegmentMask)(1 << index);

            if ((mask & flag) == 0)
            {
                continue;
            }

            digitSegments[index].SwapWith(other.digitSegments[index]);
            swappedAny = true;
        }

        return swappedAny;
    }

    public bool CopySegmentsFrom(Digit other, SegmentMask mask)
    {
        if (
            other == null ||
            other.digitSegments.Length != digitSegments.Length
        )
        {
            return false;
        }

        bool copiedAny = false;

        int count = Mathf.Min(digitSegments.Length, 7);

        for (int index = 0; index < count; index++)
        {
            SegmentMask flag = (SegmentMask)(1 << index);

            if ((mask & flag) == 0)
            {
                continue;
            }

            digitSegments[index].SetLit(
                other.digitSegments[index].IsLit()
            );

            copiedAny = true;
        }

        return copiedAny;
    }
    
    public bool RotateSegments(
        IReadOnlyList<int> cycle,
        int signedSteps
    )
    {
        if (!IsValidSegmentCycle(cycle))
        {
            return false;
        }

        int count = cycle.Count;
        int offset = Mod(signedSteps, count);

        if (offset == 0)
        {
            return false;
        }

        bool[] previousStates = new bool[count];

        for (int position = 0; position < count; position++)
        {
            int segmentIndex = cycle[position];
            previousStates[position] =
                digitSegments[segmentIndex].IsLit();
        }

        for (int sourcePosition = 0;
             sourcePosition < count;
             sourcePosition++)
        {
            int destinationPosition =
                Mod(sourcePosition + offset, count);

            int destinationSegment =
                cycle[destinationPosition];

            digitSegments[destinationSegment].SetLit(
                previousStates[sourcePosition]
            );
        }

        return true;
    }

    private bool IsValidSegmentCycle(IReadOnlyList<int> cycle)
    {
        if (cycle == null || cycle.Count < 2)
        {
            return false;
        }

        HashSet<int> seenIndices = new();

        foreach (int index in cycle)
        {
            if (
                index < 0 ||
                index >= digitSegments.Length ||
                !seenIndices.Add(index)
            )
            {
                return false;
            }
        }

        return true;
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
