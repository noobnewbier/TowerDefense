using UnityEngine;

namespace Elements.Turret.Upgrade.UI.TurretStatistic
{
    public interface IStatisticView
    {
        void UpdateDamage(float percentage);
        void UpdateShootFrequency(float percentage);
        void UpdateRange(float percentage);
    }

    public class TurretStatisticBarsView : MonoBehaviour, IStatisticView
    {
        private IStatisticPresenter _presenter;
        [SerializeField] private Transform damageBarTransform;
        [SerializeField] private Transform rangeBarTransform;
        [SerializeField] private Transform frequencyBarTransform;
        [SerializeField] private TurretStatisticPresenterFactory presenterFactory;

        public void UpdateDamage(float percentage)
        {
            UpdateBar(damageBarTransform, percentage);
        }

        public void UpdateShootFrequency(float percentage)
        {
            UpdateBar(frequencyBarTransform, percentage);
        }

        public void UpdateRange(float percentage)
        {
            UpdateBar(rangeBarTransform, percentage);
        }

        private static void UpdateBar(Transform barTransform, float percentage)
        {
            var barLocalScale = barTransform.localScale;
            percentage = Mathf.Clamp01(percentage);
            barLocalScale = new Vector3(percentage, barLocalScale.y, barLocalScale.z);
            
            barTransform.localScale = barLocalScale;
        }

        private void OnEnable()
        {
            _presenter = presenterFactory.CreatePresenter(this);
        }

        private void OnDisable()
        {
            _presenter.Dispose();
        }
    }
}