using Elements.Units.Enemies;
using UnityEngine;

namespace Elements.Turret
{
    public struct TargetInformation
    {
        public TargetInformation(Enemy enemy, Collider collider)
        {
            Enemy = enemy;
            Collider = collider;
        }

        public Enemy Enemy { get; }
        public Collider Collider { get; }
    }
}