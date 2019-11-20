using Common.Enum;
using UnityEngine;

namespace Common.Interface
{
    //Anything that the AI cares
    public interface IObjectOfInterest
    {
        AiInterestedObjectType InterestedObjectType { get; }
        Bounds Bounds { get; }
    }
}