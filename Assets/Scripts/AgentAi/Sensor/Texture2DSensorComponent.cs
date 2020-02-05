using AgentAi.Manager;
using MLAgents.Sensor;
using UnityEngine;

namespace AgentAi.Sensor
{
    public class Texture2DSensorComponent : SensorComponent
    {
        [SerializeField] private string sensorName = "Texture2DSensor";
        [SerializeField] private EnemyAgentObservationConfig config;

        /// for "shape", please See reference for shape in
        /// <see cref="CompressedObservation" />
        /// class
        public override ISensor CreateSensor()
        {
            return new Texture2DSensor(
                config.GrayScale,
                sensorName,
                config.CalculateShape(),
                GetComponent<ICanObserveEnvironment>()
            );
        }

        public override int[] GetObservationShape() => config.CalculateShape();
    }
}