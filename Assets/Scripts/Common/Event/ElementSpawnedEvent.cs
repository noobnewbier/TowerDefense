using Common.Interface;

namespace Common.Event
{
    public interface IDynamicObjectSpawnedEvent
    {
        IDynamicObjectOfInterest DynamicObject { get; }
    }

    public interface IDynamicObjectDestroyedEvent
    {
        IDynamicObjectOfInterest DynamicObject { get; }
    }

    public struct DynamicObjectSpawnedEvent : IDynamicObjectSpawnedEvent
    {
        public DynamicObjectSpawnedEvent(IDynamicObjectOfInterest element)
        {
            DynamicObject = element;
        }

        public IDynamicObjectOfInterest DynamicObject { get; }
    }
}