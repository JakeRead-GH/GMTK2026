using UnityEngine;

public class Digit : MonoBehaviour
{
    [SerializeField] DigitSegment[] digitSegments;
    [SerializeField] Color displayColour;

    //sorry for my dogshit coding. tired as fuck. also just shit


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < digitSegments.Length; i++) { 
            InitialiseSegment(digitSegments[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitialiseSegment(DigitSegment segment) { 
        segment.SetLitColour(displayColour);
    }

    public DigitSegment GetSegment(int segmentNumber) { 
        return digitSegments[segmentNumber];
    }



}
