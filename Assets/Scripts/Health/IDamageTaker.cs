using EventManagement;

namespace Health
{
    public interface IDamageTaker : IHandle<DamageEvent>
    {
        HealthData HealthData { get; }
    }
}