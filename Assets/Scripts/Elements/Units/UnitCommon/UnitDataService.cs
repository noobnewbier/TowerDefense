using System;
using Common.Enum;

namespace Elements.Units.UnitCommon
{
    //part-time repository :)   
    public interface IUnitDataService
    {
        bool IsDyingNextFrame { get; }
        EffectSource DeathSource { get; }
        void ModifyHealth(int value, EffectSource source);
        void ModifyForwardSpeed(float value);
        void ModifyBackwardSpeed(float value);

    }
    
    public class UnitDataService : IUnitDataService
    {
        private readonly UnitData _unitData;

        public UnitDataService(UnitData unitData)
        {
            _unitData = unitData;
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
        }

        public void ModifyForwardSpeed(float value)
        {
            _unitData.MaxForwardSpeed= value;
        }

        public void ModifyBackwardSpeed(float value)
        {
            _unitData.MaxBackwardSpeed = value;
        }
    }
}