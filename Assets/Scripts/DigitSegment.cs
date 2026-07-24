using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DigitSegment : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color litColour;
    private Color defaultColour;
    private Color hightlightColour;
    private bool isLit;
    private bool isHightlighted = false;

    // This fixes a race condition
    private SpriteRenderer Renderer
    {
        get {
            if (spriteRenderer == null) {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

            return spriteRenderer;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColour = Color.black;
        defaultColour.a = .4f;
        hightlightColour = Color.white;
    }

    private void Update() {
        if ((isHightlighted)) {
            ProcessHighlightSegment();
        }
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
        Renderer.color = isLit ? litColour : defaultColour;
    }

    public void SetLitColour(Color colour)
    {
        litColour = colour;
        SetLit(isLit);
    }

    public void SetHighlight(bool highlightState) { 
        isHightlighted = highlightState;
        if (!highlightState) {
            if (isLit) {
                Renderer.color = litColour;
            }
            else { 
                Renderer.color = defaultColour;
            }
        }
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

    public void ProcessHighlightSegment() {
        float highlightSpeed = 2f;
        float highlightStrength = .7f;
        float minHighlight = .3f;
        float mixValue = (Mathf.Abs(Mathf.Sin(Time.time*highlightSpeed))+minHighlight)* highlightStrength;
        Color currentColour = Color.Lerp(litColour, hightlightColour, mixValue);
        Renderer.color = currentColour;
    }
}