using Common.Interface;

namespace TrainingSpecific.Events
{
    public struct ForceResetEvent
    {
        public readonly IDynamicObjectOfInterest DynamicObjectOfInterest;

        public ForceResetEvent(IDynamicObjectOfInterest dynamicObjectOfInterest)
        {
            DynamicObjectOfInterest = dynamicObjectOfInterest;
        }
    }
}