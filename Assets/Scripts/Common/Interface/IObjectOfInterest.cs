using Common.Struct;
using UnityEngine;

namespace Common.Interface
{
    //Anything that the AI cares
    public interface IObjectOfInterest
    {
        InterestedInformation InterestedInformation { get; }
        Transform ObjectTransform { get; }
    }

    public interface IDynamicObjectOfInterest : IObjectOfInterest
    {
    }

    public interface IStaticObjectOfInterest : IObjectOfInterest
    {
    }
}