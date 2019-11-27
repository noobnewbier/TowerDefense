using System;
using Elements.Units.UnitCommon;
using MLAgents;
using MLAgents.InferenceBrain;
using MLAgents.Sensor;
using UnityEngine;

namespace AgentAi
{
    // DANGEROUS : You are extending a beta library here
    public class Texture2DSensor : ISensor
    {
        private const string ScopedName = "Texture2DSensor.GetCompressedObservation";
        private readonly IHasObservationTexture _hasObservationTexture;
        private readonly bool _grayscale;
        private readonly string _name;
        private readonly int[] _shape;

        public Texture2DSensor(string name)
        {
            _name = name;
        }

        public Texture2DSensor(IHasObservationTexture hasObservationTexture, bool grayscale, string name, int[] shape)
        {
            _hasObservationTexture = hasObservationTexture;
            _grayscale = grayscale;
            _name = name;
            _shape = shape;
        }

        public int[] GetFloatObservationShape() => _shape;

        public void WriteToTensor(TensorProxy tensorProxy, int agentIndex)
        {
            using (TimerStack.Instance.Scoped(ScopedName))
            {
                var texture = _hasObservationTexture.GetCloneOfObservationTexture();
                Utilities.TextureToTensorProxy(texture, tensorProxy, _grayscale, agentIndex);
                UnityEngine.Object.Destroy(texture);
            }
        }

        public byte[] GetCompressedObservation()
        {
            using(TimerStack.Instance.Scoped(ScopedName))
            {
                var texture = _hasObservationTexture.GetCloneOfObservationTexture();

                var compressed = texture.EncodeToPNG();
                UnityEngine.Object.Destroy(texture);
                return compressed;
            }
        }

        public SensorCompressionType GetCompressionType() => SensorCompressionType.PNG;

        public string GetName() => _name;
    }

    public interface IHasObservationTexture
    {
        Texture2D GetCloneOfObservationTexture();
    }
}