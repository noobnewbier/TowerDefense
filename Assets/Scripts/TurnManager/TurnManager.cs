using System;
using Common;
using Common.Events;
using EventManagement;
using UnityEngine;

namespace TurnManager
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private float prepareTime;
        private float _timer;
        private bool _finishedPreparation;
        private IEventAggregator _eventAggregator;

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
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
    }
}