using Elements.Units.UnitCommon;
using UnityEngine;

namespace Effects
{
    public abstract class Effect : ScriptableObject
    {
        public abstract void ApplyEffect(UnitData data);
    }
}