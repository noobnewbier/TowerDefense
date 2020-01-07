using Elements.Units.Enemies;
using UnityEngine;

namespace Elements.Units
{
    [CreateAssetMenu(menuName = "Data/EnemySpawnPointData")]
    public class EnemySpawnPointData : ScriptableObject
    {
        [SerializeField] private Enemy[] enemies;
        [SerializeField] private float spawnInterval;
        [SerializeField] private int totalNumberOfEnemies;

        public Enemy[] Enemies => enemies;

        public float SpawnInterval => spawnInterval;

        public int TotalNumberOfEnemies => totalNumberOfEnemies;
    }
}