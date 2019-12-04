using Elements.Units.UnitCommon;
using UnityEngine;

namespace Effects.Bonuses
{
    [CreateAssetMenu(menuName = "Data/Bonus/HealthBonus")]
    public class HealthBonus : Bonus
    {
        [SerializeField] private int healthChange;
        
        public override void ApplyEffect(UnitData data)
        {
            data.Health += healthChange;
        }
    }
}