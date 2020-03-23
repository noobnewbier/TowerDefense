using AgentAi.Suicidal.Hierarchy.Event;
using AgentAi.Suicidal.Hierarchy.Navigator;
using Common.Constant;
using Common.Interface;
using Elements.Units.UnitCommon;
using EventManagement;
using Experimental;
using UnityEngine;

namespace AgentAi.Suicidal.HierarchyDeprecated.Navigator
{
    public class DebugWalkStraightLineAgent : MonoBehaviour, INavigator
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

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 60, 125, 20), "MoveToDestination")) MoveToDestination();
        }
    }
}