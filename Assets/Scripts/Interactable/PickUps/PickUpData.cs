using System.Collections.Generic;
using Effects;
using Rules;
using UnityEngine;

namespace Interactable.PickUps
{
    [CreateAssetMenu(menuName = "Data/PickUpData")]
    public class PickUpData : ScriptableObject
    {
        [SerializeField] private Effect effect;
        [SerializeField] private Rule[] rules;
        public Effect Effect => effect;
        public IEnumerable<Rule> Rules => rules;
    }
}