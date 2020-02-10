using Elements.Turret.Placement.InputSource;
using ScriptableService;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.Turret.Placement
{
    public interface ITurretPositionVisualizer
    {
        void FinishVisualizePosition();
        void StartVisualizePosition(bool initialSpawnpointValidity);
        void OnInvalidSpawnpoint();
        void OnValidSpawnpoint();
    }

    public class TurretPositionVisualizer : MonoBehaviour, ITurretPositionVisualizer
    {
        [SerializeField] private GameObject validTurretPositionIndicator;
        [SerializeField] private GameObject invalidTurretPositionIndicator;
        [SerializeField] private Transform turretPosition;
        [SerializeField] private TurretPlacementInputSource turretPlacementInputSource;
        [SerializeField] private TurretPositionPresenterFactory presenterFactory;
        [SerializeField] private Vector3 spawnpointMargin;
        

        private GameObject _currentIndicator;
        private ITurretPositionPresenter _presenter;

        private void OnEnable()
        {
            _presenter = presenterFactory.CreatePresenter(this, turretPlacementInputSource, turretPosition, spawnpointMargin);
        }

        private void Update()
        {
            _presenter.OnUpdate();
        }

        public void StartVisualizePosition(bool initialSpawnpointValidity)
        {
            _currentIndicator = initialSpawnpointValidity ? validTurretPositionIndicator : invalidTurretPositionIndicator;
            
            _currentIndicator.SetActive(true);
        }

        public void OnInvalidSpawnpoint()
        {
            _currentIndicator = invalidTurretPositionIndicator;
            _currentIndicator.SetActive(true);
            validTurretPositionIndicator.SetActive(false);
        }

        public void OnValidSpawnpoint()
        {
            _currentIndicator = validTurretPositionIndicator;
            _currentIndicator.SetActive(true);
            invalidTurretPositionIndicator.SetActive(false);
        }

        public void FinishVisualizePosition()
        {
            _currentIndicator.SetActive(false);
        }
    }
}