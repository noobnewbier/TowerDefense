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

        public void WriteToTensor(TensorProxy tensorProxy, int agentIndex)
        {
            using (TimerStack.Instance.Scoped(ScopedName))
            {
                var texture = _canObserveEnvironment.GetObservation();
                Utilities.TextureToTensorProxy(texture, tensorProxy, _grayScale, agentIndex);
                Object.Destroy(texture);
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

        public SensorCompressionType GetCompressionType() => SensorCompressionType.PNG;

        public string GetName() => _name;
    }
}