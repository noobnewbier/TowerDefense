using Elements.Units.UnitCommon;
using UnityEngine;

namespace Ui.Hud.Health
{
    public interface IHealthView
    {
        void OnHealthUpdate(float percentage);
    }

    public class HealthView : MonoBehaviour, IHealthView
    {
        [SerializeField] private HealthPresenterFactory presenterFactory;
        [SerializeField] private UnitProvider unitProvider;
        [SerializeField] private Transform barTransform;
        
        private IHealthPresenter _presenter;
        
        private void OnEnable()
        {
            _presenter = presenterFactory.CreatePresenter(this, unitProvider.ProvideUnitDataRepository(), unitProvider.ProvideUnit());
        }

        private void OnDisable()
        {
            _presenter.Dispose();
        }

        public void OnHealthUpdate(float percentage)
        {
            var localScale = barTransform.localScale;
            localScale = new Vector3(percentage, localScale.y, localScale.z);
            
            barTransform.localScale = localScale;
        }
    }
}