using Experimental;
using UnityEngine;

namespace Elements.Turret.Upgrade.UI
{
    [CreateAssetMenu(menuName = "ScriptableFactory/TurretStatisticPresenterFactory")]
    public class TurretStatisticPresenterFactory : ScriptableObject
    {
        [SerializeField] private TurretStatisticModel model;
        [SerializeField] private ScriptableEventAggregator eventAggregator;
        
        public IStatisticPresenter CreatePresenter(IStatisticView view)
        {
            return new TurretStatisticPresenter(view, model, eventAggregator.Instance);
        } 
    }
}