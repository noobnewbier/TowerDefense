using AgentAi;
using Common.Class;
using Common.Event;
using EventManagement;
using JetBrains.Annotations;
using UnityEngine;

namespace ProjectSpecificUtils
{
    public class ObservedTextureDisplayer : MonoBehaviour, IHandle<GameStartEvent>, IHandle<IDynamicObjectSpawnedEvent>
    {
        private Texture2D _currentTexture;
        private IEventAggregator _eventAggregator;

        private float _timer;
        [SerializeField] private bool automaticUpdateObserver;

        [SerializeField] private bool markCentreWithMagenta;

        //interface cannot be serialized, do a dirty cast when using this
        [SerializeField] private MonoBehaviour maybeCanObserve;
        [SerializeField] private float observeFrequency;
        [SerializeField] [Range(1, 100)] private float scale;
        [SerializeField] private bool startedObserving;
        [SerializeField] private TextureSavingService textureSavingService;


        public void Handle(GameStartEvent @event)
        {
            startedObserving = true;
        }

        public void Handle(IDynamicObjectSpawnedEvent @event)
        {
            if (!automaticUpdateObserver) return;

            var canObserverEnvironment =
                @event.DynamicObject.ObjectTransform.GetComponentInChildren<ICanObserveEnvironment>();
            if (canObserverEnvironment != null) maybeCanObserve = canObserverEnvironment as MonoBehaviour;
        }

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }

        [ContextMenu("SaveDisplayedTexture")]
        private void SaveDisplayedTexture()
        {
            textureSavingService.SaveTexture(_currentTexture, "displayedTexture");
        }

        private void OnGUI()
        {
            if (startedObserving)
            {
                var canObserve = GetCanObserveEnvironment();

                if (canObserve == null) return;

                _timer += Time.deltaTime;
                if (_timer < observeFrequency) return;

                _timer = 0f;

                _currentTexture = canObserve.GetObservation();
                if (markCentreWithMagenta)
                {
                    _currentTexture.SetPixel(_currentTexture.width / 2, _currentTexture.height / 2, Color.magenta);
                    _currentTexture.Apply();
                }

                GUI.DrawTexture(
                    new Rect(
                        Screen.width - _currentTexture.width * scale,
                        0f,
                        _currentTexture.width * scale,
                        _currentTexture.height * scale
                    ),
                    _currentTexture
                );
            }
        }

        [CanBeNull]
        private ICanObserveEnvironment GetCanObserveEnvironment()
        {
            if (maybeCanObserve == null) return null;

            if (maybeCanObserve is ICanObserveEnvironment canObserveEnvironment) return canObserveEnvironment;

            return maybeCanObserve.GetComponent<ICanObserveEnvironment>();
        }
    }
}