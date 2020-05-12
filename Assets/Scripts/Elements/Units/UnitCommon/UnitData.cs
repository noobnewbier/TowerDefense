using System.Collections.Generic;
using Rules;
using UnityEngine;

// ReSharper disable ConvertToAutoProperty

namespace Elements.Units.UnitCommon
{
    [CreateAssetMenu(menuName = "Data/UnitData")]
    public class UnitData : ScriptableObject
    {
        [SerializeField] private Fact[] facts;
        [SerializeField] private float health;
        [SerializeField] private float maxBackwardSpeed;

        [SerializeField] private float maxForwardSpeed;
        [SerializeField] private float maxHealth;
        [SerializeField] private float rotationSpeed;

        public float MaxBackwardSpeed
        {
            get => maxBackwardSpeed;
            set => maxBackwardSpeed = value;
        }

        public float MaxForwardSpeed
        {
            get => maxForwardSpeed;
            set => maxForwardSpeed = value;
        }

        public float RotationSpeed => rotationSpeed;

        public IEnumerable<Fact> Facts => facts;

        public float Health
        {
            get => health;
            set => health = value;
        }

        public float MaxHealth => maxHealth;
    }
}