using System.Collections.Generic;
using Elements.Units.UnitCommon;
using JetBrains.Annotations;
using UnityEngine;

namespace Elements.Turret.TargetingPicking
{
    public abstract class TargetingStrategy : ScriptableObject
    {
        //return null if list is empty?
        [CanBeNull]
        public abstract TargetInformation? ChooseTarget(Transform turretTransform, IEnumerable<TargetInformation> targetInfo);
    }
}