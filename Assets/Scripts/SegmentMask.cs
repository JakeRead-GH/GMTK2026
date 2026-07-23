using System;

[Flags]
public enum SegmentMask
{
    None = 0,

    Segment0 = 1 << 0,
    Segment1 = 1 << 1,
    Segment2 = 1 << 2,
    Segment3 = 1 << 3,
    Segment4 = 1 << 4,
    Segment5 = 1 << 5,
    Segment6 = 1 << 6,

    Boundary =
        Segment0 |
        Segment1 |
        Segment2 |
        Segment3 |
        Segment4 |
        Segment5,
    
    TopFour =
        Segment0 | Segment1 | Segment5 | Segment6,

    BottomFour =
        Segment2 | Segment3 | Segment4 | Segment6,

    MiddleColumn =
        Segment0 | Segment3 | Segment6,

    All = Boundary | Segment6
}