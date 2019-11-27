using System;
using MLAgents.Sensor;
using UnityEngine;

namespace AgentAi
{
    public class Texture2DSensorComponent : SensorComponent
    {
        
        public override ISensor CreateSensor()
        {
            throw new NotImplementedException();
//            return new Texture2DSensor();
        }
    }
}