using Elements.Turret;
using Ui.Turret.Option.Entry;
using UnityEngine;

namespace Ui.Turret.Option.ListView
{
    public interface IUpgradeOptionsListView
    {
    }

    public class UpgradeOptionsListView : MonoBehaviour, IUpgradeOptionsListView
    {
        private IUpgradeOptionView _selectedUpgradeOptionView;
        private IUpgradeOptionListPresenter _presenter;

        [SerializeField] private UpgradeOptionListPresenterFactory presenterFactory;
        [SerializeField] private TurretProvider turretProvider;
        
        private void OnEnable()
        {
            _presenter = presenterFactory.CreatePresenter(this, turretProvider.Turret);
        }

        private void Start()
        {
            _presenter.OnInitialize();
        }

        private void OnDisable()
        {
            _presenter.Dispose();
        }
    }
}