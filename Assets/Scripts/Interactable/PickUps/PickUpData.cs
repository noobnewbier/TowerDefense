using Effects;
using UnityEngine;

namespace Interactable.PickUps
{
    [CreateAssetMenu(menuName = "Data/PickUpData")]
    public class PickUpData : ScriptableObject
    {
        [SerializeField] private Effect effect;
        public Effect Effect => effect;
    }
}