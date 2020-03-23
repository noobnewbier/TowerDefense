using Common.Class;
using Common.Enum;
using Common.Interface;
using Common.Struct;
using EventManagement;
using UnityEngine;

namespace Elements
{
    //todo: we need to really rethink this whole inheritance hierarchy... It's giving me creeps in the back mate. Future me, good luck 
    public abstract class Element : MonoBehaviour, IDynamicObjectOfInterest
    {
        protected abstract InterestCategory Category { get; }

        protected IEventAggregator EventAggregator { get; private set; }

        public abstract Bounds Bounds { get; }

        public InterestedInformation InterestedInformation =>
            new InterestedInformation(Category, Bounds);

        public Transform ObjectTransform => transform;

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