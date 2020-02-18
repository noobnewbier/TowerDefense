using Elements.Turret.Placement.InputSource;
using ScriptableService;
using UnityEngine;

namespace Elements.Turret.Placement
{
    public class TurretPlacementControlHandler : MonoBehaviour
    {
        [SerializeField] private TurretPlacementInputSource inputSource;
        [SerializeField] private TurretPlacementControlModel model;
        [SerializeField] private PlaceTurretService placeTurretService;
        [SerializeField] private SpawnPointValidator spawnPointValidator;
        [SerializeField] private Transform turretSpawnPoint;
        [SerializeField] private UseResourceService useResourceService;

        private void Update()
        {
            if (inputSource.ReceivedPlaceTurretInput()) TryPlaceTurret();
        }

        private void TryPlaceTurret()
        {
            var isSpawnpointValid = spawnPointValidator.IsSpawnPointValid(
                turretSpawnPoint.position,
                model.TurretProvider.HalfSize,
                turretSpawnPoint.transform.rotation
            );

            if (isSpawnpointValid && useResourceService.TryUseResource(model.TurretProvider.GetRepository().Cost))
                placeTurretService.PlaceTurret(model.TurretProvider, turretSpawnPoint);
        }
    }
}