using System.Collections.Generic;
using System.Linq;
using Common.Class;
using Common.Event;
using Common.Interface;
using EventManagement;
using Experimental;
using UnityEngine;

namespace AgentAi.Manager
{
    public class ObjectsOfInterestTracker : MonoBehaviour, IHandle<IDynamicObjectDestroyedEvent>, IHandle<IDynamicObjectSpawnedEvent>
    {
        [SerializeField] private DynamicObjectsSet dynamicObjectsSet;
        
        private IEventAggregator _eventAggregator;

        // ReSharper disable once MemberCanBeMadeStatic.Global
        public IEnumerable<IStaticObjectOfInterest> StaticObjectOfInterests => FindObjectsOfType(typeof(MonoBehaviour)).OfType<IStaticObjectOfInterest>();
        public IEnumerable<IDynamicObjectOfInterest> DynamicObjectOfInterests => new List<IDynamicObjectOfInterest>(dynamicObjectsSet.Items);

        public void Handle(IDynamicObjectDestroyedEvent @event)
        {
            dynamicObjectsSet.Items.Remove(@event.DynamicObject);
        }

        public void Handle(IDynamicObjectSpawnedEvent @event)
        {
            dynamicObjectsSet.Items.Add(@event.DynamicObject);
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