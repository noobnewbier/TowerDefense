using System;
using System.Collections.Generic;
using System.Linq;
using Common.Class;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Elements.Units.UnitCommon;
using EventManagement;
using JetBrains.Annotations;
using UnityEngine;
using UnityUtils;

namespace AgentAi.Manager
{
    public interface IObserveEnvironmentService
    {
        Texture2D CreateObservationAsTexture(Unit observer, IDynamicObjectOfInterest target);
    }

    // Perhaps the main bottleneck... be careful with this
    public class EnemyAgentObservationCollector : MonoBehaviour, IHandle<GameStartEvent>, IObserveEnvironmentService
    {
        private Vector3 _centerOfTexture;
        private int[,] _coordinatesWithPriority;
        private IEventAggregator _eventAggregator;
        private Texture2D _observedTexture;
        private Texture2D _terrainTexture;
        private int _textureDimension;
        [SerializeField] private EnemyAgentObservationConfig config;

        [SerializeField] private ObjectsOfInterestTracker objectsOfInterestTracker;
        public static IObserveEnvironmentService Instance { get; private set; }

        public void Handle(GameStartEvent @event)
        {
            SetupTextures();
        }

        public bool GrayScale => config.GrayScale;

        public Texture2D CreateObservationAsTexture(Unit observer, [CanBeNull] IDynamicObjectOfInterest target)
        {
            var objectsWithTargetAndObserver = objectsOfInterestTracker.DynamicObjectOfInterests
                .ReplaceAll(observer, GetObserverRepresentation(observer));

            if (target != null)
            {
                objectsWithTargetAndObserver = objectsWithTargetAndObserver
                    .ReplaceAll(target, GetTargetRepresentation(target));
            }

            //if this is too slow, rotate before writing
            Graphics.CopyTexture(_terrainTexture, _observedTexture);
            DrawObjectsOnTexture(_observedTexture, objectsWithTargetAndObserver, false);

            var observerPosition = observer.transform.position;
            _observedTexture.TranslateTexture((int) (observerPosition.x * config.Precision), (int) (observerPosition.z * config.Precision));
            _observedTexture.RotateTexture(-observer.transform.rotation.eulerAngles.y, AiInterestCategory.NullArea.Color);

            return Instantiate(_observedTexture);
        }

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
                Instance = this;

            _textureDimension = config.CalculateTextureDimension();

            _centerOfTexture = new Vector3(_textureDimension / 2f, 0, _textureDimension / 2f);
            _terrainTexture = new Texture2D(_textureDimension, _textureDimension, TextureFormat.RGB24, false);

            _observedTexture = new Texture2D(_textureDimension, _textureDimension, TextureFormat.RGB24, false);
            _coordinatesWithPriority = new int[_textureDimension, _textureDimension];
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

        private void SetupTextures()
        {
            var interestedObjects = objectsOfInterestTracker.StaticObjectOfInterests;

            //default to null area
            var nullColors = Enumerable.Repeat(AiInterestCategory.NullArea.Color, _textureDimension * _textureDimension).ToArray();
            _terrainTexture.SetPixels(nullColors);

            DrawObjectsOnTexture(_terrainTexture, interestedObjects, true);
        }

        private void DrawObjectsOnTexture(Texture2D texture2D, IEnumerable<IObjectOfInterest> interestedObjects, bool shouldWritePriority)
        {
            interestedObjects = interestedObjects.ToList().OrderBy(i => i.InterestCategory.Priority);

            foreach (var objectOfInterest in interestedObjects)
            {
                var rescaledBounds = RescaleBoundsToTexture(objectOfInterest.Bounds);
                try
                {
                    objectOfInterest.InterestCategory.Drawer.DrawObjectWithPriority(
                        texture2D,
                        rescaledBounds,
                        objectOfInterest.InterestCategory.Color,
                        _coordinatesWithPriority,
                        objectOfInterest.InterestCategory.Priority,
                        shouldWritePriority
                    );
                }
                catch (IndexOutOfRangeException e)
                {
                    Debug.Log(e);
                    Debug.Log($"{objectOfInterest} is going out of predefined area, Rescaled Bounds: {objectOfInterest.Bounds}");
                }
            }

            //not very sure if we need this or not
            texture2D.Apply();
        }

        //technically we will be fine without returning a new one.... we will optimize when we need
        private Bounds RescaleBoundsToTexture(Bounds bounds)
        {
            bounds.center *= config.Precision;
            bounds.center += _centerOfTexture;
            bounds.size *= config.Precision;

            return bounds;
        }

        private static IDynamicObjectOfInterest GetObserverRepresentation(IObjectOfInterest dynamicObjectOfInterest) => new Observer(dynamicObjectOfInterest);
        private static IDynamicObjectOfInterest GetTargetRepresentation(IObjectOfInterest dynamicObjectOfInterest) => new Target(dynamicObjectOfInterest);

        private class Observer : IDynamicObjectOfInterest
        {
            public Observer(IObjectOfInterest objectOfInterest)
            {
                Bounds = objectOfInterest.Bounds;
                DynamicObjectTransform = objectOfInterest.DynamicObjectTransform;
            }

            public AiInterestCategory InterestCategory => AiInterestCategory.Observer;
            public Bounds Bounds { get; }
            public Transform DynamicObjectTransform { get; }
        }

        private class Target : IDynamicObjectOfInterest
        {
            public Target(IObjectOfInterest objectOfInterest)
            {
                Bounds = objectOfInterest.Bounds;
                DynamicObjectTransform = objectOfInterest.DynamicObjectTransform;
            }

            public AiInterestCategory InterestCategory => AiInterestCategory.Target;
            public Bounds Bounds { get; }
            public Transform DynamicObjectTransform { get; }
        }
    }
}