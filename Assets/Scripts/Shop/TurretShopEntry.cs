using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "Data/TurretShopEntry")]
    public class TurretShopEntry : ScriptableObject
    {
        [SerializeField] private GameObject turretPrefab;
        [SerializeField] private int price;

        public GameObject TurretPrefab => turretPrefab;

        public int Price => price;
    }
}