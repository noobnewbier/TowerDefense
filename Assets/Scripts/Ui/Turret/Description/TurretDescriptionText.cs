using TMPro;
using UnityEngine;

namespace Ui.Turret.Description
{
    public interface ITurretDescriptionView
    {
        void UpdateText(string description);
    }

    public class TurretDescriptionText : MonoBehaviour, ITurretDescriptionView
    {
        [SerializeField] private TurretDescriptionPresenterFactory presenterFactory;
        [SerializeField] private TextMeshProUGUI textComponent;
        
        private ITurretDescriptionPresenter _presenter;

        private void OnEnable()
        {
            _presenter = presenterFactory.CreatePresenter(this);
        }

        public void UpdateText(string description)
        {
            textComponent.text = description;
        }

        private void OnDisable()
        {
            _presenter.Dispose();
        }
    }
}