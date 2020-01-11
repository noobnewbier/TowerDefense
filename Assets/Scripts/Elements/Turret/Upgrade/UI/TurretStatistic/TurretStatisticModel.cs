using Experimental;
using UnityEngine;

namespace Elements.Turret.Upgrade.UI.TurretStatistic
{
    public interface IStatisticModel
    {
        float Value { get; set; }
        float MaxValue { get; }
    }

    [CreateAssetMenu(menuName = "ScriptableModel/StatisticModel")]
    public class TurretStatisticModel : ScriptableObject, IStatisticModel
    {
        [SerializeField] private ScriptableEventAggregator eventAggregator;
        
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                eventAggregator.Instance.Publish(new TurretStatisticModelUpdatesEvent(this));        
            }
        }

        [SerializeField] private float maxValue;
        private float _value;

        public float MaxValue => maxValue;
    }
}