using Common.Enum;
using EventManagement;
using UnityEngine;

namespace Elements.Units.UnitCommon
{
    //part-time repository :)   
    public interface IUnitDataModificationService
    {
        bool IsDyingNextFrame { get; }
        EffectSource DeathSource { get; }
        void ModifyHealth(float value, EffectSource source);
        void ModifyForwardSpeed(float value);
        void ModifyBackwardSpeed(float value);
    }

    public class UnitDataModificationModificationService : IUnitDataModificationService
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Unit _unit;
        private readonly UnitData _unitData;

        public UnitDataModificationModificationService(UnitData unitData, IEventAggregator eventAggregator, Unit unit)
        {
            _unitData = unitData;
            _eventAggregator = eventAggregator;
            _unit = unit;
        }

        public bool IsDyingNextFrame { get; private set; }
        public EffectSource DeathSource { get; private set; }

        public void ModifyHealth(float value, EffectSource source)
        {
            // if it is already dead, don't do anything
            if (IsDyingNextFrame) return;

            //if it is going to die after taking the damage
            if (value <= 0)
            {
                IsDyingNextFrame = true;
                DeathSource = source;
            }

            var originalHealth = _unitData.Health;
            _unitData.Health = Mathf.Min(value, _unitData.MaxHealth);

            _eventAggregator.Publish(new UnitHealthChangedEvent(_unit, source, _unitData.Health - originalHealth));
        }

        public void ModifyForwardSpeed(float value)
        {
            _unitData.MaxForwardSpeed = value;
        }

        public void ModifyBackwardSpeed(float value)
        {
            _unitData.MaxBackwardSpeed = value;
        }
    }
}