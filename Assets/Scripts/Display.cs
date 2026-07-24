using UnityEngine;

public class Display : MonoBehaviour
{
    [SerializeField] Digit[] digits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        digits[0].SetPattern("12");
        digits[1].SetPattern("01346");
        digits[2].SetPattern("0123456");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
