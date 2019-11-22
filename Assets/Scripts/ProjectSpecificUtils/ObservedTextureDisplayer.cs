using System;
using AgentAi;
using Common.Class;
using Common.Event;
using EventManagement;
using Units.Enemies;
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

        //todo: fix this crap
        private void FixedUpdate()
        {
            if (_startedObserving)
            {
                var sprite = _enemyAgentObservationCollector.ObserveEnvironment(dummyUnit);
                _spriteRenderer.sprite = Sprite.Create(sprite,  new Rect(transform.position, new Vector2(60f, 60f)), new Vector2(0, 0));
            }
        }
    }
}