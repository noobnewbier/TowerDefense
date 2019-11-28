using Elements.Units.UnitCommon;
using MLAgents.Sensor;
using UnityEngine;

namespace AgentAi
{
    public class Texture2DSensorComponent : SensorComponent
    {
        [SerializeField] private bool grayScale;
        [SerializeField] private string sensorName = "Texture2DSensor";
        [SerializeField] private Unit unit;

        /// for "shape", please See reference for shape in
        /// <see cref="CompressedObservation" />
        /// class
        public override ISensor CreateSensor()
        {
            var enemyAgentObservationCollector = EnemyAgentObservationCollector.instance;
            
            return new Texture2DSensor(
                grayScale,
                sensorName,
                enemyAgentObservationCollector.Shape,
                unit,
                enemyAgentObservationCollector
            );
        }
    }
}