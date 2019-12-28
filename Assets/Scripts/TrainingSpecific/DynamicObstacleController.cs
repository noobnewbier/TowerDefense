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
    public class DynamicObstacleController : MonoBehaviour, IHandle<HandleDynamicObstacleEvent>
    {
        [SerializeField] private LocationProvider locationProvider;
        [SerializeField] private ActivityProvider activityProvider;
        [SerializeField] private ScaleProvider scaleProvider;
        [SerializeField] private DynamicObstacle dynamicObstacle;
        [SerializeField] private SpawnPointValidator spawnPointValidator;
        
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
            var isActive = activityProvider.ProvideIsActive();
            dynamicObstacle.gameObject.SetActive(isActive);
            if (isActive)
            {
                do
                {
                    dynamicObstacle.transform.position = locationProvider.ProvideLocation();
                    dynamicObstacle.transform.localScale = scaleProvider.ProvideScale();
                } while (!spawnPointValidator.IsSpawnPointValid(
                    dynamicObstacle.transform.position,
                    dynamicObstacle.transform.localScale / 2f,
                    dynamicObstacle.transform.rotation,
                    dynamicObstacle.gameObject
                ));
            }
            
            _eventAggregator.Publish(new DynamicObstacleHandledEvent());
        }
    }
}