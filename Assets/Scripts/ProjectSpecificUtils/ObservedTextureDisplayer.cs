using System;
using AgentAi;
using Common.Class;
using Common.Event;
using Elements.Units.UnitCommon;
using EventManagement;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSpecificUtils
{
    public class ObservedTextureDisplayer : MonoBehaviour, IHandle<GameStartEvent>
    {
        private IEventAggregator _eventAggregator;
        private Image _image;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private bool startedObserving;
        [SerializeField] private EnemyAgentObservationCollector enemyAgentObservationCollector;
        [SerializeField] private Unit dummyUnit;

        public void Handle(GameStartEvent @event)
        {
            startedObserving = true;
        }

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _image = GetComponent<Image>();

            _eventAggregator.Subscribe(this);

            if (_image == null && _spriteRenderer == null)
                throw new ArgumentException("Mate at least have something so that I can actually show you what is rendered");
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }

        private void FixedUpdate()
        {
            if (startedObserving)
            {
                var texture = enemyAgentObservationCollector.ObserveEnvironment(dummyUnit);
                var sprite = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(60f, 60f)), new Vector2(0.5f, 0.5f));
                if (_spriteRenderer != null) _spriteRenderer.sprite = sprite;
                if (_image != null) _image.sprite = sprite;
            }
        }
    }
}