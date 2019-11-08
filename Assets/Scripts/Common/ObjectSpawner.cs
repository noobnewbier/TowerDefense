using UnityEngine;

namespace Common
{
    //We don't give a damn what do you want, give me something, I will create it for ya
    public static class ObjectSpawner
    {
        public static GameObject SpawnInCircle(GameObject prefab, float radius, Vector3 spawnPoint)
        {
            var randomXz = Random.insideUnitCircle * radius;
            return Object.Instantiate(
                prefab,
                spawnPoint + new Vector3(randomXz.x, 0f,  randomXz.y),
                Quaternion.identity
            );
        }
    }
}