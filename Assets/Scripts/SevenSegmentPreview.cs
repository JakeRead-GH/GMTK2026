using UnityEngine;
using UnityEngine.UI;

public sealed class SevenSegmentPreview : MonoBehaviour
{
    [Tooltip("Order these as segment 0 through segment 6.")]
    [SerializeField] private Image[] segments = new Image[7];

    [SerializeField] private Color affectedColour = Color.white;
    [SerializeField] private Color unaffectedColour =
        new Color(1f, 1f, 1f, 0.15f);

    public void SetMask(SegmentMask mask)
    {
        if (segments == null || segments.Length != 7) {
            Debug.LogError(
                $"{name} must have exactly seven segment images.",
                this
            );

            return;
        }

        for (int index = 0; index < segments.Length; index++) {
            if (segments[index] == null) {
                continue;
            }

            SegmentMask segmentFlag = (SegmentMask)(1 << index);
            bool isAffected = (mask & segmentFlag) != 0;

            segments[index].color = isAffected
                ? affectedColour
                : unaffectedColour;
        }
    }
}