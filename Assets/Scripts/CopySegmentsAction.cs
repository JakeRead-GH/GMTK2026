using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "CopySegmentsAction",
    menuName = "Actions/Copy Segments"
)]
public sealed class CopySegmentsAction : DigitAction
{
    [SerializeField]
    private SegmentMask segments = SegmentMask.All;

    public override SegmentMask AffectedSegments => segments;

    public override int RequiredTargetCount => 2;

    public override bool TryApply(
        IReadOnlyList<Digit> targets
    )
    {
        if (
            targets == null ||
            targets.Count != 2 ||
            targets[0] == null ||
            targets[1] == null ||
            targets[0] == targets[1] ||
            segments == SegmentMask.None
        )
        {
            return false;
        }

        Digit source = targets[0];
        Digit destination = targets[1];

        return destination.CopySegmentsFrom(
            source,
            segments
        );
    }
}