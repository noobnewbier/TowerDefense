using System.Collections.Generic;
using JetBrains.Annotations;
using Units.UnitCommon;
using UnityEngine;

namespace Turrets.TargetingStrategies
{
    public abstract class TargetingStrategy : ScriptableObject
    {
        //return null if list is empty?
        [CanBeNull]
        public abstract Unit ChooseTarget(Transform turretTransform, IEnumerable<Unit> enemies);
    }
}