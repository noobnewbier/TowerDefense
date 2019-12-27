using Common.Class;
using UnityEngine;
using UnityUtils.LocationProviders;

namespace TrainingSpecific
{
    public class LocationChangeWithLevelProvider : LocationProvider
    {
        private LevelTracker _levelTracker;

        // need to be length of threshold in curriculum + 1
        [SerializeField] private LocationProvider[] positions;

        private void OnEnable()
        {
            _levelTracker = LevelTracker.Instance;
        }

        public override Vector3 ProvideLocation() => positions[_levelTracker.CurrentLevel].ProvideLocation();
    }
}