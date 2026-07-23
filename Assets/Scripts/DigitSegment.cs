using UnityEngine;


//[System.Serializable]
public class DigitSegment : MonoBehaviour
{
    Color litColour;
    bool isLit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLit(bool _isLit) { 
        isLit = _isLit;
        GetComponent<SpriteRenderer>().color = litColour;
    }

    public bool IsLit() { 
        return isLit;
    }

    public void SetLitColour(Color colour) { 
        litColour = colour;
    }
}
