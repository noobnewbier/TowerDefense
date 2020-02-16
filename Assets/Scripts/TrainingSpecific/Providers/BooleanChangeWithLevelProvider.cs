using UnityEngine;
using UnityUtils;
using UnityUtils.BooleanProviders;

namespace TrainingSpecific.Providers
{
    public class BooleanChangeWithLevelProvider : BooleanProvider
    {
        private LevelTracker _levelTracker;

        // need to be length of threshold in curriculum + 1
        [SerializeField] private BooleanProvider[] isActives;

        private void OnEnable()
        {
            _levelTracker = LevelTracker.Instance;
        }

        public override bool ProvideBoolean() => isActives[_levelTracker.CurrentLevel].ProvideBoolean();
    }
}