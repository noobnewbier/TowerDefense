using UnityEngine;

namespace Effects.Modifiers
{
    [CreateAssetMenu(menuName = "Modifier/Constant")]
    public class ConstantModifier : Modifier
    {
        [SerializeField] private float amount;

        public override float ModifyValue(float value)
        {
            return value + amount;
        }

        public override float RevertValue(float value)
        {
            return value - amount;
        }

        /// <summary>
        ///     dirty, as you don't want to rewrite the hierarchy(well it doesn't take that long, but still)
        ///     shall the need arise re-write the hierarchy so Modifier no longer inherit from SO,
        ///     instead having a ModifierProvider that is a SO but provides a POCO of Modifier
        /// </summary>
        public static ConstantModifier CreateInstantHealthEffect(float amount)
        {
            var toReturn = CreateInstance<ConstantModifier>();
            toReturn.amount = amount;

            return toReturn;
        }
    }
}