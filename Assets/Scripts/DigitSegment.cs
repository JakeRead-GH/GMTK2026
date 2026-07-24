using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DigitSegment : MonoBehaviour
{
    [Header("Lock")]
    [SerializeField]
    private SpriteRenderer lockOverlayRenderer;

    private SpriteRenderer spriteRenderer;

    private Color litColour;
    private Color defaultColour;
    private Color highlightColour;

    private bool isLit;
    private bool isHighlighted;
    private bool isLocked;

    private Material shaderMaterial;

    // Shader values
    private float targetFlickerStrength = 1f;
    private float currentFlickerStrength = 1f;
    private float flickerSpeed = 24f;

    public bool IsLit()
    {
        return isLit;
    }

    public bool IsLocked()
    {
        return isLocked;
    }

    private SpriteRenderer Renderer
    {
        get
        {
            if (spriteRenderer == null)
            {
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
        defaultColour.a = 0.4f;

        highlightColour = Color.white;

        UpdateLockOverlay();
    }

    private void Update()
    {
        if (isHighlighted)
        {
            ProcessHighlightSegment();
        }

        if (currentFlickerStrength != targetFlickerStrength)
        {
            UpdateShader();
        }
    }

    public bool Toggle()
    {
        return SetLit(!isLit);
    }

    public bool ToggleOn()
    {
        return SetLit(true);
    }

    public bool ToggleOff()
    {
        return SetLit(false);
    }

    public bool SetLit(bool shouldBeLit)
    {
        if (isLocked)
        {
            return false;
        }

        bool changed = isLit != shouldBeLit;

        isLit = shouldBeLit;
        UpdateColour();

        targetFlickerStrength = isLit ? 1f : 10f;

        return changed;
    }

    public void SetLitColour(Color colour)
    {
        litColour = colour;
        UpdateColour();
    }

    public void ToggleLock()
    {
        SetLocked(!isLocked);
    }

    public void SetLocked(bool locked)
    {
        isLocked = locked;
        UpdateLockOverlay();
    }

    private void UpdateLockOverlay()
    {
        if (lockOverlayRenderer != null)
        {
            lockOverlayRenderer.enabled = isLocked;
        }
    }

    public void SetHighlight(bool highlightState)
    {
        isHighlighted = highlightState;

        if (!isHighlighted)
        {
            UpdateColour();
        }
    }

    public void SwapWith(DigitSegment other)
    {
        if (other == null)
        {
            return;
        }

        bool previousState = isLit;
        bool otherPreviousState = other.IsLit();

        SetLit(otherPreviousState);
        other.SetLit(previousState);
    }

    private void UpdateColour()
    {
        Renderer.color = isLit
            ? litColour
            : defaultColour;
    }

    private void ProcessHighlightSegment()
    {
        const float highlightSpeed = 2f;
        const float highlightStrength = 0.7f;
        const float minimumHighlight = 0.3f;

        float mixValue =
            (Mathf.Abs(Mathf.Sin(Time.time * highlightSpeed))
             + minimumHighlight)
            * highlightStrength;

        Color currentColour = isLit
            ? Color.Lerp(litColour, highlightColour, mixValue)
            : Color.Lerp(defaultColour, highlightColour, mixValue);

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