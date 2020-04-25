using System.Collections.Generic;
using System.Linq;
using Common.Event;
using Common.Interface;
using EventManagement;
using EventManagement.Providers;
using Experimental;
using UnityEngine;

namespace AgentAi.Manager
{
    [CreateAssetMenu(menuName = "ScriptableService/ObjectOfInterestTracker")]
    public class ObjectsOfInterestTracker : ScriptableObject, IHandle<IDynamicObjectDestroyedEvent>,
                                            IHandle<IDynamicObjectSpawnedEvent>
    {
        private IEventAggregator _eventAggregator;
        [SerializeField] private DynamicObjectsSet dynamicObjectsSet;
        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;


        // ReSharper disable once MemberCanBeMadeStatic.Global
        public IEnumerable<IStaticObjectOfInterest> StaticObjectOfInterests =>
            FindObjectsOfType(typeof(MonoBehaviour)).OfType<IStaticObjectOfInterest>();

        public IEnumerable<IDynamicObjectOfInterest> DynamicObjectOfInterests =>
            new List<IDynamicObjectOfInterest>(dynamicObjectsSet.Items);

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
            _eventAggregator = eventAggregatorProvider.ProvideEventAggregator();

            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}