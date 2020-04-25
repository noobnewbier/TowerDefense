using System;
using System.IO;
using System.Linq;
using AgentAi.Manager;
using AgentAi.Suicidal;
using Common.Compression;
using Common.Enum;
using UnityEngine;
using UnityUtils;

namespace AgentAi.Record
{
    /// <summary>
    ///     Look dangerously dirty... One day you will regret and KYS
    ///     Very similar to <see cref="EnemyAgentObservationService" />
    /// </summary>
    public class EnvironmentReplayer : MonoBehaviour
    {
        //not sure if I actually need it tbh, but let's leave it here(no plan to extend this thing, only for debug use anyway)
        private int[,] _coordinatesWithPriority;
        private RoundData _currentRoundData;
        private EnvironmentDrawConfig _drawConfig;
        private Texture2D _finalTexture;
        private bool _showTexture;

        private Texture2D _terrainTexture;
        [SerializeField] private EnemyAgentObservationConfig agentObservationConfig;
        [SerializeField] private int currentRound;
        [SerializeField] private EnvironmentToTextureService environmentToTextureService;
        [SerializeField] private bool markCentreWithMagenta;

        [SerializeField] private string recordName;
        [Range(1, 10)] [SerializeField] private float scale;
        [SerializeField] private int step;
        [SerializeField] private PathExporter pathExporter;
        [SerializeField] private RecordRepository recordRepository;

        [ContextMenu("DisplayCurrentRound")]
        private void DisplayCurrentRound()
        {
            ResetFields();
            _showTexture = true;
            _currentRoundData = recordRepository.GetRoundData(recordName, currentRound);

            environmentToTextureService.DrawObjectsOnTexture(
                _terrainTexture,
                _currentRoundData.StaticDynamicEnvironmentInfo.ObjectsInfo,
                _drawConfig.CategoryAndColors,
                _drawConfig.CategoryAndPriority,
                _drawConfig.CategoryAndDrawer,
                _coordinatesWithPriority,
                agentObservationConfig.Precision,
                false
            );
        }

        [ContextMenu("ExportPath")]
        private void ExportPath()
        {
            var roundData = recordRepository.GetRoundData(recordName, currentRound);
            pathExporter.ExportPath(roundData);
        }
        
        private void ResetFields()
        {
            _drawConfig = agentObservationConfig.DrawingConfig;

            var textureDimension = agentObservationConfig.GetTextureDimension();
            _finalTexture = new Texture2D(
                textureDimension,
                textureDimension,
                TextureFormat.RGB24,
                false
            );

            _terrainTexture = new Texture2D(
                textureDimension,
                textureDimension,
                TextureFormat.RGB24,
                false
            );

            //default to null area
            var nullColors = Enumerable.Repeat(
                    _drawConfig.CategoryAndColors[InterestCategory.NullArea],
                    textureDimension * textureDimension
                )
                .ToArray();
            _terrainTexture.SetPixels(nullColors);

            _coordinatesWithPriority = new int[textureDimension, textureDimension];
        }

        private void OnGUI()
        {
            if (_showTexture)
            {
                Graphics.CopyTexture(_terrainTexture, _finalTexture);
                step = Math.Min(_currentRoundData.DynamicEnvironmentsInfo.Count - 1, Math.Max(step, 0));
                var envInfo = _currentRoundData.DynamicEnvironmentsInfo[step];

                environmentToTextureService.DrawObjectsOnTexture(
                    _finalTexture,
                    envInfo.ObjectsInfo,
                    _drawConfig.CategoryAndColors,
                    _drawConfig.CategoryAndPriority,
                    _drawConfig.CategoryAndDrawer,
                    _coordinatesWithPriority,
                    agentObservationConfig.Precision,
                    true
                );

                if (agentObservationConfig.UseTranslation)
                    _finalTexture.TranslateTexture(
                        (int) (envInfo.ObserverPosition.x * agentObservationConfig.Precision),
                        (int) (envInfo.ObserverPosition.y * agentObservationConfig.Precision)
                    );

                if (agentObservationConfig.UseTextureRotation)
                    _finalTexture.RotateTexture(
                        -envInfo.ObserverYEuler,
                        agentObservationConfig.DrawingConfig.CategoryAndColors[InterestCategory.NullArea]
                    );
                
                if (markCentreWithMagenta)
                {
                    _finalTexture.SetPixel(_finalTexture.width / 2, _finalTexture.height / 2, Color.magenta);
                    _finalTexture.Apply();
                }
                GUI.DrawTexture(
                    new Rect(
                        Screen.width - _finalTexture.width * scale,
                        0f,
                        _finalTexture.width * scale,
                        _finalTexture.height * scale
                    ),
                    _finalTexture
                );
                Debug.Log(envInfo.CumulativeReward);
            }
        }
    }
}