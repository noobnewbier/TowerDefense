using UnityEngine;

namespace AgentAi.Manager
{
    [CreateAssetMenu(menuName = "AIConfig/EnemyAgentObservationConfig")]
    public class EnemyAgentObservationConfig : ScriptableObject
    {
        [SerializeField] private bool grayScale;
        [SerializeField] [Range(1, 200)] private int mapDimension;
        [Range(1, 10)] [SerializeField] private float precision = 1;
        [SerializeField] private bool useTextureRotation;
        [SerializeField] private bool useTranslation;

        public int MapDimension => mapDimension;
        public bool UseTranslation => useTranslation;
        public bool UseTextureRotation => useTextureRotation;
        public bool GrayScale => grayScale;
        public float Precision => precision;

        public int CalculateTextureDimension()
        {
            var maximumSideLength = (float) mapDimension;
            if (useTextureRotation)
            {
                maximumSideLength *= maximumSideLength;
                maximumSideLength = Mathf.Sqrt(maximumSideLength * 2);
            }

            if (useTranslation) maximumSideLength *= 2;

            return (int) (Mathf.CeilToInt(maximumSideLength) * precision);
        }

        public int[] CalculateShape()
        {
            var textureDimension = CalculateTextureDimension();
            return new[] {textureDimension, textureDimension, grayScale ? 1 : 3};
        }
    }
}