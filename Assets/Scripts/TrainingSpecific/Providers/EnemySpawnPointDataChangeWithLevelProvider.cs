using Elements.Units;
using Elements.Units.Enemies;
using Elements.Units.Enemies.Provider;
using UnityEngine;

namespace TrainingSpecific.Providers
{
    public class EnemySpawnPointDataChangeWithLevelProvider : EnemySpawnPointDataProvider
    {
        private LevelTracker _levelTracker;
        [SerializeField] private EnemySpawnPointDataProvider[] dataProviders;

        private void OnEnable()
        {
            _levelTracker = LevelTracker.Instance;
        }

        public override EnemySpawnPointData ProvideData()
        {
            return dataProviders[_levelTracker.CurrentLevel].ProvideData();
        }
    }
}