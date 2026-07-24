using System.Collections.Generic;
using UnityEngine;

public enum RotationRegion
{
    Boundary,
    TopFour,
    BottomFour
}

[CreateAssetMenu(
    fileName = "RotateSegmentsAction",
    menuName = "Actions/Rotate Segments"
)]
public sealed class RotateSegmentsAction : DigitAction
{
    // Segment layout:
    //
    //      0
    //   5     1
    //      6
    //   4     2
    //      3

    private static readonly int[] BoundaryCycle =
    {
        0, 1, 2, 3, 4, 5
    };

    private static readonly int[] TopFourCycle =
    {
        0, 1, 6, 5
    };

    private static readonly int[] BottomFourCycle =
    {
        6, 2, 3, 4
    };

    [SerializeField]
    private RotationRegion region = RotationRegion.Boundary;

    [SerializeField]
    private bool clockwise = true;

    [SerializeField, Min(1)]
    private int steps = 1;

    public override int RequiredTargetCount => 1;

    public override SegmentMask AffectedSegments =>
        region switch {
            RotationRegion.Boundary =>
                SegmentMask.Boundary,

            RotationRegion.TopFour =>
                SegmentMask.TopFour,

            RotationRegion.BottomFour =>
                SegmentMask.BottomFour,

            _ => SegmentMask.None
        };

    private IReadOnlyList<int> Cycle =>
        region switch {
            RotationRegion.Boundary =>
                BoundaryCycle,

            RotationRegion.TopFour =>
                TopFourCycle,

            RotationRegion.BottomFour =>
                BottomFourCycle,

            _ => BoundaryCycle
        };

    public override bool TryApply(
        IReadOnlyList<Digit> targets
    ) {
        if (
            targets == null ||
            targets.Count != 1 ||
            targets[0] == null
        )
        {
            return false;
        }

        int signedSteps = clockwise
            ? steps
            : -steps;

        return targets[0].RotateSegments(
            Cycle,
            signedSteps
        );
    }
}