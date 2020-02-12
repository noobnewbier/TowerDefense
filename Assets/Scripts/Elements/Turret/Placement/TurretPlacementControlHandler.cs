using Elements.Turret.Placement.InputSource;
using ScriptableService;
using UnityEngine;

namespace Elements.Turret.Placement
{
    public class TurretPlacementControlHandler : MonoBehaviour
    {
        [SerializeField] private TurretPlacementInputSource inputSource;
        [SerializeField] private TurretPlacementControlModel model;
        [SerializeField] private SpawnPointValidator spawnPointValidator;
        [SerializeField] private Transform turretSpawnPoint;
        [SerializeField] private UseResourceService useResourceService;


        private void Update()
        {
            if (inputSource.ReceivedPlaceTurretInput()) TryPlaceTurret();
        }

        private void TryPlaceTurret()
        {
            var isSpawnpointValid = spawnPointValidator.IsSpawnPointValid(turretSpawnPoint.position, model.HalfSize,
                turretSpawnPoint.transform.rotation);

            if (isSpawnpointValid && useResourceService.TryUseResource(model.TurretPrice))
            {
                var turretGameObject = model.ProvideCopyOfTurret;
                //just to offset the height, assuming origin is half the height;
                var position = turretSpawnPoint.position;
                turretGameObject.transform.position = new Vector3(
                    position.x,
                    position.y + 0.5f, //0.5 is a hard code value such that turret stands on the ground, fix later
                    position.z
                );

                turretGameObject.transform.rotation = turretSpawnPoint.rotation;
            }
        }
    }
}