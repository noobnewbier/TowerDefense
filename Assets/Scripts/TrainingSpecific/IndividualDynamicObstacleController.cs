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
    public class IndividualDynamicObstacleController : MonoBehaviour
    {
        [SerializeField] private LocationProvider locationProvider;
        [SerializeField] private ActivityProvider activityProvider;
        [SerializeField] private ScaleProvider scaleProvider;
        [SerializeField] private DynamicObstacle dynamicObstacle;
        [SerializeField] private SpawnPointValidator spawnPointValidator;
        
        
        public void PrepareObjectForTraining()
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
        }
    }
}