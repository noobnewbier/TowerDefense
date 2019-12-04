using Common.Event;
using EventManagement;

namespace Interactable
{
    public interface ICanInteract : IHandle<ApplyEffectEvent>
    {
    }
}