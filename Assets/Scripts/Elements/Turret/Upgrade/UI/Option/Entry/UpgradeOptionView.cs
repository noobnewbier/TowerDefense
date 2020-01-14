using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Elements.Turret.Upgrade.UI.Option.Entry
{
    public interface IUpgradeOptionView
    {
        void UpdateTurretName(string newName);
        void UpdateTurretCost(string costStringRepresentation);
        void HideOption();
        void ShowOption();
    }

    public class UpgradeOptionView : MonoBehaviour, IUpgradeOptionView, IPointerClickHandler
    {
        [SerializeField] private UpgradeOptionPresenterFactory presenterFactory;
        [SerializeField] private TextMeshProUGUI turretName;
        [SerializeField] private TextMeshProUGUI turretCost;
        [SerializeField] private TurretProvider currentTurretProvider;
        [SerializeField] private bool setAsDefault;

        private IUpgradeOptionPresenter _presenter;

        private void OnEnable()
        {
            _presenter = presenterFactory.CreatePresenter(this, currentTurretProvider.Turret, transform.GetSiblingIndex());
        }

        private void Start()
        {
            if (setAsDefault)
            {
                _presenter.OnCheckOption();
            }
        }

        public void UpdateTurretName(string newName)
        {
            turretName.text = newName;
        }

        public void UpdateTurretCost(string costStringRepresentation)
        {
            turretCost.text = costStringRepresentation;
        }

        public void HideOption()
        {
            gameObject.SetActive(false);
        }

        public void ShowOption()
        {
            gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _presenter.Dispose();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.clickCount)
            {
                case 1:
                    _presenter.OnCheckOption();
                    break;
                case 2:
                    _presenter.OnSelectOption();
                    break;
                default:
                    _presenter.OnCheckOption();
                    break;
            }
        }
    }
}