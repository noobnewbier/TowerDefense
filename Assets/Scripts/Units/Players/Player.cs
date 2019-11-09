using System.Diagnostics;
using Common;
using Common.Events;
using Units.UnitCommon;
using UnityEngine;

namespace Units.Players
{
    public class Player : Unit
    {
        [SerializeField] private UnitData unitData;

        protected override UnitData UnitData
        {
            get => unitData;
            set => unitData = value;
        }

        protected override void Dies()
        {
            EventAggregator.Publish(new PlayerDeadEvent());
            DeathEffect();
            Destroy(gameObject);
        }

        [Conditional(GameConfig.GameplayMode)]
        protected void DeathEffect()
        {
            //not implemented
        }
    }
}