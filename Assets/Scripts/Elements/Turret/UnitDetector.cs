using System.Collections.Generic;
using Common.Class;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Elements.Units.UnitCommon;
using EventManagement;
using UnityEngine;

namespace Elements.Turret
{
    public class UnitDetector : MonoBehaviour, IHandle<EnemyDeadEvent>, IObjectOfInterest
    {
        private IEventAggregator _eventAggregator;
        [SerializeField] private SphereCollider rangeCollider;
        public IList<Unit> EnemiesInRange { get; private set; }

        public void Handle(EnemyDeadEvent @event)
        {
            EnemiesInRange.Remove(@event.Unit);
        }

        public AiInterestCategory InterestCategory => AiInterestCategory.TurretRange;
        public Bounds Bounds => rangeCollider.bounds;

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void Initialize(TurretData data)
        {
            EnemiesInRange = new List<Unit>();
            rangeCollider.radius = data.DetectionRange;
            rangeCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy))
            {
                EnemiesInRange.Add(other.gameObject.GetComponent<Unit>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy))
            {
                EnemiesInRange.Remove(other.GetComponent<Unit>());
            }
        }
    }
}