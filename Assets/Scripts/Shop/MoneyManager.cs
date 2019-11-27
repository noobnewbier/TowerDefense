using UnityEngine;

namespace Shop
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private float money = 100;
        [SerializeField] private float moneyGainPerTick = 0.0005f;

        public float Money => money;

        private void FixedUpdate()
        {
            money += moneyGainPerTick;
        }
    }
}