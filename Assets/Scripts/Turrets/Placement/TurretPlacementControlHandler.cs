using Shop;
using Turrets.Placement.InputSource;
using UnityEngine;

namespace Turrets.Placement
{
    public class TurretPlacementControlHandler : MonoBehaviour
    {
        [SerializeField] private MoneyManager moneyManager;
        [SerializeField] private TurretPlacementInputSource inputSource;
        [SerializeField] private TurretPlacementControlModel model;
        [SerializeField] private Transform turretSpawnPoint;

        private void Update()
        {
            if (inputSource.ReceivedPlaceTurretInput())
            {
                if (moneyManager.Money >= model.TurretPrice)
                {
                    PlaceTurret(model.CopyOfTurret);
                }
                else
                {
                    Debug.Log("you need error handling here");
                }
            }
        }

        private void PlaceTurret(GameObject turretGameObject)
        {
            //just to offset the height, assuming origin is half the height;
            var position = turretSpawnPoint.position;
            turretGameObject.transform.position = new Vector3(
                position.x,
                0.5f, //hard code value, fix later
                position.z);
            
            turretGameObject.transform.rotation = turretSpawnPoint.rotation;
            
        }
    }
}