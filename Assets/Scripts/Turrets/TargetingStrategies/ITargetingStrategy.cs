using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Turrets.TargetingStrategies
{
    public interface ITargetingStrategy
    {
        //return null if list is empty?
        Vector3? ChooseTarget(Transform turretTransform, IEnumerable<IEnemy> enemies);
    }
}