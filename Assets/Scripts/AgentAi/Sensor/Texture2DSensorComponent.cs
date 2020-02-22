using MLAgents.Sensor;
using UnityEngine;

namespace AgentAi.Sensor
{
    public class Texture2DSensorComponent : SensorComponent
    {
        [SerializeField] private ObservationServiceProvider observationServiceProvider;
        [SerializeField] private string sensorName = "Texture2DSensor";

        /// for "shape", please See reference for shape in
        /// <see cref="CompressedObservation" />
        /// class
        public override ISensor CreateSensor()
        {
            var config = observationServiceProvider.ProvideService().Config;
            return new Texture2DSensor(
                config.GrayScale,
                sensorName,
                config.CalculateShape(),
                GetComponent<ICanObserveEnvironment>()
            );
        }

        public override int[] GetObservationShape()
        {
            return observationServiceProvider.ProvideService().Config.CalculateShape();
        }
    }
}