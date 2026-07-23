using System.Collections.Generic;
using UnityEngine;

public abstract class DigitAction : ScriptableObject
{
    [Header("Card")]
    [SerializeField] private string actionName;
    [SerializeField] private Sprite cardIcon;

    [Header("Cursor")]
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 cursorHotspot;

    public string ActionName => actionName;
    public Sprite CardIcon => cardIcon;

    public Texture2D CursorTexture => cursorTexture;
    public Vector2 CursorHotspot => cursorHotspot;

    public abstract SegmentMask AffectedSegments { get; }
    public abstract int RequiredTargetCount { get; }

    public abstract bool TryApply(IReadOnlyList<Digit> targets);
}