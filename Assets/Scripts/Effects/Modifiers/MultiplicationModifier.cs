using UnityEngine;
using UnityEngine.Serialization;

namespace Effects.Modifiers
{
    [CreateAssetMenu(menuName = "Modifier/Multiplication")]
    public class MultiplicationModifier : Modifier
    {
        [FormerlySerializedAs("percentage")] [SerializeField] private float multiplicand;

        public override float ModifyValue(float value) => value * multiplicand;
        public override float RevertValue(float value) => value / multiplicand;
    }
}