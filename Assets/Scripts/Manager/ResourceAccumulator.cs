using Experimental;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public class ResourceAccumulator : MonoBehaviour
    {
        [FormerlySerializedAs("money")] [SerializeField] private RuntimeFloat resource;
        [FormerlySerializedAs("moneyGainPerTick")] [SerializeField] private float resourceGainPerTick = 0.0005f;

        public float Resource => resource.CurrentValue;

        private void FixedUpdate()
        {
            resource.CurrentValue += resourceGainPerTick;
        }
    }
}