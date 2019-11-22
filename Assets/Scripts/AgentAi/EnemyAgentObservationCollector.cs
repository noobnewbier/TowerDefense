using System;
using System.Linq;
using Common.Class;
using Common.Event;
using Common.Interface;
using EventManagement;
using Units.UnitCommon;
using UnityEngine;

namespace AgentAi
{
    // Perhaps the main bottleneck... be careful with this
    public class EnemyAgentObservationCollector : MonoBehaviour, IHandle<GameStartEvent>
    {
        [SerializeField] private int textureWidth;
        [SerializeField] private int textureHeight;
        
        private Texture2D _terrainTexture;
        private Texture2D _observedTexture;
        private IEventAggregator _eventAggregator;
        private Vector3 _centerOfTexture;

        private void Awake()
        {
            _centerOfTexture = new Vector3(textureWidth / 2f,0, textureHeight / 2f);
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

        public void Handle(GameStartEvent @event)
        {
            SetupTextures();
        }

        private void SetupTextures()
        {
            var interestedObjects = FindObjectsOfType(typeof(MonoBehaviour)).OfType<IObjectOfInterest>().ToList().OrderBy(i => i.InterestCategory.Priority);
            
            _terrainTexture = new Texture2D(textureWidth, textureHeight);
            
            //this can be optimized....?, but for now we will leave it alone.  
            foreach (var objectOfInterest in interestedObjects)
            {
                var rescaledBounds = RescaleBoundsToTexture(objectOfInterest.Bounds);

                for (var x = (int)rescaledBounds.min.x; x < rescaledBounds.max.x; x++)
                {
                    for (var y = (int)rescaledBounds.min.y; y < rescaledBounds.max.y; y++)
                    {
                        _terrainTexture.SetPixel(x, y, objectOfInterest.InterestCategory.Color);
                    }
                }
            }
        }

        public Texture2D ObserveEnvironment(Unit unit)
        {
            _observedTexture = _terrainTexture;
            //todo : need to update this crap
            return _observedTexture;
        }

        //technically we will be fine without returning a new one.... we will optimize when we need
        private Bounds RescaleBoundsToTexture(Bounds bounds)
        {
            //we assume the scene creator have already made the texture height and width aligned with the scene's size in units (so 600 unit wide -> 600 pixels wide in textures)
            bounds.center += _centerOfTexture;

            return bounds;
        }
    }
}