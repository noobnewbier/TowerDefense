using System.Collections.Generic;
using System.Linq;
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
        private int _layerMaskToIgnore;
        private ITurretRepository _repository;

        [SerializeField] private Transform bulletSpawnpoint;
        [SerializeField] private TurretProvider provider;
        [SerializeField] private SphereCollider rangeCollider;

        public IEnumerable<TargetInformation> VisibleTargetsInfo =>
            InRangeEnemies.Where(IsTargetVisible);

        private List<TargetInformation> InRangeEnemies { get; set; }
        protected override InterestCategory Category => InterestCategory.TurretRange;
        public override Bounds Bounds => rangeCollider.bounds;

        public void Handle(EnemyDeadEvent @event)
        {
            InRangeEnemies.RemoveAll(i => i.Enemy == @event.Enemy);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _repository = provider.GetRepository();
            _layerMaskToIgnore = ~((1 << LayerMask.NameToLayer(LayerNames.Turret)) |
                                   (1 << LayerMask.NameToLayer(LayerNames.PlayerDamageTaker)));
            InRangeEnemies = new List<TargetInformation>();
            rangeCollider.radius = _repository.DetectionRange;
            rangeCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy))
                InRangeEnemies.Add(new TargetInformation(other.gameObject.GetComponent<Enemy>(), other));
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy))
                InRangeEnemies.RemoveAll(i => i.Enemy == other.GetComponent<Enemy>());
        }

        private bool IsTargetVisible(TargetInformation targetInformation)
        {
            var bulletSpawnpointPosition = bulletSpawnpoint.position;
            var targetPosition = targetInformation.Collider.bounds.center;

            return Physics.Raycast(
                       bulletSpawnpointPosition,
                       targetPosition - bulletSpawnpointPosition,
                       out var hit,
                       Vector3.Distance(targetPosition, bulletSpawnpointPosition),
                       _layerMaskToIgnore
                   ) && hit.collider.gameObject.layer == LayerMask.NameToLayer(LayerNames.AiDamageTaker);
        }
    }
}