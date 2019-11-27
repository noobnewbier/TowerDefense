using System;
using AgentAi;
using Common.Class;
using Common.Event;
using Elements.Units.Enemies;
using EventManagement;
using UnityEngine;

namespace ProjectSpecificUtils
{
    [RequireComponent(typeof(EnemyAgentObservationCollector))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObservedTextureDisplayer : MonoBehaviour, IHandle<GameStartEvent>
    {
        private IEventAggregator _eventAggregator;
        private SpriteRenderer _spriteRenderer;
        private EnemyAgentObservationCollector _enemyAgentObservationCollector;
        private bool _startedObserving;
        [SerializeField] private DummyUnit dummyUnit;

        public void Handle(GameStartEvent @event)
        {
            _startedObserving = true;
        }

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _enemyAgentObservationCollector = GetComponent<EnemyAgentObservationCollector>();

            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }

        private void FixedUpdate()
        {
            if (_startedObserving)
            {
                var sprite = _enemyAgentObservationCollector.ObserveEnvironment(dummyUnit);
                _spriteRenderer.sprite = Sprite.Create(sprite,  new Rect(Vector2.zero, new Vector2(60f, 60f)), new Vector2(0.5f, 0.5f));
            }
        }
    }
}