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

    public bool IsLit() { 
        return isLit;
    }

    public void ToggleOn() {
        isLit = true;
        GetComponent<SpriteRenderer>().color = litColour;
    }

    public void ToggleOff() {
        isLit = false;
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void SetLit(bool isLit)
    {
        if(isLit) { 
            ToggleOn(); 
        }
        else { 
            ToggleOff(); 
        }
    }

    public void SetLitColour(Color colour) { 
        litColour = colour;
        SetLit(isLit);
    }

    public void SwapWith(DigitSegment other)
    {
        bool tempIsLit = IsLit();
        SetLit(other.IsLit());
        other.SetLit(tempIsLit);
    }
}
