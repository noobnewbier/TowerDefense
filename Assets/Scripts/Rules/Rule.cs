using System.Linq;
using UnityEngine;

namespace Rules
{
    [CreateAssetMenu(menuName = "Rule")]
    public class Rule : ScriptableObject
    {
        [SerializeField] private Fact[] requiredFact;

        public bool AdhereToRule(IHasFact hasFact)
        {
            return requiredFact.All(t => hasFact.Facts.Contains(t));
        }
    }
}