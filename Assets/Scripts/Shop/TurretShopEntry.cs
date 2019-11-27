using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "Data/TurretShopEntry")]
    public class TurretShopEntry : ScriptableObject
    {
        [field: SerializeField] public GameObject TurretPrefab { get; }

        [field: SerializeField] public int Price { get; }
    }
}