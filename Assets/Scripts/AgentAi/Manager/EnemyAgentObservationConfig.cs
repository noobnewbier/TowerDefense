using UnityEngine;

namespace AgentAi.Manager
{
    [CreateAssetMenu(menuName = "AIConfig/EnemyAgentObservationConfig")]
    public class EnemyAgentObservationConfig : ScriptableObject
    {
        private int? _maximumSideLength;
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

        public int GetTextureDimension()
        {
            if (_maximumSideLength == null) _maximumSideLength = CalculateTextureDimension();

            return _maximumSideLength.Value;
        }

        private int CalculateTextureDimension()
        {
            var toReturn = (float) mapDimension;
            if (useTextureRotation)
            {
                toReturn *= toReturn;
                toReturn = Mathf.Sqrt(toReturn * 2);
            }

            if (useTranslation) toReturn *= 2;
            return (int) (Mathf.CeilToInt(toReturn) * precision);
        }

        public int[] CalculateShape()
        {
            var textureDimension = GetTextureDimension();
            return new[] {textureDimension, textureDimension, grayScale ? 1 : 3};
        }
    }
}