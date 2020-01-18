using Elements.Turret.Placement.InputSource;

namespace Ui.Turret.Placement
{
    public interface ITurretPositionPresenter
    {
        void OnUpdate();
    }

    public class TurretPositionPresenter : ITurretPositionPresenter
    {
        private readonly ITurretPlacementInputSource _inputSource;
        private readonly ITurretPositionVisualizer _visualizer;

        private bool _isVisualizing;

        public TurretPositionPresenter(ITurretPositionVisualizer visualizer, ITurretPlacementInputSource inputSource)
        {
            _visualizer = visualizer;
            _inputSource = inputSource;
        }

        public void OnUpdate()
        {
            if (_inputSource.ReceivedPendingTurretPlacementInput() && !_isVisualizing)
            {
                if (_isVisualizing)
                    OnHoldPlacingTurret();
                else
                    OnBeginPlacingTurret();
            }

            if (!_inputSource.ReceivedPendingTurretPlacementInput() && _isVisualizing) OnFinishPlacingTurret();
        }

        private void OnBeginPlacingTurret()
        {
            _visualizer.StartVisualizePosition();

            _isVisualizing = true;
        }

        private void OnFinishPlacingTurret()
        {
            _visualizer.FinishVisualizePosition();

            _isVisualizing = false;
        }

        private void OnHoldPlacingTurret()
        {
            _visualizer.UpdateVisualizerPosition();
        }
    }
}