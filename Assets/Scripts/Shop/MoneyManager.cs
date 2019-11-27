using UnityEngine;

namespace Shop
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private float moneyGainPerTick = 0.0005f;

        [field: SerializeField] public float Money { get; private set; } = 100;

        private void FixedUpdate()
        {
            Money += moneyGainPerTick;
        }
    }
}