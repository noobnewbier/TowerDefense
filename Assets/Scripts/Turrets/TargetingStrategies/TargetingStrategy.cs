using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Turrets.TargetingStrategies
{
    public abstract class TargetingStrategy : ScriptableObject
    {
        //return null if list is empty?
        public abstract Vector3? ChooseTarget(Transform turretTransform, IEnumerable<IEnemy> enemies);
    }
}