using System.Linq;
using Effects.Modifiers;
using UnityEngine;
using UnityUtils.FloatProvider;

namespace TrainingSpecific.Providers
{
    public class ApplyModifierFloatProvider : FloatProvider
    {
        [SerializeField] private FloatProvider floatProvider;
        [SerializeField] private Modifier[] modifiers;

        public override float ProvideFloat()
        {
            var value = floatProvider.ProvideFloat();
            return modifiers.Aggregate(value, (current, modifier) => modifier.ModifyValue(current));
        }
    }
}