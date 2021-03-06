using System.Collections.Generic;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Elements.Units.Enemies;
using EventManagement;
using TrainingSpecific.Events;
using UnityEngine;

namespace Elements.Turret
{
    /// As a child of
    /// <see cref="Turret" />
    /// , when the parent get destroyed it's gone as well, so no need to handle
    /// <see cref="ForceResetEvent" />
    public class UnitDetector : Element, IHandle<EnemyDeadEvent>
    {
        private ITurretRepository _repository;

        [SerializeField] private TurretProvider provider;
        [SerializeField] private SphereCollider rangeCollider;

        public IList<Enemy> EnemiesInRange { get; private set; }

        public override AiInterestCategory InterestCategory => AiInterestCategory.TurretRange;
        public override Bounds Bounds => rangeCollider.bounds;

        public void Handle(EnemyDeadEvent @event)
        {
            EnemiesInRange.Remove(@event.Enemy);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _repository = provider.GetRepository();
            EnemiesInRange = new List<Enemy>();
            rangeCollider.radius = _repository.DetectionRange;
            rangeCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy)) EnemiesInRange.Add(other.gameObject.GetComponent<Enemy>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy)) EnemiesInRange.Remove(other.GetComponent<Enemy>());
        }
    }
}