using UnityEngine;

namespace AgentAi.Manager
{
    [CreateAssetMenu(menuName = "AIConfig/EnemyAgentObservationConfig")]
    public class EnemyAgentObservationConfig : ScriptableObject
    {
        [SerializeField] private bool grayScale;
        [SerializeField] [Range(1, 200)] private int mapDimension;
        [Range(1, 10)] [SerializeField] private float precision = 1;

        public int MapDimension => mapDimension;
        public bool GrayScale => grayScale;
        public float Precision => precision;

        public int CalculateTextureDimension() => (int) (Mathf.CeilToInt(Mathf.Sqrt(Mathf.Pow(mapDimension, 2) * 2)) * 2 * precision);

        public int[] CalculateShape()
        {
            var textureDimension = CalculateTextureDimension();
            return new[] {textureDimension, textureDimension, grayScale ? 1 : 3};
        }
    }
}