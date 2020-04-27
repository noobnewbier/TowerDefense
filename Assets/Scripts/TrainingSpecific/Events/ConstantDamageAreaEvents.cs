using Common.Event;
using Common.Interface;
// ReSharper disable SuggestBaseTypeForParameter

namespace TrainingSpecific.Events
{
    public class ConstantDamageAreaSpawnedEvent : IDynamicObjectSpawnedEvent
    {
        public ConstantDamageAreaSpawnedEvent(ConstantDamageArea constantDamageArea)
        {
            DynamicObject = constantDamageArea;
        }

        public IDynamicObjectOfInterest DynamicObject { get; }
    }

    public class ConstantDamageAreaDestroyedEvent : IDynamicObjectDestroyedEvent
    {
        public ConstantDamageAreaDestroyedEvent(ConstantDamageArea constantDamageArea)
        {
            DynamicObject = constantDamageArea;
        }

        public IDynamicObjectOfInterest DynamicObject { get; }
    }
}