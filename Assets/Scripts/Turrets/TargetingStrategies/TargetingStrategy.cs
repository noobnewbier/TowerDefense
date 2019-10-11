using System.Collections.Generic;
using Enemies;
using JetBrains.Annotations;
using UnityEngine;

namespace Turrets.TargetingStrategies
{
    public abstract class TargetingStrategy : ScriptableObject
    {
        //return null if list is empty?
        [CanBeNull]
        public abstract IEnemy ChooseTarget(Transform turretTransform, IEnumerable<IEnemy> enemies);
    }
}