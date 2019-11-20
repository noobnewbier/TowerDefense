using System.Collections.Generic;
using Common.Class;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
using EventManagement;
using Units.UnitCommon;
using UnityEngine;

namespace Turret
{
    public class UnitDetector : MonoBehaviour, IHandle<EnemyDeadEvent>, IObjectOfInterest
    {
        [SerializeField] private SphereCollider rangeCollider;
        public IList<Unit> EnemiesInRange { get; private set; }
        private IEventAggregator _eventAggregator;

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

        public void Handle(EnemyDeadEvent @event)
        {
            EnemiesInRange.Remove(@event.Unit);
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

        public AiInterestedObjectType InterestedObjectType => AiInterestedObjectType.TurretRange;
        public Bounds Bounds => rangeCollider.bounds;
    }
}