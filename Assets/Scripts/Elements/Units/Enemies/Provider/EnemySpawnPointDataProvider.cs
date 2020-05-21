using UnityEngine;

namespace Elements.Units.Enemies.Provider
{
    public abstract class EnemySpawnPointDataProvider : MonoBehaviour
    {
        public abstract EnemySpawnPointData ProvideData();
    }
}