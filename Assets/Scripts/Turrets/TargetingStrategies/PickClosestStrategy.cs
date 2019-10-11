using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;

namespace Turrets.TargetingStrategies
{
    [CreateAssetMenu(menuName = "TurretStrategy/PickClosest")]
    public class PickClosestStrategy : ScriptableObject, ITargetingStrategy
    {
        public Vector3? ChooseTarget(Transform turretTransform, IEnumerable<IEnemy> enemies)
        {
            var minDistance = float.MaxValue;
            IEnemy currentClosest = null;
            foreach (var enemy in enemies)
            {
                var distance = Vector3.Distance(enemy.Transform.position, turretTransform.position);
                if (!(distance < minDistance)) continue;
                
                currentClosest = enemy;
                minDistance = distance;
            }

            return currentClosest?.Transform.position;
        }
    }
}