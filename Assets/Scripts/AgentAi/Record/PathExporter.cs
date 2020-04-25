using System.Collections.Generic;
using AgentAi.Manager;
using AgentAi.Suicidal;
using Common.Class;
using Common.Enum;
using UnityEngine;

namespace AgentAi.Record
{
    [CreateAssetMenu(menuName = "ScriptableService/PathExporter")]
    public class PathExporter : ScriptableObject
    {
        [SerializeField] private EnemyAgentObservationConfig agentObservationConfig;
        [SerializeField] private EnvironmentDrawConfig drawConfig;
        [SerializeField] private EnvironmentToTextureService environmentToTextureService;
        [SerializeField] private Color pathColor;
        [SerializeField] private Color startingPointColor;
        
        [SerializeField] private TextureSavingService textureSavingService;
        
        public void ExportPath(RoundData roundData)
        {
            var terrainTexture = new Texture2D(
                agentObservationConfig.MapDimension,
                agentObservationConfig.MapDimension,
                TextureFormat.RGB24,
                false
            );
            var finalTexture = new Texture2D(
                agentObservationConfig.MapDimension,
                agentObservationConfig.MapDimension,
                TextureFormat.RGB24,
                false
            );
            var coordinatesWithPriority =
                new int[agentObservationConfig.MapDimension, agentObservationConfig.MapDimension];

            environmentToTextureService.DrawObjectsOnTexture(
                terrainTexture,
                roundData.StaticDynamicEnvironmentInfo.ObjectsInfo,
                drawConfig.CategoryAndColors,
                drawConfig.CategoryAndPriority,
                drawConfig.CategoryAndDrawer,
                coordinatesWithPriority,
                agentObservationConfig.Precision,
                false
            );

            Graphics.CopyTexture(terrainTexture, finalTexture);
            var usePathColorDrawConfig = new Dictionary<InterestCategory, Color>(drawConfig.CategoryAndColors);
            var pathFirstPriority = new Dictionary<InterestCategory, int>(drawConfig.CategoryAndPriority)
            {
                [InterestCategory.Observer] = int.MaxValue
            };

            usePathColorDrawConfig[InterestCategory.Observer] = pathColor;
            for (var i = 1; i < roundData.DynamicEnvironmentsInfo.Count; i++)
            {
                var dynamicEnvironmentData = roundData.DynamicEnvironmentsInfo[i];
                environmentToTextureService.DrawObjectsOnTexture(
                    finalTexture,
                    dynamicEnvironmentData.ObjectsInfo,
                    usePathColorDrawConfig,
                    pathFirstPriority,
                    drawConfig.CategoryAndDrawer,
                    coordinatesWithPriority,
                    agentObservationConfig.Precision,
                    true
                );
            }
            
            usePathColorDrawConfig[InterestCategory.Observer] = startingPointColor;
            //draw the starting point last to prevent it being over written, dirty...
            environmentToTextureService.DrawObjectsOnTexture(
                finalTexture,
                roundData.DynamicEnvironmentsInfo[0].ObjectsInfo,
                usePathColorDrawConfig,
                pathFirstPriority,
                drawConfig.CategoryAndDrawer,
                coordinatesWithPriority,
                agentObservationConfig.Precision,
                true
            );

            textureSavingService.SaveTexture(finalTexture, "path");
        }
    }
}