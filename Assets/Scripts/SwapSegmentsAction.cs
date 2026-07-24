using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "SwapSegmentsAction",
    menuName = "Actions/Swap Segments"
)]
public sealed class SwapSegmentsAction : DigitAction
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

        return targets[0].SwapSegmentsWith(
            targets[1],
            segments
        );
    }
}