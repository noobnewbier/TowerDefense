using ScriptableService;
using UnityEngine;
using UnityUtils.FloatProvider;
using UnityUtils.LocationProviders;

namespace TrainingSpecific.DynamicObjectController
{
    /// <summary>
    ///     Another dirty bastard, but we don't have time....
    /// </summary>
    public class ConstantDamageAreaController : DynamicObjectController
    {
        [SerializeField] private ConstantDamageArea constantDamageArea;
        [SerializeField] private LocationProvider locationProvider;
        [SerializeField] private FloatProvider rangeProvider;
        [SerializeField] private SpawnPointValidator spawnPointValidator;

        protected override void PrepareObjectForTraining()
        {
            var range = rangeProvider.ProvideFloat();
            constantDamageArea.Radius = range;
            constantDamageArea.gameObject.SetActive(true);

            do
            {
                constantDamageArea.transform.position = locationProvider.ProvideLocation();
            } while (!spawnPointValidator.IsSpawnPointValid(
                constantDamageArea.transform.position,
                constantDamageArea.OccupiedBounds.extents,
                constantDamageArea.transform.rotation,
                constantDamageArea.gameObject
            ));
        }
        
        protected override void CleanUpObjectForTraining()
        {
            base.CleanUpObjectForTraining();
            constantDamageArea.gameObject.SetActive(false);
        }
        
    }
}