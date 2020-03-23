using System.Linq;
using AgentAi.Record;
using AgentAi.Suicidal;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Common.Struct;
using Elements.Units.UnitCommon;
using EventManagement;
using Experimental;
using JetBrains.Annotations;
using UnityEngine;
using UnityUtils;

namespace AgentAi.Manager
{
    public interface IObserveEnvironmentService
    {
        EnemyAgentObservationConfig Config { get; }
        Texture2D CreateObservationAsTexture(Unit observer, IDynamicObjectOfInterest target);
    }

    // Perhaps the main bottleneck... be careful with this
    [CreateAssetMenu(menuName = "ScriptableService/EnemyAgentObservationService")]
    public class EnemyAgentObservationService : ScriptableObject, IHandle<GameStartEvent>, IObserveEnvironmentService, IHandle<WaveStartEvent>, IHandle<WaveEndEvent>
    {
        private int[,] _coordinatesWithPriority;
        private EnvironmentDrawConfig _drawingConfig;
        private IEventAggregator _eventAggregator;
        private Texture2D _observedTexture;
        private Texture2D _terrainTexture;
        private int _textureDimension;
        [SerializeField] private EnemyAgentObservationConfig config;
        [SerializeField] private EnvironmentToTextureService environmentToTextureService;
        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [SerializeField] private ObjectsOfInterestTracker objectsOfInterestTracker;
        [SerializeField] private EnvironmentRecorder environmentRecorder;        
        [SerializeField] private bool logEnvironment;
        
        public void Handle(GameStartEvent @event)
        {
            SetupTextures();
            if (logEnvironment)
            {
                environmentRecorder.CreateNewRecord();
            }
        }

        public Texture2D CreateObservationAsTexture(Unit observer, [CanBeNull] IDynamicObjectOfInterest target)
        {
            var objectsWithTargetAndObserver = objectsOfInterestTracker.DynamicObjectOfInterests
                .ReplaceAll(observer, GetObserverRepresentation(observer));

            if (target != null)
                objectsWithTargetAndObserver = objectsWithTargetAndObserver
                    .ReplaceAll(target, GetTargetRepresentation(target));

            //if this is too slow, rotate before writing
            Graphics.CopyTexture(_terrainTexture, _observedTexture);
            
            var dynamicObjectOfInterests = objectsWithTargetAndObserver.ToList();
            environmentToTextureService.DrawObjectsOnTexture(
                _observedTexture,
                dynamicObjectOfInterests.Select(o => o.InterestedInformation),
                _drawingConfig.CategoryAndColors,
                _drawingConfig.CategoryAndPriority,
                _drawingConfig.CategoryAndDrawer,
                _coordinatesWithPriority,
                config.Precision,
                false
            );

            var observerPosition = observer.transform.position;
            if (config.UseTranslation)
                _observedTexture.TranslateTexture(
                    (int) (observerPosition.x * config.Precision),
                    (int) (observerPosition.z * config.Precision)
                );

            if (config.UseTextureRotation)
                _observedTexture.RotateTexture(
                    -observer.transform.rotation.eulerAngles.y,
                    _drawingConfig.CategoryAndColors[InterestCategory.NullArea]
                );

            if (logEnvironment)
            {
                environmentRecorder.AddCurrentStep(dynamicObjectOfInterests, observer);
            }

            return Instantiate(_observedTexture);
        }

        public EnemyAgentObservationConfig Config => config;

        private void OnEnable()
        {
            _textureDimension = config.GetTextureDimension();
            _eventAggregator = eventAggregatorProvider.ProvideEventAggregator();
            _drawingConfig = config.DrawingConfig;

            _terrainTexture = new Texture2D(_textureDimension, _textureDimension, TextureFormat.RGB24, false);
            _observedTexture = new Texture2D(_textureDimension, _textureDimension, TextureFormat.RGB24, false);
            _coordinatesWithPriority = new int[_textureDimension, _textureDimension];

            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }

        private void SetupTextures()
        {
            var interestedObjects = objectsOfInterestTracker.StaticObjectOfInterests;

            //default to null area
            var nullColors = Enumerable.Repeat(
                    _drawingConfig.CategoryAndColors[InterestCategory.NullArea],
                    _textureDimension * _textureDimension
                )
                .ToArray();
            _terrainTexture.SetPixels(nullColors);

            environmentToTextureService.DrawObjectsOnTexture(
                _terrainTexture,
                interestedObjects.Select(i => i.InterestedInformation),
                _drawingConfig.CategoryAndColors,
                _drawingConfig.CategoryAndPriority,
                _drawingConfig.CategoryAndDrawer,
                _coordinatesWithPriority,
                config.Precision,
                true
            );
        }

        private static IDynamicObjectOfInterest GetObserverRepresentation(IObjectOfInterest dynamicObjectOfInterest)
        {
            return new Observer(dynamicObjectOfInterest);
        }

        private static IDynamicObjectOfInterest GetTargetRepresentation(IObjectOfInterest dynamicObjectOfInterest)
        {
            return new Target(dynamicObjectOfInterest);
        }

        private class Observer : IDynamicObjectOfInterest
        {
            public Observer(IObjectOfInterest objectOfInterest)
            {
                ObjectTransform = objectOfInterest.ObjectTransform;
                InterestedInformation = new InterestedInformation(
                    InterestCategory.Observer,
                    objectOfInterest.InterestedInformation.Bounds
                );
            }

            public InterestedInformation InterestedInformation { get; }
            public Transform ObjectTransform { get; }
        }

        private class Target : IDynamicObjectOfInterest
        {
            public Target(IObjectOfInterest objectOfInterest)
            {
                ObjectTransform = objectOfInterest.ObjectTransform;
                InterestedInformation = new InterestedInformation(
                    InterestCategory.Target,
                    objectOfInterest.InterestedInformation.Bounds
                );
            }

            public InterestedInformation InterestedInformation { get; }
            public Transform ObjectTransform { get; }
        }

        public void Handle(WaveStartEvent @event)
        {
            environmentRecorder.CreateNewRound(objectsOfInterestTracker.StaticObjectOfInterests);
        }

        public void Handle(WaveEndEvent @event)
        {
            environmentRecorder.EndRound();
        }
    }
}