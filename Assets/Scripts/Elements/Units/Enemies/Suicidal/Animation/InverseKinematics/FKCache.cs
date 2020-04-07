using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Elements.Units.Enemies.Suicidal.Animation.InverseKinematics
{
    [CreateAssetMenu(menuName = "ScriptableService/FKCache")]
    public class FKCache : ScriptableObject
    {
        public Dictionary<Vector3, List<float[]>> Cache { get; private set; }

        private static string BasePath =>
            $"{Directory.GetParent(Application.dataPath)}{Path.DirectorySeparatorChar}FKCache{Path.DirectorySeparatorChar}";

        private void OnEnable()
        {
            Cache = new Dictionary<Vector3, List<float[]>>();
        }

        public void CreateCache(Transform rootTransform, IKConfig config, Joint[] joints)
        {
            CreateCacheForJoints(config, joints, new float[joints.Length], 1, rootTransform);
        }

        private void CreateCacheForJoints(IKConfig config,
                                          IReadOnlyList<Joint> joints,
                                          float[] angles,
                                          int index,
                                          Transform rootTransform)
        {
            var joint = joints[index];
            for (var i = joint.MinAngle; i < joint.MaxAngle; i += config.SamplingDistance)
            {
                angles[index] = i;
                if (index < joints.Count - 2)
                {
                    CreateCacheForJoints(config, joints, angles, index + 1, rootTransform);
                }
                else
                {
                    var location = IKUtils.ForwardKinematics(angles, joints);
                    var cachedList = Cache.ContainsKey(location) ? Cache[location] : new List<float[]>();
                    cachedList.Add(angles);
                    
                    Cache[location] = cachedList;
                }
            }
        }

        [ContextMenu("WriteToJson")]
        private void WriteToJson()
        {
            Directory.CreateDirectory(BasePath);

            var json = JsonConvert.SerializeObject(Cache);
            File.WriteAllText(BasePath + name + ".json", json);
        }
    }
}