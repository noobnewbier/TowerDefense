using MLAgents;
using MLAgents.InferenceBrain;
using MLAgents.Sensor;
using UnityEngine;

namespace AgentAi.Sensor
{
    // DANGEROUS : You are extending a beta library here
    public class Texture2DSensor : ISensor
    {
        private const string ScopedName = "Texture2DSensor.GetCompressedObservation";
        private readonly ICanObserveEnvironment _canObserveEnvironment;

        private readonly bool _grayScale;
        private readonly string _name;
        private readonly int[] _shape;

        public Texture2DSensor
        (
            bool grayScale,
            string name,
            int[] shape,
            ICanObserveEnvironment canObserveEnvironment
        )
        {
            _grayScale = grayScale;
            _name = name;
            _shape = shape;
            _canObserveEnvironment = canObserveEnvironment;
        }

        public int[] GetFloatObservationShape() => _shape;

        public int Write(WriteAdapter adapter)
        {
            using (TimerStack.Instance.Scoped("RenderTexSensor.GetCompressedObservation"))
            {
                var texture = _canObserveEnvironment.GetObservation();
                var numWritten = Utilities.TextureToTensorProxy(texture, adapter, _grayScale);
                Object.Destroy(texture);
                
                return numWritten;
            }
        }

        public byte[] GetCompressedObservation()
        {
            using (TimerStack.Instance.Scoped(ScopedName))
            {
                var texture = _canObserveEnvironment.GetObservation();

                var compressed = texture.EncodeToPNG();
                Object.Destroy(texture);
                return compressed;
            }
        }

        public void Update()
        {
        }

        public SensorCompressionType GetCompressionType() => SensorCompressionType.PNG;

        public string GetName() => _name;
    }
}