using Elements.Units.UnitCommon;
using UnityEngine;

namespace Effects
{
    public abstract class Bonus : ScriptableObject
    {
        public abstract void ApplyEffect(UnitData data);
    }
}