using UnityEngine;
using UnityUtils;

namespace TrainingSpecific.Providers
{
    public class ActivityChangeWithLevelProvider : ActivityProvider
    {
        private LevelTracker _levelTracker;

        // need to be length of threshold in curriculum + 1
        [SerializeField] private ActivityProvider[] isActives;

        private void OnEnable()
        {
            _levelTracker = LevelTracker.Instance;
        }

        public override bool ProvideIsActive() => isActives[_levelTracker.CurrentLevel].ProvideIsActive();
    }
}