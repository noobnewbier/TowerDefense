using AgentAi.Manager;
using MLAgents.Sensor;
using UnityEngine;

namespace AgentAi.Sensor
{
    public class Texture2DSensorComponent : SensorComponent
    {
        [SerializeField] private bool grayScale;
        [SerializeField] private string sensorName = "Texture2DSensor";

        /// for "shape", please See reference for shape in
        /// <see cref="CompressedObservation" />
        /// class
        public override ISensor CreateSensor()
        {
            var enemyAgentObservationCollector = EnemyAgentObservationCollector.Instance;

            return new Texture2DSensor(
                grayScale,
                sensorName,
                enemyAgentObservationCollector.Shape,
                GetComponent<ICanObserveEnvironment>()
            );
        }
    }
}