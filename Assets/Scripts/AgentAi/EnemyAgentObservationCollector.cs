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
    // Perhaps the main bottleneck... be careful with this
    public class EnemyAgentObservationCollector : MonoBehaviour, IHandle<GameStartEvent>, IHandle<IDynamicObjectDestroyedEvent>,
        IHandle<IDynamicObjectSpawnedEvent>
    {
        private Vector3 _centerOfTexture;
        private int[,] _coordinatesWithPriority;
        private IList<IDynamicObjectOfInterest> _dynamicObjects;
        private IEventAggregator _eventAggregator;
        private Texture2D _observedTexture;

        private Texture2D _terrainTexture;
        [SerializeField] private int textureHeight;
        [SerializeField] private int textureWidth;

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
            _centerOfTexture = new Vector3(textureWidth / 2f, 0, textureHeight / 2f);
            _terrainTexture = new Texture2D(textureWidth, textureHeight);
            _observedTexture = new Texture2D(textureWidth, textureHeight);
            _dynamicObjects = new List<IDynamicObjectOfInterest>();
            _coordinatesWithPriority = new int[textureWidth, textureHeight];
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
            var nullColors = Enumerable.Repeat(AiInterestCategory.NullArea.Color, textureWidth * textureHeight).ToArray();
            _terrainTexture.SetPixels(0, 0, textureWidth, textureHeight, nullColors);

            DrawObjectsOnTexture(_terrainTexture, interestedObjects, true);
        }

        public Texture2D ObserveEnvironment(Unit unit)
        {
            Graphics.CopyTexture(_terrainTexture, _observedTexture);

            DrawObjectsOnTexture(_observedTexture, _dynamicObjects.ReplaceAll(unit, GetObserverRepresentation(unit)), false);

            return _observedTexture;
        }

        private void DrawObjectsOnTexture(Texture2D texture2D, IEnumerable<IObjectOfInterest> interestedObjects, bool shouldWritePriority)
        {
            interestedObjects = interestedObjects.ToList().OrderBy(i => i.InterestCategory.Priority);

            foreach (var objectOfInterest in interestedObjects)
            {
                var rescaledBounds = RescaleBoundsToTexture(objectOfInterest.Bounds);

                objectOfInterest.InterestCategory.Drawer.DrawObjectWithPriority(texture2D, rescaledBounds, objectOfInterest.InterestCategory.Color,
                    _coordinatesWithPriority, objectOfInterest.InterestCategory.Priority, shouldWritePriority);
            }

            //not very sure if we need this or not
            texture2D.Apply();
        }

        //technically we will be fine without returning a new one.... we will optimize when we need
        private Bounds RescaleBoundsToTexture(Bounds bounds)
        {
            //we assume the scene creator have already made the texture height and width aligned with the scene's size in units (so 600 unit wide -> 600 pixels wide in textures)
            bounds.center += _centerOfTexture;

            return bounds;
        }

        private static IDynamicObjectOfInterest GetObserverRepresentation(IObjectOfInterest dynamicObjectOfInterest)
        {
            return new Observer(dynamicObjectOfInterest);
        }

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