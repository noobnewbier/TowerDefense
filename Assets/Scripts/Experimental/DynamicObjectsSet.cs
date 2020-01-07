using Common.Interface;
using UnityEngine;

namespace Experimental
{
    [CreateAssetMenu(menuName = "RuntimeSet/DynamicObjects")]
    public class DynamicObjectsSet : RuntimeSet<IDynamicObjectOfInterest>
    {
    }
}