using UnityEngine;

namespace Elements.Units
{
    [CreateAssetMenu(menuName = "Data/EnemySpawnPointData")]
    public class EnemySpawnPointData : ScriptableObject
    {
        [field: SerializeField] public GameObject[] EnemiesPrefabs { get; }

        [field: SerializeField] public float SpawnInterval { get; }

        [field: SerializeField] public int TotalNumberOfEnemies { get; }
    }
}