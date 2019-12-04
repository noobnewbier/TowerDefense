using System.Collections.Generic;
using Common.Class;
using Common.Event;
using Elements.Turret;
using EventManagement;
using UnityEngine;

namespace Manager
{
    public class SurvivingTurretTracker : MonoBehaviour, IHandle<TurretSpawnedEvent>, IHandle<TurretDestroyedEvent>
    {
        public IList<Turret> TurretsInField { get; private set; }

        private IEventAggregator _eventAggregator;

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            TurretsInField = new List<Turret>();
            
            _eventAggregator.Subscribe(this);
        }

        public void Handle(TurretSpawnedEvent @event)
        {
            TurretsInField.Add(@event.Turret);
        }

        public void Handle(TurretDestroyedEvent @event)
        {
            TurretsInField.Remove(@event.Turret);
        }
    }
}