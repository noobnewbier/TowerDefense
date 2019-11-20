using Common.Event;
using EventManagement;
using UnityEngine;

namespace AgentAi
{
    public class EnemyAgentObservationCollector : MonoBehaviour, IHandle<GameStartEvent>
    {
        [SerializeField] private int textureWidth;
        [SerializeField] private int textureHeight;
        
        private Texture _terrainTexture;
        private Texture _observedTexture;
        

        public void Handle(GameStartEvent @event)
        {
            SetupTextures();
        }

        private void SetupTextures()
        {
            _terrainTexture = new Texture2D(textureWidth, textureHeight);
            
            
        }
    }
}