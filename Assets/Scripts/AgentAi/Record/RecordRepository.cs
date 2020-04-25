using System.Diagnostics.CodeAnalysis;
using System.IO;
using Common.Compression;
using UnityEngine;

namespace AgentAi.Record
{
    [CreateAssetMenu(menuName = "ScriptableService/RecordRepository")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public class RecordRepository : ScriptableObject
    {
        public RoundData GetRoundData(string recordName, int round)
        {
            var fileContents = File.ReadAllBytes(
                $"{EnvironmentRecorder.BasePath}{recordName}{Path.DirectorySeparatorChar}{round}.rcd"
            );
            return JsonUtility.FromJson<RoundData>(Compression.Unzip(fileContents));
        }
    }
}