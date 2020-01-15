using UnityEngine;

namespace Experimental
{
    [CreateAssetMenu(menuName = "RuntimeFloat")]
    public class RuntimeFloat : ScriptableObject
    {
        [SerializeField] private float initialValue;
        [SerializeField] private float currentValue;

        public float CurrentValue
        {
            get => currentValue;
            set => currentValue = value;
        }

        private void OnEnable()
        {
            currentValue = initialValue;
        }
    }
}