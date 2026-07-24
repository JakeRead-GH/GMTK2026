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
    private Material shaderMaterial;

    //shader values
    private float targetFlickerStrength = 1f;
    private float currentFlickerStrength = 1f;
    private float flickerSpeed = 24f;

    // This fixes a race condition
    private SpriteRenderer Renderer
    {
        get {
            if (spriteRenderer == null) {
                spriteRenderer = GetComponent<SpriteRenderer>();
                shaderMaterial = spriteRenderer.material;
            }

            return spriteRenderer;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shaderMaterial = spriteRenderer.material;
        defaultColour = Color.black;
        defaultColour.a = .4f;
        hightlightColour = Color.white;
    }

    private void Update() {
        if ((isHightlighted)) {
            ProcessHighlightSegment();
        }
        if (currentFlickerStrength != targetFlickerStrength) { 
            UpdateShader();
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
        UpdateColour();
        targetFlickerStrength = isLit ? 1f : 10f;
    }

    void UpdateColour() {
        if (currentFlickerStrength != targetFlickerStrength) { 
            //return;
        }
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
                UpdateColour();
            }
            else {
                UpdateColour();
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
        Color currentColour;
        if (isLit) {
            currentColour = Color.Lerp(litColour, hightlightColour, mixValue);
        }
        else {
            currentColour = Color.Lerp(defaultColour, hightlightColour, mixValue);
        }
        Renderer.color = currentColour;
    }

    void UpdateShader() { 
        float minimumDelta = 1.0f;
        if ((currentFlickerStrength - targetFlickerStrength)*(currentFlickerStrength - targetFlickerStrength) < minimumDelta*minimumDelta) {
            currentFlickerStrength = targetFlickerStrength;
            shaderMaterial.SetFloat("_Flicker_Out_Fader", currentFlickerStrength);
            UpdateColour();
            return;
        }
        if (currentFlickerStrength > targetFlickerStrength) { 
            currentFlickerStrength -= flickerSpeed*Time.deltaTime;
        }
        if (currentFlickerStrength < targetFlickerStrength) {
            currentFlickerStrength += flickerSpeed * Time.deltaTime;
        }
        shaderMaterial.SetFloat("_Flicker_Out_Fader", currentFlickerStrength);

    }
}