using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "ToggleSegmentsAction",
    menuName = "Actions/Toggle Segments"
)]
public sealed class ToggleSegmentsAction : DigitAction
{
    [SerializeField]
    private SegmentMask segments = SegmentMask.Boundary;

    public override SegmentMask AffectedSegments => segments;

    public override int RequiredTargetCount => 1;

    public override bool TryApply(IReadOnlyList<Digit> targets)
    {
        if (
            targets == null ||
            targets.Count != 1 ||
            targets[0] == null ||
            segments == SegmentMask.None
        ) {
            return false;
        }

        return targets[0].ToggleSegments(segments);
    }
}