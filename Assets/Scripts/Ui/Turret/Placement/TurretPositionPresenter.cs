using Elements.Turret.Placement;
using Elements.Turret.Placement.InputSource;
using ScriptableService;
using UnityEngine;

namespace Ui.Turret.Placement
{
    public interface ITurretPositionPresenter
    {
        void OnUpdate();
    }

    public class TurretPositionPresenter : ITurretPositionPresenter
    {
        private readonly ITurretPlacementInputSource _inputSource;
        private readonly Transform _spawnpoint;
        private readonly ISpawnPointValidator _spawnPointValidator;
        private readonly TurretPlacementControlModel _turretPlacementControlModel;
        private readonly ITurretPositionVisualizer _visualizer;
        private readonly Vector3 _spawnpointMargin;

        private bool _isVisualizing;

        public TurretPositionPresenter
        (
            ITurretPositionVisualizer visualizer,
            ITurretPlacementInputSource inputSource,
            ISpawnPointValidator spawnPointValidator,
            Transform spawnpoint,
            TurretPlacementControlModel turretPlacementControlModel,
            Vector3 spawnpointMargin
        )
        {
            _visualizer = visualizer;
            _inputSource = inputSource;
            _spawnPointValidator = spawnPointValidator;
            _spawnpoint = spawnpoint;
            _turretPlacementControlModel = turretPlacementControlModel;
            _spawnpointMargin = spawnpointMargin;
        }

        public void OnUpdate()
        {
            if (_inputSource.ReceivedPendingTurretPlacementInput() && !_isVisualizing)
                OnBeginPlacingTurret();
            else if (_isVisualizing)
                CheckSpawnPointValidity();

            if (!_inputSource.ReceivedPendingTurretPlacementInput() && _isVisualizing) OnFinishPlacingTurret();
        }

        private void OnBeginPlacingTurret()
        {
            _visualizer.StartVisualizePosition(IsSpawnpointValid());

            _isVisualizing = true;
        }

        private void OnFinishPlacingTurret()
        {
            _visualizer.FinishVisualizePosition();

            _isVisualizing = false;
        }

        private bool IsSpawnpointValid()
        {
            //will be wrong if local scale no longer aligns with actual size
            var isValid = _spawnPointValidator.IsSpawnPointValid(
                _spawnpoint.position,
                _turretPlacementControlModel.TurretProvider.HalfSize + _spawnpointMargin,
                _spawnpoint.rotation
            );

            return isValid;
        }

        private void CheckSpawnPointValidity()
        {
            if (IsSpawnpointValid())
                _visualizer.OnValidSpawnpoint();
            else
                _visualizer.OnInvalidSpawnpoint();
        }
    }
}