using Common.Events;
using Units.UnitCommon;
using UnityEngine;

namespace Units.Enemies
{
    public class DummyUnit : Unit
    {
        [SerializeField] private UnitData unitData;
        
        protected override UnitData UnitData
        {
            get => unitData;
            set => unitData = value;
        }

        protected override void Dies()
        {
            EventAggregator.Publish(new EnemyDeadEvent(this));
            Destroy(gameObject);
        }
    }
}