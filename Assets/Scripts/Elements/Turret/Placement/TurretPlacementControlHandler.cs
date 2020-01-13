using Elements.Turret.Placement.InputSource;
using Manager;
using UnityEngine;

namespace Elements.Turret.Placement
{
    public class TurretPlacementControlHandler : MonoBehaviour
    {
        private MoneyManager _moneyManager;
        [SerializeField] private TurretPlacementInputSource inputSource;
        [SerializeField] private TurretPlacementControlModel model;
        [SerializeField] private Transform turretSpawnPoint;

        private void OnEnable()
        {
            _moneyManager = FindObjectOfType<MoneyManager>();
        }

        private void Update()
        {
            if (inputSource.ReceivedPlaceTurretInput())
            {
                if (_moneyManager.Money >= model.TurretPrice)
                {
                    PlaceTurret(model.CopyOfTurret);
                }
            }
        }

        private void PlaceTurret(GameObject turretGameObject)
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