using System.Collections.Generic;
using Units.UnitCommon;
using UnityEngine;

namespace Turrets.TargetingStrategies
{
    [CreateAssetMenu(menuName = "TurretStrategy/PickClosest")]
    public class PickClosestStrategy : TargetingStrategy
    {
        public override Unit ChooseTarget(Transform turretTransform, IEnumerable<Unit> enemies)
        {
            var minDistance = float.MaxValue;
            Unit currentClosest = null;
            foreach (var enemy in enemies)
            {
                var distance = Vector3.Distance(enemy.Transform.position, turretTransform.position);
                if (!(distance < minDistance))
                {
                    continue;
                }

                currentClosest = enemy;
                minDistance = distance;
            }

            return currentClosest;
        }
    }
}