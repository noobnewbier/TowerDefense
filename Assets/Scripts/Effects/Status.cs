using Elements.Units.UnitCommon;
using UnityEngine;

namespace Effects
{
    public abstract class Status : ScriptableObject
    {
        public abstract void ApplyEffect(UnitData data);
    }
}