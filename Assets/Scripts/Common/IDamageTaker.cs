using Common.Events;
using EventManagement;

namespace Common
{
    public interface IDamageTaker : IHandle<DamageEvent>
    {
    }
}