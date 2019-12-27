using Common.Class;
using EventManagement;
using TrainingSpecific;
using TrainingSpecific.Events;
using UnityEngine;

namespace ProjectSpecificUtils
{
    public class NextLevelEventPublisher : MonoBehaviour
    {
        private IEventAggregator _eventAggregator;
        private LevelTracker _levelTracker;

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _levelTracker = LevelTracker.Instance;
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 20), "To Next Level"))
            {
                _eventAggregator.Publish(new CurrentTrainingLevelEvent(_levelTracker.CurrentLevel + 1));
            }
        }
    }
}