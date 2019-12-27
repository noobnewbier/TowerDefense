using Common.Class;
using EventManagement;
using ScriptableService;
using Terrain;
using TrainingSpecific.Events;
using UnityEngine;
using UnityUtils;
using UnityUtils.LocationProviders;
using UnityUtils.ScaleProviders;

namespace TrainingSpecific
{
    public class DynamicObstacleController : MonoBehaviour, IHandle<SessionBeginEvent>
    {
        [SerializeField] private LocationProvider locationProvider;
        [SerializeField] private ActivityProvider activityProvider;
        [SerializeField] private ScaleProvider scaleProvider;
        [SerializeField] private DynamicObstacle dynamicObstacle;

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

        public void Handle(SessionBeginEvent @event)
        {
            dynamicObstacle.gameObject.SetActive(activityProvider.ProvideIsActive());
            do
            {
                dynamicObstacle.transform.position = locationProvider.ProvideLocation();
                dynamicObstacle.transform.localScale = scaleProvider.ProvideScale();
            } while (!SpawnPointValidator.IsSpawnPointValid(dynamicObstacle.Bounds.center, dynamicObstacle.Bounds.extents, dynamicObstacle.transform.rotation));
        }
    }
}