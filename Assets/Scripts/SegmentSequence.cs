using System;
using System.Collections.Generic;
using UnityEngine;

//   0
// 5   1
//   6
// 4   2
//   3

// Ex. Clockwise top four rotation would have sequence 0, 1, 6, 5 

[Serializable]
public sealed class SegmentSequence
{
    [Tooltip("Segment indices in rotation order.")]
    [SerializeField] private int[] indices = Array.Empty<int>();

    public IReadOnlyList<int> Indices => indices;

    public SegmentMask Mask
    {
        get
        {
            SegmentMask mask = SegmentMask.None;

            foreach (int index in indices)
            {
                if (index >= 0 && index < 7)
                {
                    mask |= (SegmentMask)(1 << index);
                }
            }

            return mask;
        }
    }
}