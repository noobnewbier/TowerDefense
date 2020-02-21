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

        public IEnumerable<Enemy> VisibleEnemies => InRangeEnemies.Where(e => IsTargetVisible(e.transform));

        private IList<Enemy> InRangeEnemies { get; set; }
        public override AiInterestCategory InterestCategory => AiInterestCategory.TurretRange;
        public override Bounds Bounds => rangeCollider.bounds;

        public void Handle(EnemyDeadEvent @event)
        {
            InRangeEnemies.Remove(@event.Enemy);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _repository = provider.GetRepository();
            _layerMaskToIgnore = ~((1 << LayerMask.NameToLayer(LayerNames.Turret)) |
                                 (1 << LayerMask.NameToLayer(LayerNames.PlayerDamageTaker)));
            InRangeEnemies = new List<Enemy>();
            rangeCollider.radius = _repository.DetectionRange;
            rangeCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy)) InRangeEnemies.Add(other.gameObject.GetComponent<Enemy>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy)) InRangeEnemies.Remove(other.GetComponent<Enemy>());
        }

        private bool IsTargetVisible(Transform targetTransform)
        {
            var bulletSpawnpointPosition = bulletSpawnpoint.position;
            var targetPosition = targetTransform.position;
            return Physics.Raycast(
                       bulletSpawnpointPosition,
                       targetPosition - bulletSpawnpointPosition,
                       out var hit,
                       Vector3.Distance(targetPosition, bulletSpawnpointPosition),
                       _layerMaskToIgnore
                   ) && hit.collider.CompareTag(ObjectTags.Enemy);
        }
    }
}