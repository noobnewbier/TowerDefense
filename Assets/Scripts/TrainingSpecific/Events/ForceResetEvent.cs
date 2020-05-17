using Common.Interface;

namespace TrainingSpecific.Events
{
    public struct ForceResetEvent
    {
        public IDynamicObjectOfInterest DynamicObjectOfInterest { get; }

        public ForceResetEvent(IDynamicObjectOfInterest dynamicObjectOfInterest)
        {
            DynamicObjectOfInterest = dynamicObjectOfInterest;
        }
    }
}