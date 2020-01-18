using UnityEngine;

namespace UnityTechRaw.KartAndFPS.Assets.PostProcessing.Runtime.Attributes
{
    public sealed class MinAttribute : PropertyAttribute
    {
        public readonly float min;

        public MinAttribute(float min)
        {
            this.min = min;
        }
    }
}
