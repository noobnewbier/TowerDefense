using UnityEngine;

namespace Elements.Units
{
    [CreateAssetMenu(menuName = "Data/EnemySpawnPointData")]
    public class EnemySpawnPointData : ScriptableObject
    {
        [SerializeField] private GameObject[] enemiesPrefabs;
        [SerializeField] private float spawnInterval;
        [SerializeField] private int totalNumberOfEnemies;
        public GameObject[] EnemiesPrefabs => enemiesPrefabs;

        public float SpawnInterval => spawnInterval;

        public int TotalNumberOfEnemies => totalNumberOfEnemies;
    }
}