using UnityEngine;
using UnityUtils.ScaleProviders;

namespace TrainingSpecific.Providers
{
    public class ScaleChangeWithLevelProvider : ScaleProvider
    {
        private LevelTracker _levelTracker;
        [SerializeField] private ScaleProvider[] scales;
        public override Vector3 ProvideScale() => scales[_levelTracker.CurrentLevel].ProvideScale();

        private void OnEnable()
        {
            _levelTracker = LevelTracker.Instance;
        }
    }
}