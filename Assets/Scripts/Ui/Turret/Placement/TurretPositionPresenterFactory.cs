using Elements.Turret.Placement;
using Elements.Turret.Placement.InputSource;
using ScriptableService;
using UnityEngine;

namespace Ui.Turret.Placement
{
    [CreateAssetMenu(menuName = "ScriptableFactory/TurretPositionPresenter")]
    public class TurretPositionPresenterFactory : ScriptableObject
    {
        [SerializeField] private SpawnPointValidator spawnPointValidator;
        [SerializeField] private TurretPlacementControlModel placemenControlModel;

        // ReSharper disable once MemberCanBeMadeStatic.Global
        public ITurretPositionPresenter CreatePresenter
        (
            ITurretPositionVisualizer visualizer,
            ITurretPlacementInputSource inputSource,
            Transform spawnpoint,
            Vector3 spawnpointMargin
        ) =>
            new TurretPositionPresenter(visualizer, inputSource, spawnPointValidator, spawnpoint, placemenControlModel, spawnpointMargin);
    }
}