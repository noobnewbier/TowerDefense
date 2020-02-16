using ScriptableService;
using Terrain;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;
using UnityUtils.BooleanProviders;
using UnityUtils.LocationProviders;
using UnityUtils.ScaleProviders;

namespace TrainingSpecific
{
    public class IndividualDynamicObstacleController : MonoBehaviour
    {
        [FormerlySerializedAs("activityProvider")] [SerializeField] private BooleanProvider booleanProvider;
        [SerializeField] private DynamicObstacle dynamicObstacle;
        [SerializeField] private LocationProvider locationProvider;
        [SerializeField] private ScaleProvider scaleProvider;
        [SerializeField] private SpawnPointValidator spawnPointValidator;


        public void PrepareObjectForTraining()
        {
            var isActive = booleanProvider.ProvideBoolean();
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