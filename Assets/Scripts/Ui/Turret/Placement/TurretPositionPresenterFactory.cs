using Elements.Turret.Placement.InputSource;
using UnityEngine;

namespace Ui.Turret.Placement
{
    [CreateAssetMenu(menuName = "ScriptableFactory/TurretPositionPresenter")]
    public class TurretPositionPresenterFactory : ScriptableObject
    {
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public ITurretPositionPresenter CreatePresenter(ITurretPositionVisualizer visualizer, ITurretPlacementInputSource inputSource)
        {
            return new TurretPositionPresenter(visualizer, inputSource);
        }
    }
}