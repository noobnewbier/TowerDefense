using Common.Events;
using EventManagement;

namespace Common.Interface
{
    public interface IDamageTaker : IHandle<DamageEvent>
    {
    }
}