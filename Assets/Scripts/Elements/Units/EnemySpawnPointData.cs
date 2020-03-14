using UnityEngine;

namespace Elements.Units
{
    [CreateAssetMenu(menuName = "Data/EnemySpawnPointData")]
    public class EnemySpawnPointData : ScriptableObject
    {
        [SerializeField] private GameObject[] enemies;
        [SerializeField] private float spawnInterval;
        [SerializeField] private int totalNumberOfEnemies;

        public GameObject[] Enemies => enemies;
        public float SpawnInterval => spawnInterval;
        public int TotalNumberOfEnemies => totalNumberOfEnemies;
    }
}