using Common.Class;
using Common.Enum;
using Common.Interface;
using EventManagement;
using UnityEngine;

namespace Elements
{
    //todo: we need to really rethink this whole inheritance hierarchy... It's giving me creeps in the back mate. Future me, good luck 
    public abstract class Element : MonoBehaviour, IDynamicObjectOfInterest
    {
        protected IEventAggregator EventAggregator { get; private set; }

        public abstract AiInterestCategory InterestCategory { get; }
        public abstract Bounds Bounds { get; }
        public Transform DynamicObjectTransform => transform;

        protected virtual void OnEnable()
        {
            EventAggregator = EventAggregatorHolder.Instance;
            EventAggregator.Subscribe(this);
        }

        protected virtual void OnDisable()
        {
            EventAggregator.Unsubscribe(this);
        }
    }
}