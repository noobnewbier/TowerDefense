using UnityEngine;

namespace Elements.Units.Enemies.Provider
{
    public class ExactEnemySpawnPointDataProvider : EnemySpawnPointDataProvider
    {
        [SerializeField] private EnemySpawnPointData spawnPointData;
        
        public override EnemySpawnPointData ProvideData()
        {
            return spawnPointData;
        }
    }
}