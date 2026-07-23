using UnityEngine;

public class Digit : MonoBehaviour
{
    [SerializeField] DigitSegment[] digitSegments;
    [SerializeField] Color litColour;

    //sorry for my dogshit coding. tired as fuck. also just shit


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < digitSegments.Length; i++) { 
            InitialiseSegment(digitSegments[i]);
        }
        digitSegments[0].ToggleOn();
        digitSegments[1].ToggleOn();
        digitSegments[6].ToggleOn();
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



}
