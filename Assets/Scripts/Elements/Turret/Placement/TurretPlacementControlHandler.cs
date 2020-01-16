using Elements.Turret.Placement.InputSource;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elements.Turret.Placement
{
    public class TurretPlacementControlHandler : MonoBehaviour
    {
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private TurretPlacementInputSource inputSource;
        [SerializeField] private TurretPlacementControlModel model;
        [SerializeField] private Transform turretSpawnPoint;
        

        private void Update()
        {
            if (inputSource.ReceivedPlaceTurretInput())
            {
                if (resourceManager.Resource >= model.TurretPrice)
                {
                    PlaceTurret(model.CopyOfTurret);
                }
            }
        }

        private void PlaceTurret(GameObject turretGameObject)
        {
            if (resourceManager.TryUseResource(model.TurretPrice))
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