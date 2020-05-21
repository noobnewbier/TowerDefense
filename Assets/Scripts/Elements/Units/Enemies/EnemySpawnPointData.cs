using UnityEngine;

namespace Elements.Units.Enemies
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

        //dirty as hell, but again this has to do for now.
        //seriously I hate myself for doing this
        public EnemySpawnPointData WithTotalNumberOfEnemies(int count)
        {
            var toReturn = Instantiate(this);
            toReturn.totalNumberOfEnemies = count;

            return toReturn;
        }
    }
}