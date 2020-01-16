using Elements.Turret.Placement.InputSource;
using Manager;
using ScriptableService;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elements.Turret.Placement
{
    public class TurretPlacementControlHandler : MonoBehaviour
    {
        [SerializeField] private UseResourceService useResourceService;
        [SerializeField] private TurretPlacementInputSource inputSource;
        [SerializeField] private TurretPlacementControlModel model;
        [SerializeField] private Transform turretSpawnPoint;


        private void Update()
        {
            if (inputSource.ReceivedPlaceTurretInput())
            {
                TryPlaceTurret(model.CopyOfTurret);
            }
        }

        private void TryPlaceTurret(GameObject turretGameObject)
        {
            if (useResourceService.TryUseResource(model.TurretPrice))
            {
                //just to offset the height, assuming origin is half the height;
                var position = turretSpawnPoint.position;
                turretGameObject.transform.position = new Vector3(
                    position.x,
                    0.5f, //hard code value such that turret stands on the ground, fix later
                    position.z
                );

                turretGameObject.transform.rotation = turretSpawnPoint.rotation;
            }
        }
    }
}