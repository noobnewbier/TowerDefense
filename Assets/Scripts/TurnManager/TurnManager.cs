using System;
using Common;
using Common.Class;
using Common.Event;
using EventManagement;
using UnityEngine;

namespace TurnManager
{
    public class TurnManager : MonoBehaviour, IHandle<PreparationBeginsEvent>
    {
        [SerializeField] private float prepareTime;
        private float _timer;
        private bool _finishedPreparation;
        private IEventAggregator _eventAggregator;

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);
        }

        private void Update()
        {
            if (!_finishedPreparation)
            {
                _timer += Time.deltaTime;

                if (_timer >= prepareTime)
                {
                    _finishedPreparation = true;
                    
                    _eventAggregator.Publish(new SetupTimesUpEvent());
                }
            }
        }

        public void Handle(PreparationBeginsEvent @event)
        {
            _timer = 0f;
            _finishedPreparation = false;
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}