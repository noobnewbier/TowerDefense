using Elements.Turret;
using UnityEngine;

namespace ScriptableService
{
    public interface IPlaceTurretService
    {
        void PlaceTurret(TurretProvider turretProvider, Transform turretSpawnPoint);
    }

    [CreateAssetMenu(menuName = "ScriptableService/PlaceTurretService")]
    public class PlaceTurretService : ScriptableObject, IPlaceTurretService
    {
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void PlaceTurret(TurretProvider turretProvider, Transform turretSpawnPoint)
        {
            var newTurret = turretProvider.GetTurretPrefab();
            var position = turretSpawnPoint.position;
            
            newTurret.transform.position = new Vector3(
                position.x,
                newTurret.transform.position
                    .y, //use the game object's position, as different model is having a different origin, geeez
                position.z
            );

            newTurret.transform.rotation = turretSpawnPoint.rotation;
        }
    }
}