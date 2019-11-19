using Common.Enum;
using Common.Interface;

namespace Common.Events
{
    public struct DamageEvent
    {
        public DamageEvent(IDamageTaker damageTaker, int amount, DamageSource damageSource)
        {
            DamageTaker = damageTaker;
            Amount = amount;
            DamageSource = damageSource;
        }

        public int Amount { get; }
        public IDamageTaker DamageTaker { get; }
        public DamageSource DamageSource { get; }
    }
}