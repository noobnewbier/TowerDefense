using Common.Event;
using EventManagement;

namespace Common.Interface
{
    public interface IDamageTaker : IHandle<DamageEvent>
    {
    }
}