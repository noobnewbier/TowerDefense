using Movement.InputSource;
using UnityEngine;
using UnityUtils;
using UnityUtils.BooleanProviders;

namespace Elements.Units.Enemies.Suicidal
{
    public class HasForwardInput : BooleanProvider
    {
        [SerializeField] private MovementInputSource inputSource;
        
        public override bool ProvideBoolean()
        {
            return !FloatUtil.NearlyEqual(inputSource.Vertical(), 0f);
        }
    }
}