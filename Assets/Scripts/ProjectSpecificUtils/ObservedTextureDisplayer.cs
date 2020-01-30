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
        private IEventAggregator _eventAggregator;

        private float _timer;
        [SerializeField] private bool automaticUpdateObserver;
        //interface cannot be serialized, do a dirty cast when using this
        [SerializeField] private MonoBehaviour maybeCanObserve;
        [SerializeField] private float observeFrequency;
        [SerializeField] private bool startedObserving;
        [SerializeField] [Range(1, 100)] private float scale;
        [SerializeField] private bool markCentreWithMagenta;
        

        public void Handle(GameStartEvent @event)
        {
            startedObserving = true;
        }

        public void Handle(IDynamicObjectSpawnedEvent @event)
        {
            if (!automaticUpdateObserver)
            {
                return;
            }

            var canObserverEnvironment = @event.DynamicObject.DynamicObjectTransform.GetComponent<ICanObserveEnvironment>();
            if (canObserverEnvironment != null)
            {
                maybeCanObserve = canObserverEnvironment as MonoBehaviour;
            }
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

        private void OnGUI()
        {
            if (startedObserving)
            {
                var canObserve = GetCanObserveEnvironment();

                if (canObserve == null)
                {
                    return;
                }

                _timer += Time.deltaTime;
                if (_timer < observeFrequency)
                {
                    return;
                }

                _timer = 0f;

                var texture = canObserve.GetObservation();
                if (markCentreWithMagenta)
                {
                    texture.SetPixel(texture.width/2, texture.height/2, Color.magenta);
                    texture.Apply();
                }

                GUI.DrawTexture(new Rect(Screen.width - texture.width * scale, 0f, texture.width * scale, texture.height * scale), texture);
            }
        }

        [CanBeNull]
        private ICanObserveEnvironment GetCanObserveEnvironment()
        {
            if (maybeCanObserve == null)
            {
                return null;
            }

            if (maybeCanObserve is ICanObserveEnvironment canObserveEnvironment)
            {
                return canObserveEnvironment;
            }

            return maybeCanObserve.GetComponent<ICanObserveEnvironment>();
        }
    }
}