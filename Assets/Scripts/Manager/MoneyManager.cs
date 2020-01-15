using Experimental;
using UnityEngine;

namespace Manager
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private RuntimeFloat money;
        [SerializeField] private float moneyGainPerTick = 0.0005f;

        public float Money => money.CurrentValue;

        private void FixedUpdate()
        {
            money.CurrentValue += moneyGainPerTick;
        }
    }
}