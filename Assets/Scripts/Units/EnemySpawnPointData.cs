using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "EnemySpawnPointData")]
    public class EnemySpawnPointData : ScriptableObject
    {
        public GameObject[] EnemiesPrefabs => enemiesPrefabs;

        public float SpawnInterval => spawnInterval;

        public int TotalNumberOfEnemies => totalNumberOfEnemies;

        [SerializeField] private GameObject[] enemiesPrefabs;
        [SerializeField] private float spawnInterval;
        [SerializeField] private int totalNumberOfEnemies;
    }
}