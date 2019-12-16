using System;
using AgentAi;
using AgentAi.Manager;
using Common.Class;
using Common.Event;
using EventManagement;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSpecificUtils
{
    public class ObservedTextureDisplayer : MonoBehaviour, IHandle<GameStartEvent>
    {
        private IEventAggregator _eventAggregator;
        private Image _image;
        private Rect _rect;
        private SpriteRenderer _spriteRenderer;

        private float _timer;

        [SerializeField] private EnemyAgentObservationCollector enemyAgentObservationCollector;

        //interface cannot be serialized, do a dirty cast when using this
        [SerializeField] private MonoBehaviour maybeCanObserve;
        [SerializeField] private float observeFrequency;
        [SerializeField] private bool startedObserving;

        public void Handle(GameStartEvent @event)
        {
            startedObserving = true;
        }

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _image = GetComponent<Image>();
            var size = new Vector2(enemyAgentObservationCollector.TextureDimension, enemyAgentObservationCollector.TextureDimension);
            _rect = new Rect(Vector2.zero, size);

            _eventAggregator.Subscribe(this);

            if (_image == null && _spriteRenderer == null)
            {
                throw new ArgumentException("Mate at least have something so that I can actually show you what is rendered");
            }
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }

        private void FixedUpdate()
        {
            var canObserve = TryGetCanObserveEnvironment();
            if (startedObserving && canObserve != null)
            {
                _timer += Time.deltaTime;
                if (_timer < observeFrequency)
                {
                    return;
                }

                _timer = 0f;

                var texture = canObserve.GetObservation();
                var sprite = Sprite.Create(texture, _rect, new Vector2(0.5f, 0.5f));
                if (_spriteRenderer != null)
                {
                    _spriteRenderer.sprite = sprite;
                }

                if (_image != null)
                {
                    _image.sprite = sprite;
                }
            }
        }

        [CanBeNull]
        private ICanObserveEnvironment TryGetCanObserveEnvironment()
        {
            if (maybeCanObserve is ICanObserveEnvironment canObserveEnvironment)
            {
                return canObserveEnvironment;
            }

            return maybeCanObserve.GetComponent<ICanObserveEnvironment>();
        }
    }
}