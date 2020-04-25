using Experimental;
using UnityEngine;
using UnityUtils.ScriptableReference;

namespace ScriptableService
{
    public interface IUseResourceService
    {
        bool TryUseResource(int amount);
    }

    [CreateAssetMenu(menuName = "ScriptableService/UseResourceService")]
    public class UseResourceService : ScriptableObject, IUseResourceService
    {
        [SerializeField] private RuntimeFloat resource;

        public bool TryUseResource(int amount)
        {
            if (resource.CurrentValue > amount)
            {
                resource.CurrentValue -= amount;
                return true;
            }

            return false;
        }
    }
}