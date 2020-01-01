using System.Collections;
using Common.Class;
using EventManagement;
using TrainingSpecific.Events;
using UnityEngine;

namespace TrainingSpecific
{
    public class DynamicObstacleHandler : MonoBehaviour, IHandle<HandleDynamicObstacleEvent>
    {
        private static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
        
        [SerializeField] private IndividualDynamicObstacleController[] controllers;
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

        public void Handle(HandleDynamicObstacleEvent @event)
        {
            StartCoroutine(HandleDynamicObjectsCoroutine());
        }

        private IEnumerator HandleDynamicObjectsCoroutine()
        {
            //we need to handle one at a frame, otherwise spawnpoint validator could not tell if they are overlapping
            foreach (var controller in controllers)
            {
                controller.PrepareObjectForTraining();
                yield return WaitForFixedUpdate;
            }

            _eventAggregator.Publish(new DynamicObstacleHandledEvent());
        }
    }
}