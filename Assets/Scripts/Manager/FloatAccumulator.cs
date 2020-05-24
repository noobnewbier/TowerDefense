using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.ScriptableReference;

namespace Manager
{
    public class FloatAccumulator : MonoBehaviour
    {
        [FormerlySerializedAs("resource")] [FormerlySerializedAs("money")] [SerializeField]
        private RuntimeFloat runtimeFloat;

        [FormerlySerializedAs("resourceGainPerTick")] [FormerlySerializedAs("moneyGainPerTick")] [SerializeField]
        private float gainPerTick = 0.0005f;

        private void FixedUpdate()
        {
            runtimeFloat.CurrentValue += gainPerTick;
        }
    }
}