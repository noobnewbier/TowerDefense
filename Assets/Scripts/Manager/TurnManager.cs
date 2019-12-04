using Common.Class;
using Common.Event;
using EventManagement;
using UnityEngine;

namespace Manager
{
    public class TurnManager : MonoBehaviour, IHandle<PreparationBeginsEvent>
    {
        private IEventAggregator _eventAggregator;
        private bool _finishedPreparation;
        private float _timer;
        [SerializeField] private float prepareTime;

        public void Handle(PreparationBeginsEvent @event)
        {
            _timer = 0f;
            _finishedPreparation = false;
        }

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

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}