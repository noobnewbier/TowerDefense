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

        [FormerlySerializedAs("controllerGameObject")] [FormerlySerializedAs("dynamicObstacle")] [SerializeField]
        protected GameObject controlledGameObject;

        protected override void PrepareObjectForTraining()
        {
            var controlledTransform = controlledGameObject.transform;
            controlledGameObject.SetActive(true);
            
            do
            {
                controlledTransform.position = locationProvider.ProvideLocation();
                controlledTransform.localScale = scaleProvider.ProvideScale();
            } while (!spawnPointValidator.IsSpawnPointValid(
                controlledTransform.position,
                controlledTransform.localScale / 2f,
                controlledTransform.rotation,
                controlledGameObject
            ));
        }

        protected override void CleanUpObjectForTraining()
        {
            base.CleanUpObjectForTraining();
            controlledGameObject.SetActive(false);
        }
    }
}