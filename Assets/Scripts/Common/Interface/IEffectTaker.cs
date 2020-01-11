using Common.Event;
using EventManagement;
using Rules;

namespace Common.Interface
{
    public interface IEffectTaker : IHasFact, IHandle<ApplyEffectEvent>
    {
    }
}