using UnityEngine;

// ReSharper disable ConvertToAutoProperty

namespace Health
{
    [CreateAssetMenu(menuName = "Data/HealthData")]
    public class HealthData : ScriptableObject
    {
        [SerializeField] private int health;

        public int Health
        {
            get => health;
            set => health = value;
        }

        [SerializeField] private int maxHealth;
        public int MaxHealth => maxHealth;
    }
}