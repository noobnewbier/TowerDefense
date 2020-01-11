using UnityEngine;

namespace Elements.Turret.Upgrade.UI
{
    public interface IStatisticView
    {
        void UpdateView(float percentage);
    }

    public class TurretStatisticBar : MonoBehaviour, IStatisticView
    {
        private IStatisticPresenter _presenter;
        [SerializeField] private Transform imageTransform;
        [SerializeField] private TurretStatisticPresenterFactory presenterFactory;

        public void UpdateView(float percentage)
        {
            imageTransform.localScale = new Vector3(percentage, imageTransform.transform.localScale.y, imageTransform.localScale.z);
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