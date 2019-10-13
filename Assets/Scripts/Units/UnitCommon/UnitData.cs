using UnityEngine;

// ReSharper disable ConvertToAutoProperty

namespace Units.UnitCommon
{
    [CreateAssetMenu(menuName = "Data/UnitData")]
    public class UnitData : ScriptableObject
    {
        [SerializeField] private int health;

        [SerializeField] private int maxHealth;

        public int Health
        {
            get => health;
            set => health = value;
        }

        public int MaxHealth => maxHealth;
    }
}