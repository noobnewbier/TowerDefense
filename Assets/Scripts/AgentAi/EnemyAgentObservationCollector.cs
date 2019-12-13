using System;
using System.Collections.Generic;
using System.Linq;
using Common.Class;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Elements.Units.UnitCommon;
using EventManagement;
using UnityEngine;
using UnityUtils;

namespace AgentAi
{
    public interface ICanObserveEnvironment
    {
        Texture2D ObserveEnvironment(Unit unit);
        int[] Shape { get; }
    }

    // Perhaps the main bottleneck... be careful with this
    public class EnemyAgentObservationCollector : MonoBehaviour, IHandle<GameStartEvent>, IHandle<IDynamicObjectDestroyedEvent>,
        IHandle<IDynamicObjectSpawnedEvent>, ICanObserveEnvironment
    {
        public static ICanObserveEnvironment Instance;

        private Vector3 _centerOfTexture;
        private int[,] _coordinatesWithPriority;
        private IList<IDynamicObjectOfInterest> _dynamicObjects;
        private IEventAggregator _eventAggregator;
        private Texture2D _observedTexture;
        private Texture2D _terrainTexture;

        [SerializeField] [Range(1, 200)] private int mapDimension;
        public int TextureDimension { get; private set; }


        public int[] Shape => new[] {TextureDimension, TextureDimension, 3};

        public Texture2D ObserveEnvironment(Unit unit)
        {
            //if this is too slow, rotate before writing
            Graphics.CopyTexture(_terrainTexture, _observedTexture);
            DrawObjectsOnTexture(_observedTexture, _dynamicObjects.ReplaceAll(unit, GetObserverRepresentation(unit)), false);
//            _observedTexture.RotateTexture(unit.transform.eulerAngles.y - 90);

            return Instantiate(_observedTexture);
        }

        public void Handle(GameStartEvent @event)
        {
            SetupTextures();
        }

        public void Handle(IDynamicObjectDestroyedEvent @event)
        {
            _dynamicObjects.Remove(@event.DynamicObject);
        }

        public void Handle(IDynamicObjectSpawnedEvent @event)
        {
            _dynamicObjects.Add(@event.DynamicObject);
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

//            TextureDimension = Mathf.CeilToInt(Mathf.Sqrt(Mathf.Pow(mapDimension, 2) * 2));
            TextureDimension = mapDimension;

            _centerOfTexture = new Vector3(TextureDimension / 2f, 0, TextureDimension / 2f);
            _terrainTexture = new Texture2D(TextureDimension, TextureDimension, TextureFormat.RGB24, false);
            _observedTexture = new Texture2D(TextureDimension, TextureDimension, TextureFormat.RGB24, false);
            _dynamicObjects = new List<IDynamicObjectOfInterest>();
            _coordinatesWithPriority = new int[TextureDimension, TextureDimension];
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
            var interestedObjects = FindObjectsOfType(typeof(MonoBehaviour)).OfType<IStaticObjectOfInterest>();

            //default to null area
            var nullColors = Enumerable.Repeat(AiInterestCategory.NullArea.Color, TextureDimension * TextureDimension).ToArray();
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
            bounds.center += _centerOfTexture;

            return bounds;
        }

        private static IDynamicObjectOfInterest GetObserverRepresentation(IObjectOfInterest dynamicObjectOfInterest) => new Observer(dynamicObjectOfInterest);

        private class Observer : IDynamicObjectOfInterest
        {
            public Observer(IObjectOfInterest objectOfInterest)
            {
                Bounds = objectOfInterest.Bounds;
            }

            public AiInterestCategory InterestCategory => AiInterestCategory.Observer;
            public Bounds Bounds { get; }
        }
    }
}