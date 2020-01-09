using UnityEngine;

namespace Effects.Modifiers
{
    [CreateAssetMenu(menuName = "Modifier/Constant")]
    public class ConstantModifier : Modifier
    {
        [SerializeField] private float amount;

        public override float ModifyValue(float value) => value + amount;
        public override float RevertValue(float value) => value - amount;
    }
}