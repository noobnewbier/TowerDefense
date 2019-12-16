using System.Collections.Generic;
using System.Linq;
using Common.Class;
using Common.Event;
using Common.Interface;
using EventManagement;
using UnityEngine;

namespace AgentAi.Manager
{
    public class ObjectsOfInterestTracker : MonoBehaviour, IHandle<IDynamicObjectDestroyedEvent>, IHandle<IDynamicObjectSpawnedEvent>
    {
        private IEventAggregator _eventAggregator;
        private IList<IDynamicObjectOfInterest> _dynamicObjects;

        // ReSharper disable once MemberCanBeMadeStatic.Global
        public IEnumerable<IStaticObjectOfInterest> StaticObjectOfInterests => FindObjectsOfType(typeof(MonoBehaviour)).OfType<IStaticObjectOfInterest>();
        public IEnumerable<IDynamicObjectOfInterest> DynamicObjectOfInterests => new List<IDynamicObjectOfInterest>(_dynamicObjects);

        public void Handle(IDynamicObjectDestroyedEvent @event)
        {
            _dynamicObjects.Remove(@event.DynamicObject);
        }

        public void Handle(IDynamicObjectSpawnedEvent @event)
        {
            _dynamicObjects.Add(@event.DynamicObject);
        }

        private void Awake()
        {
            _dynamicObjects = new List<IDynamicObjectOfInterest>();
        }

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;

            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}