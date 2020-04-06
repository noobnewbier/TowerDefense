using AgentAi.Suicidal.HierarchyDeprecated.Event;
using AgentAi.Suicidal.HierarchyDeprecated.TargetPicker;
using Common.Constant;
using Common.Interface;
using Elements.Units.UnitCommon;
using EventManagement;
using EventManagement.Providers;
using Experimental;
using UnityEngine;

namespace AgentAi.Suicidal.HierarchyDeprecated.Navigator
{
    /// <summary>
    ///     Used to train with <see cref="SuicidalUnitRoutePlannerAgent" />
    /// </summary>
    public class InstantWalkStraightLineAgent : MonoBehaviour, INavigator
    
    {
        private IEventAggregator _localEventAggregator;
        private LayerMask _obstacleLayerMask;
        private IObjectOfInterest _target;
        private Unit _unit;

        [SerializeField] private LocalEventAggregatorProvider localEventAggregatorProvider;
        [SerializeField] private UnitProvider unitProvider;

        public void Handle(NewTargetIssuedEvent @event)
        {
            _target = @event.Target;
        }

        private void OnEnable()
        {
            _unit = unitProvider.ProvideUnit();
            _obstacleLayerMask = 1 << LayerMask.NameToLayer(LayerNames.Obstacle);
            _localEventAggregator = localEventAggregatorProvider.ProvideEventAggregator();

            _localEventAggregator.Subscribe(this);
            _localEventAggregator.Publish(new RequestNewTargetEvent());
        }

        private void FixedUpdate()
        {
            if (_target != null) MoveToDestination();
        }

        private void MoveToDestination()
        {
            var currentPosition = _unit.transform.position;
            var targetPosition = _target.ObjectTransform.position;
            var maximumDistance = Vector3.Distance(targetPosition, currentPosition);
            var direction = Vector3.Normalize(targetPosition - currentPosition);

            //raycast to destination, if there are obstacles midway, stop there
            if (Physics.Raycast(
                currentPosition,
                direction,
                out var hit,
                maximumDistance,
                _obstacleLayerMask,
                QueryTriggerInteraction.Ignore
            ))
            {
                _unit.transform.position = hit.point + -direction * _unit.Bounds.extents.magnitude;
                _localEventAggregator.Publish(new FinishPathingEvent(false));
            }
            else
            {
                _unit.transform.position = targetPosition;
                _localEventAggregator.Publish(new FinishPathingEvent(true));
            }

            _localEventAggregator.Publish(new RequestNewTargetEvent());
        }
    }
}