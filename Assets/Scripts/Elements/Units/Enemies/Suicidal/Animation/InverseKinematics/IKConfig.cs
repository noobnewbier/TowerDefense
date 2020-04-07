using UnityEngine;

namespace Elements.Units.Enemies.Suicidal.Animation.InverseKinematics
{
    [CreateAssetMenu(menuName = "Data/IKConfig")]
    public class IKConfig : ScriptableObject
    {
        [SerializeField] private float learningRate;
        [SerializeField] private float samplingDistance;
        [SerializeField] private float undergroundPenalty;
        [SerializeField] private float distanceThreshold;

        public float DistanceThreshold => distanceThreshold;
        public float LearningRate => learningRate;
        public float SamplingDistance => samplingDistance;
        public float UndergroundPenalty => undergroundPenalty;
    }
}