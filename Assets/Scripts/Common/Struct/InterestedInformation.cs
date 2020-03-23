using System;
using Common.Enum;
using UnityEngine;

namespace Common.Struct
{
    [Serializable]
    public struct InterestedInformation
    {
        [SerializeField] private InterestCategory category;
        [SerializeField] private Bounds bounds;

        public InterestedInformation(InterestCategory category, Bounds bounds)
        {
            this.category = category;
            this.bounds = bounds;
        }

        public InterestCategory Category => category;

        public Bounds Bounds => bounds;
    }
}