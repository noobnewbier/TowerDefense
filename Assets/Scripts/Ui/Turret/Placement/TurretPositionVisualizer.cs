using Elements.Turret.Placement.InputSource;
using UnityEngine;

namespace Ui.Turret.Placement
{
    public interface ITurretPositionVisualizer
    {
        void FinishVisualizePosition();
        void UpdateVisualizerPosition();
        void StartVisualizePosition();
    }

    public class TurretPositionVisualizer : MonoBehaviour, ITurretPositionVisualizer
    {
        [SerializeField] private GameObject turretPositionVisualizingPrefab;
        [SerializeField] private Transform turretPosition;
        [SerializeField] private TurretPlacementInputSource turretPlacementInputSource;
        [SerializeField] private TurretPositionPresenterFactory presenterFactory;

        private GameObject _currentVisualizer;
        private ITurretPositionPresenter _presenter;

        private void OnEnable()
        {
            _presenter = presenterFactory.CreatePresenter(this, turretPlacementInputSource);
        }

        private void Update()
        {
            _presenter.OnUpdate();
        }

        public void UpdateVisualizerPosition()
        {
            _currentVisualizer.transform.position = turretPosition.position;
            _currentVisualizer.transform.rotation = turretPosition.rotation;
        }

        public void StartVisualizePosition()
        {
            _currentVisualizer = Instantiate(turretPositionVisualizingPrefab, turretPosition);
        }

        public void FinishVisualizePosition()
        {
            Destroy(_currentVisualizer);
        }
        
    }
}