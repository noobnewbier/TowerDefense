using Common.Interface;
using UnityEngine;
using UnityUtils.ScriptableReference;

namespace Experimental
{
    [CreateAssetMenu(menuName = "RuntimeSet/DynamicObjects")]
    public class DynamicObjectsSet : RuntimeSet<IDynamicObjectOfInterest>
    {
    }
}