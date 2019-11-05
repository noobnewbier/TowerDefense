namespace Common.Events
{
    public struct DamageEvent
    {
        public DamageEvent(IDamageTaker damageTaker, int amount)
        {
            DamageTaker = damageTaker;
            Amount = amount;
        }

        public int Amount { get; }
        public IDamageTaker DamageTaker { get; }
    }
}