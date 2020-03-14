using Common.Enum;
using UnityEngine;

namespace Common.Interface
{
    //Anything that the AI cares
    public interface IObjectOfInterest
    {
        AiInterestCategory InterestCategory { get; }
        Bounds Bounds { get; }
        Transform ObjectTransform { get; }
    }

    public interface IDynamicObjectOfInterest : IObjectOfInterest
    {
    }

    public interface IStaticObjectOfInterest : IObjectOfInterest
    {
    }
}