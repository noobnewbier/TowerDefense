using AgentAi.Manager;
using MLAgents.Sensor;
using UnityEngine;

namespace AgentAi.Sensor
{
    public class Texture2DSensorComponent : SensorComponent
    {
        [SerializeField] private string sensorName = "Texture2DSensor";

        /// for "shape", please See reference for shape in
        /// <see cref="CompressedObservation" />
        /// class
        public override ISensor CreateSensor()
        {
            var enemyAgentObservationCollector = EnemyAgentObservationCollector.Instance;

            return new Texture2DSensor(
                enemyAgentObservationCollector.GrayScale,
                sensorName,
                enemyAgentObservationCollector.Shape,
                GetComponent<ICanObserveEnvironment>()
            );
        }

        public override int[] GetObservationShape() => EnemyAgentObservationCollector.Instance.Shape;
    }
}