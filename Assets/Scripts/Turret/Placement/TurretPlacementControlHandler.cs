using Shop;
using Turret.Placement.InputSource;
using UnityEngine;

namespace Turret.Placement
{
    public class TurretPlacementControlHandler : MonoBehaviour
    {
        [SerializeField] private TurretPlacementInputSource inputSource;
        [SerializeField] private TurretPlacementControlModel model;
        [SerializeField] private Transform turretSpawnPoint;
        private MoneyManager _moneyManager;

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
                else
                {
//                    Debug.Log("you need error handling here");
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