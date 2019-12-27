using Common.Class;
using EventManagement;
using UnityEngine;
using UnityUtils;
using UnityUtils.LocationProviders;

namespace TrainingSpecific
{
    public class DynamicObstacleController : MonoBehaviour, IHandle<SessionBeginState>
    {
        [SerializeField] private LocationProvider locationProvider;
        [SerializeField] private ActivityProvider activityProvider;
        [SerializeField] private GameObject dynamicObstacle;
        
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

        public void Handle(SessionBeginState @event)
        {
            dynamicObstacle.transform.position = locationProvider.ProvideLocation();
            dynamicObstacle.SetActive(activityProvider.ProvideIsActive());
        }
    }
}