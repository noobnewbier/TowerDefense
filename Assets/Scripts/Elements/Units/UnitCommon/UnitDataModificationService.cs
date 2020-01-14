using System;
using Common.Enum;
using EventManagement;

namespace Elements.Units.UnitCommon
{
    //part-time repository :)   
    public interface IUnitDataModificationService
    {
        bool IsDyingNextFrame { get; }
        EffectSource DeathSource { get; }
        void ModifyHealth(int value, EffectSource source);
        void ModifyForwardSpeed(float value);
        void ModifyBackwardSpeed(float value);
    }

    public class UnitDataModificationModificationService : IUnitDataModificationService
    {
        private readonly UnitData _unitData;
        private readonly Unit _unit;
        private readonly IEventAggregator _eventAggregator;

        public UnitDataModificationModificationService(UnitData unitData, IEventAggregator eventAggregator, Unit unit)
        {
            _unitData = unitData;
            _eventAggregator = eventAggregator;
            _unit = unit;
        }

        public bool IsDyingNextFrame { get; private set; }
        public EffectSource DeathSource { get; private set; }

        public void ModifyHealth(int value, EffectSource source)
        {
            // if it is already dead, don't do anything
            if (IsDyingNextFrame)
            {
                return;
            }

            //if it is going to die after taking the damage
            if (value <= 0)
            {
                IsDyingNextFrame = true;
                DeathSource = source;
            }

            _unitData.Health = Math.Min(value, _unitData.MaxHealth);

            _eventAggregator.Publish(new UnitHealthChangedEvent(_unit));
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