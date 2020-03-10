using Common.Interface;

namespace AgentAi.Manager.TargetPicker
{
    public interface ITargetPicker
    {
        IDynamicObjectOfInterest RequestTarget();
    }
}