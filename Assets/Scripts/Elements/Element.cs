using Common.Class;
using Common.Enum;
using Common.Event;
using Common.Interface;
using EventManagement;
using UnityEngine;

namespace Elements
{
    public abstract class Element : MonoBehaviour, IDynamicObjectOfInterest
    {
        protected IEventAggregator eventAggregator;

        public abstract AiInterestCategory InterestCategory { get; }
        public abstract Bounds Bounds { get; }

        protected virtual void OnEnable()
        {
            eventAggregator = EventAggregatorHolder.Instance;
            eventAggregator.Subscribe(this);

            eventAggregator.Publish(new DynamicObjectSpawnedEvent(this));
        }

        protected void OnDisable()
        {
            eventAggregator.Unsubscribe(this);
        }
    }
}