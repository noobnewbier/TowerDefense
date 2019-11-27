using Elements.Units.UnitCommon;
using MLAgents.Sensor;
using UnityEngine;

namespace AgentAi
{
    public class Texture2DSensorComponent : SensorComponent
    {
        [SerializeField] private EnemyAgentObservationCollector enemyAgentObservationCollector;
        [SerializeField] private bool grayScale;
        [SerializeField] private string sensorName = "RenderTextureSensor";
        [SerializeField] private Unit unit;

        /// for "shape", please See reference for shape in
        /// <see cref="CompressedObservation" />
        /// class
        public override ISensor CreateSensor() =>
            new Texture2DSensor(
                grayScale,
                sensorName,
                new int[3] {enemyAgentObservationCollector.TextureWidth, enemyAgentObservationCollector.TextureHeight, grayScale ? 1 : 3},
                unit,
                enemyAgentObservationCollector
            );
    }
}