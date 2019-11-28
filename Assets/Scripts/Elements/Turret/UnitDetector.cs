using System.Collections.Generic;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Elements.Units.UnitCommon;
using EventManagement;
using TrainingSpecific;
using UnityEngine;

namespace Elements.Turret
{
    /// As a child of <see cref="Turret"/>, when the parent get destroyed it's gone as well, so no need to handle <see cref="ForceResetEvent"/> 
    public class UnitDetector : Element, IHandle<EnemyDeadEvent>
    {
        [SerializeField] private SphereCollider rangeCollider;
        public IList<Unit> EnemiesInRange { get; private set; }

        public override AiInterestCategory InterestCategory => AiInterestCategory.TurretRange;
        public override Bounds Bounds => rangeCollider.bounds;

        public void Handle(EnemyDeadEvent @event)
        {
            EnemiesInRange.Remove(@event.Unit);
        }

        public void Initialize(TurretData data)
        {
            EnemiesInRange = new List<Unit>();
            rangeCollider.radius = data.DetectionRange;
            rangeCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy)) EnemiesInRange.Add(other.gameObject.GetComponent<Unit>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy)) EnemiesInRange.Remove(other.GetComponent<Unit>());
        }
    }
}