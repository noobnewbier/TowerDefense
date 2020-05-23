using System.Collections.Generic;
using UnityEngine;

namespace Elements.Turret.TargetingPicking
{
    [CreateAssetMenu(menuName = "TurretStrategy/PickClosest")]
    public class PickClosestStrategy : TargetingStrategy
    {
        public override TargetInformation? ChooseTarget(Transform turretTransform,
                                                        IEnumerable<TargetInformation> targetInfo)
        {
            var minDistance = float.MaxValue;
            TargetInformation? currentClosestTargetInfo = null;
            foreach (var info in targetInfo)
            {
                var distance = Vector3.Distance(info.Enemy.ObjectTransform.position, turretTransform.position);
                if (!(distance < minDistance)) continue;

                currentClosestTargetInfo = info;
                minDistance = distance;
            }

            return currentClosestTargetInfo;
        }
    }
}