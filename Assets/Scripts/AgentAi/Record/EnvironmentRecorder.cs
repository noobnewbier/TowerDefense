using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Compression;
using Common.Interface;
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

        [SerializeField] private string recordName;

        public static string BasePath =>
            $"{Directory.GetParent(Application.dataPath)}{Path.DirectorySeparatorChar}{EnvironmentDirectoryNamePrefix}{Path.DirectorySeparatorChar}";

        public void EndRound()
        {
            WriteData();
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
                                   IDynamicObjectOfInterest observer)
        {
            var observerObjectTransform = observer.ObjectTransform;
            var position = observerObjectTransform.position;
            var data = new DynamicEnvironmentData(
                dynamicObjectOfInterests.Select(i => i.InterestedInformation).ToList(),
                observerObjectTransform.eulerAngles.y,
                new Vector2(position.x, position.z)
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

        public void CreateNewRecord()
        {
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
                _path = $"{BasePath}{recordName}-{_pathSuffix}";
            } while (Directory.Exists(_path));
        }
    }
}