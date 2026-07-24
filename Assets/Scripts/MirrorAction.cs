using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "MirrorSegmentsAction",
    menuName = "Actions/Mirror Segments"
)]
public sealed class MirrorSegmentsAction : DigitAction
{
    [SerializeField]
    private MirrorAxis axis = MirrorAxis.Horizontal;

    [SerializeField]
    private SegmentMask segments = SegmentMask.All;

    public override SegmentMask AffectedSegments => segments;

    public override int RequiredTargetCount => 1;

    public override bool TryApply(
        IReadOnlyList<Digit> targets
    )
    {
        if (
            targets == null ||
            targets.Count != 1 ||
            targets[0] == null ||
            segments == SegmentMask.None
        ) {
            return false;
        }

        return targets[0].MirrorSegments(
            axis,
            segments
        );
    }
}