using UnityEngine;

namespace Effects.Modifiers
{
    [CreateAssetMenu(menuName = "Modifier/Percentage")]
    public class PercentageModifier : Modifier
    {
        [SerializeField] private float percentage;

        public override float ModifyValue(float value) => value * percentage;
        public override float RevertValue(float value) => value / percentage;
    }
}