using System;
using System.Diagnostics.CodeAnalysis;
using AgentAi.Manager;
using AgentAi.Suicidal.Hierarchy.Config;
using AgentAi.Suicidal.Hierarchy.Event;
using Common.Enum;
using Common.Event;
using Elements.Units.UnitCommon;
using EventManagement;
using Experimental;
using MLAgents;
using UnityEngine;

namespace AgentAi.Suicidal.Hierarchy.TargetPicker
{
    public class SuicidalUnitRoutePlannerAgent : Agent, IHandle<EnemyDeadEvent>, ICanObserveEnvironment, ITargetPicker
    {
        private IEventAggregator _globalEventAggregator;
        private IEventAggregator _localEventAggregator;
        private IObserveEnvironmentService _observeEnvironmentService;
        private TargetMarker _targetMarker;
        private Unit _unit;

        [SerializeField] private SuicidalUnitRoutePlannerConfig config;
        [SerializeField] private EventAggregatorProvider globalEventAggregatorProvider;
        [SerializeField] private LocalEventAggregatorProvider localEventAggregatorProvider;
        [SerializeField] private ObservationServiceProvider observationServiceProvider;
        [SerializeField] private UnitProvider unitProvider;

        public Texture2D GetObservation()
        {
            return _observeEnvironmentService.CreateObservationAsTexture(_unit, _targetMarker);
        }

        public void Handle(EnemyDeadEvent @event)
        {
            if (@event.Enemy != _unit) return;

            RewardIsDead(@event.DeathCause);
        }

        public void Handle(FinishPathingEvent @event)
        {
            AddReward(@event.PreviousTargetReached ? config.ArrivedTargetReward : config.CollisionPunishment);
        }

        public void Handle(RequestNewTargetEvent @event)
        {
            RequestDecision();
        }

        public override float[] Heuristic()
        {
            var position = _targetMarker.ObjectTransform.position /
                           (_observeEnvironmentService.Config.MapDimension / 2f);
            return new[] {position.x, position.z};
        }

        public override void InitializeAgent()
        {
            base.InitializeAgent();
            _globalEventAggregator = globalEventAggregatorProvider.ProvideEventAggregator();
            _localEventAggregator = localEventAggregatorProvider.ProvideEventAggregator();
            _observeEnvironmentService = observationServiceProvider.ProvideService();
            _unit = unitProvider.ProvideUnit();

            _targetMarker =
                new GameObject("Target Marker").AddComponent<TargetMarker>();
            _targetMarker.transform.position = _unit.transform.position;

            _globalEventAggregator.Subscribe(this);
            _localEventAggregator.Subscribe(this);

            _localEventAggregator.Publish(new SubAgentSpawnedEvent());
        }

        public override void AgentOnDone()
        {
            base.AgentOnDone();
            _localEventAggregator.Publish(new SubAgentDoneEvent());

            _localEventAggregator.Unsubscribe(this);
            _globalEventAggregator.Unsubscribe(this);
            
            Destroy(_targetMarker.gameObject);
        }

        public override void AgentAction(float[] vectorAction)
        {
            var newTargetPosition = InputToTargetPosition(vectorAction[0], vectorAction[1]);

            var targetMarkerTransform = _targetMarker.ObjectTransform;
            targetMarkerTransform.position = new Vector3(
                newTargetPosition.x,
                targetMarkerTransform.position.y,
                newTargetPosition.y
            );

            PunishRoaming();

            _localEventAggregator.Publish(new NewTargetIssuedEvent(_targetMarker));

            Debug.Log(GetCumulativeReward());
        }

        [SuppressMessage("ReSharper", "RedundantCaseLabel")]
        private void RewardIsDead(EffectSource deathCause)
        {
            switch (deathCause)
            {
                case EffectSource.Player:
                    Debug.Log("rewarded with player");
                    AddReward(config.KilledPunishment);
                    break;
                case EffectSource.System:
                    break;
                case EffectSource.SelfDestruction:
                    AddReward(config.SelfDestructionReward);
                    break;
                case EffectSource.Ai:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Done();
        }

        //Don't walk around forever pls
        private void PunishRoaming()
        {
            AddReward(config.RoamingPunishment);
        }

        private Vector2 InputToTargetPosition(float x, float y)
        {
            var xCord = Mathf.RoundToInt(
                Mathf.Clamp(x, -1, 1) * (_observeEnvironmentService.Config.MapDimension / 2f)
            );
            var yCord = Mathf.RoundToInt(
                Mathf.Clamp(y, -1, 1) * (_observeEnvironmentService.Config.MapDimension / 2f)
            );
            
            return new Vector2(xCord, yCord);
        }
    }
}