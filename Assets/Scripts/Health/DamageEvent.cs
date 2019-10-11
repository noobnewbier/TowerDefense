namespace Health
{
    public struct DamageEvent
    {
        public readonly IDamageTaker DamageTaker;
        public readonly int Damage;

        public DamageEvent(int damage, IDamageTaker damageTaker)
        {
            Damage = damage;
            DamageTaker = damageTaker;
        }
    }
}