using Experimental;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public class ResourceManager : MonoBehaviour
    {
        [FormerlySerializedAs("money")] [SerializeField] private RuntimeFloat resource;
        [FormerlySerializedAs("moneyGainPerTick")] [SerializeField] private float resourceGainPerTick = 0.0005f;

        public float Resource => resource.CurrentValue;

        private void FixedUpdate()
        {
            resource.CurrentValue += resourceGainPerTick;
        }

        public bool TryUseResource(int amount)
        {
            if (resource.CurrentValue > amount)
            {
                resource.CurrentValue -= amount;
                return true;
            }

            return false;
        }
    }
}