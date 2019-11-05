using UnityEngine;

namespace Common
{
    //We don't give a damn what do you want, give me something, I will create it for ya
    public static class ObjectSpawner
    {
        public static void SpawnInCircle(GameObject prefab, float radius, Vector3 spawnPoint)
        {
            var randomXz = Random.insideUnitCircle * radius;
            Object.Instantiate(
                prefab,
                spawnPoint + new Vector3(randomXz.x, randomXz.y, 0f),
                Quaternion.identity
            );
        }
    }
}