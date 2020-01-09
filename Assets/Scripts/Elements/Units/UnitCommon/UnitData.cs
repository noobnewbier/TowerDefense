using Rules;
using UnityEngine;

// ReSharper disable ConvertToAutoProperty

namespace Elements.Units.UnitCommon
{
    [CreateAssetMenu(menuName = "Data/UnitData")]
    public class UnitData : ScriptableObject
    {
        [SerializeField] private int health;
        [SerializeField] private int maxHealth;
        [SerializeField] private float maxSpeed;
        [SerializeField] private Fact[] facts;

        public Fact[] Facts => facts;

        public int Health
        {
            get => health;
            set => health = value;
        }

        public float MaxSpeed
        {
            get => maxSpeed;
            set => maxSpeed = value;
        }

        public int MaxHealth => maxHealth;
    }
}