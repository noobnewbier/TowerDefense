using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Compression;
using Common.Interface;
using MLAgents;
using UnityEngine;

namespace AgentAi.Record
{
    [CreateAssetMenu(menuName = "ScriptableService/EnvironmentRecorder")]
    public class EnvironmentRecorder : ScriptableObject
    {
        private const string EnvironmentDirectoryNamePrefix = "EnvironmentRecords";
        private int _currentRound;
        private List<DynamicEnvironmentData> _dynamicEnvironmentData;

        private string _path;
        private int _pathSuffix;
        private StaticEnvironmentData _staticDynamicEnvironmentInfo;
        [Range(1, 100)] [SerializeField] private int recordFrequency;

        private string recordName;
        public static string BasePath =>
#if UNITY_EDITOR
            $"{Directory.GetParent(Application.dataPath)}{Path.DirectorySeparatorChar}{EnvironmentDirectoryNamePrefix}{Path.DirectorySeparatorChar}";
#else
            $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}{EnvironmentDirectoryNamePrefix}{Path.DirectorySeparatorChar}";
#endif

        public void EndRound()
        {
            //only record according to frequency -- I know it's not the best to handle the logic here, but this will have to do
            if (_currentRound % recordFrequency == 0) WriteData();
        }

        private void WriteData()
        {
            var path =
                $"{_path}{Path.DirectorySeparatorChar}{_currentRound}.rcd";
            var compressedData = Compression.Zip(
                JsonUtility.ToJson(new RoundData(_dynamicEnvironmentData, _staticDynamicEnvironmentInfo))
            );

            File.WriteAllBytes(path, compressedData);
        }

        public void AddCurrentStep(IEnumerable<IDynamicObjectOfInterest> dynamicObjectOfInterests,
                                   IDynamicObjectOfInterest observer,
                                   Agent agent)
        {
            var observerObjectTransform = observer.ObjectTransform;
            var position = observerObjectTransform.position;
            var data = new DynamicEnvironmentData(
                dynamicObjectOfInterests.Select(i => i.InterestedInformation).ToList(),
                observerObjectTransform.eulerAngles.y,
                new Vector2(position.x, position.z),
                agent.GetCumulativeReward()
            );

            _dynamicEnvironmentData.Add(data);
        }

        public void CreateNewRound(IEnumerable<IStaticObjectOfInterest> staticObjectOfInterests)
        {
            _currentRound++;
            _dynamicEnvironmentData.Clear();
            _staticDynamicEnvironmentInfo = new StaticEnvironmentData(
                staticObjectOfInterests.Select(i => i.InterestedInformation).ToList()
            );
        }

        public void CreateNewRecord(string newRecordName)
        {
            recordName = newRecordName;
            InitRecordPath();
            Directory.CreateDirectory(_path);
        }

        private void OnEnable()
        {
            _dynamicEnvironmentData = new List<DynamicEnvironmentData>();
            _currentRound = -1;
            _pathSuffix = -1;
        }

        private void InitRecordPath()
        {
            do
            {
                _pathSuffix++;
                _path = $"{BasePath}{recordName}-{Nanoid.Nanoid.Generate()}-{_pathSuffix}";
            } while (Directory.Exists(_path));
        }
    }
}