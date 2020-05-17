using ScriptableService;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.LocationProviders;
using UnityUtils.ScaleProviders;

namespace TrainingSpecific.DynamicObjectController
{
    public class DynamicObstacleController : DynamicObjectController
    {
        [SerializeField] private LocationProvider locationProvider;
        [SerializeField] private ScaleProvider scaleProvider;
        [SerializeField] private SpawnPointValidator spawnPointValidator;
        [SerializeField] private bool ignoreSpawnPointCollision;

        [FormerlySerializedAs("controllerGameObject")] [FormerlySerializedAs("dynamicObstacle")] [SerializeField]
        protected GameObject controlledGameObject;

        protected override void PrepareObjectForTraining()
        {
            var controlledTransform = controlledGameObject.transform;
            controlledGameObject.SetActive(true);

            //I like how your code is getting increasingly dirtier, but you just DGAF anymore
            if (ignoreSpawnPointCollision)
            {
                ConfigureObstacle(controlledTransform);
            }
            else
            {
                do
                {
                    ConfigureObstacle(controlledTransform);
                } while (!spawnPointValidator.IsSpawnPointValid(
                    controlledTransform.position,
                    controlledTransform.localScale / 2f,
                    controlledTransform.rotation,
                    controlledGameObject
                ));
            }
        }

        private void ConfigureObstacle(Transform obstacleTransform)
        {
            obstacleTransform.position = locationProvider.ProvideLocation();
            obstacleTransform.localScale = scaleProvider.ProvideScale();
        }

        protected override void CleanUpObjectForTraining()
        {
            base.CleanUpObjectForTraining();
            controlledGameObject.SetActive(false);
        }
    }
}