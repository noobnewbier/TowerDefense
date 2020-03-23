using System;
using System.Collections.Generic;
using Common.Struct;
using UnityEngine;

namespace AgentAi.Record
{
    [Serializable]
    public class RoundData
    {
        [SerializeField] private List<DynamicEnvironmentData> dynamicEnvironmentsInfo;
        [SerializeField] private StaticEnvironmentData staticDynamicEnvironmentInfo;

        public RoundData(List<DynamicEnvironmentData> dynamicEnvironmentsInfo,
                         StaticEnvironmentData staticDynamicEnvironmentInfo)
        {
            this.dynamicEnvironmentsInfo = dynamicEnvironmentsInfo;
            this.staticDynamicEnvironmentInfo = staticDynamicEnvironmentInfo;
        }

        public List<DynamicEnvironmentData> DynamicEnvironmentsInfo => dynamicEnvironmentsInfo;

        public StaticEnvironmentData StaticDynamicEnvironmentInfo => staticDynamicEnvironmentInfo;
    }

    [Serializable]
    public class DynamicEnvironmentData
    {
        [SerializeField] private List<InterestedInformation> objectsInfo;
        [SerializeField] private Vector2 observerPosition;
        [SerializeField] private float observerYEuler;

        public DynamicEnvironmentData(List<InterestedInformation> objectsInfo,
                                      float observerYEuler,
                                      Vector2 observerPosition)
        {
            this.objectsInfo = objectsInfo;
            this.observerYEuler = observerYEuler;
            this.observerPosition = observerPosition;
        }

        public Vector2 ObserverPosition => observerPosition;

        public float ObserverYEuler => observerYEuler;

        public List<InterestedInformation> ObjectsInfo => objectsInfo;
    }

    [Serializable]
    public class StaticEnvironmentData
    {
        [SerializeField] private List<InterestedInformation> objectsInfo;

        public StaticEnvironmentData(List<InterestedInformation> objectsInfo)
        {
            this.objectsInfo = objectsInfo;
        }

        public List<InterestedInformation> ObjectsInfo => objectsInfo;
    }
}