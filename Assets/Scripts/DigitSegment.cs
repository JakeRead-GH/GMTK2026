using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DigitSegment : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color litColour;
    private bool isLit;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool IsLit()
    {
        return isLit;
    }

    public void Toggle()
    {
        SetLit(!isLit);
    }

    public void ToggleOn()
    {
        SetLit(true);
    }

    public void ToggleOff()
    {
        SetLit(false);
    }

    public void SetLit(bool shouldBeLit)
    {
        isLit = shouldBeLit;
        spriteRenderer.color = isLit ? litColour : Color.black;
    }

    public void SetLitColour(Color colour)
    {
        litColour = colour;
        SetLit(isLit);
    }

    public void SwapWith(DigitSegment other)
    {
        if (other == null)
        {
            return;
        }

        bool previousState = isLit;
        SetLit(other.IsLit());
        other.SetLit(previousState);
    }
}