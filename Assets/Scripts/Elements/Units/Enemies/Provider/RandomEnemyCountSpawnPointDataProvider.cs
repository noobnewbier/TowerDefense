using UnityEngine;

namespace Elements.Units.Enemies.Provider
{
    public class RandomEnemyCountSpawnPointDataProvider : EnemySpawnPointDataProvider
    {
        [SerializeField] private EnemySpawnPointData data;
        [SerializeField] private int max;
        [SerializeField] private int min;

        public override EnemySpawnPointData ProvideData()
        {
            var count = Random.Range(min, max + 1);
            return data.WithTotalNumberOfEnemies(count);
        }
    }
}