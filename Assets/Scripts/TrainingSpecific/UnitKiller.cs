using System;
using Common.Class;
using EventManagement;
using UnityEngine;

namespace TrainingSpecific
{
    public class UnitKiller : MonoBehaviour, IHandle<ForceResetEvent>
    {
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

        public void Handle(ForceResetEvent @event)
        {
        }
    }
}