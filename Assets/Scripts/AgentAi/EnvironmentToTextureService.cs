using System;
using System.Collections.Generic;
using System.Linq;
using Common.Class.ObjectDrawer;
using Common.Enum;
using Common.Struct;
using UnityEngine;

namespace AgentAi
{
    [CreateAssetMenu(menuName = "ScriptableService/EnvironmentToTexture")]
    public class EnvironmentToTextureService : ScriptableObject
    {
        // No Proper reason not to be static, other than the fact that I don't like it
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void DrawObjectsOnTexture(Texture2D texture2D,
                                         IEnumerable<InterestedInformation> interestedObjects,
                                         IDictionary<InterestCategory, Color> categoryAndColor,
                                         IDictionary<InterestCategory, int> categoryAndPriority,
                                         IDictionary<InterestCategory, Drawer> categoryAndDrawer,
                                         int[,] coordinatesWithPriority,
                                         float precision,
                                         bool shouldWritePriority)
        {
            var centerOfTexture = new Vector3(texture2D.width / 2f, 0, texture2D.height / 2f);

            interestedObjects = interestedObjects.ToList().OrderBy(
                i => categoryAndPriority[i.Category]
            );

            foreach (var objectOfInterest in interestedObjects)
            {
                var rescaledBounds = RescaleBoundsToTexture(objectOfInterest.Bounds, precision, centerOfTexture);

                categoryAndDrawer[objectOfInterest.Category].DrawObjectWithPriority(
                    texture2D,
                    rescaledBounds,
                    categoryAndColor[objectOfInterest.Category],
                    coordinatesWithPriority,
                    categoryAndPriority[objectOfInterest.Category],
                    shouldWritePriority
                );
            }

            //not very sure if we need this or not
            texture2D.Apply();
        }

        //technically we will be fine without returning a new one.... we will optimize when we need
        private static Bounds RescaleBoundsToTexture(Bounds bounds, float precision, Vector3 centerOfTexture)
        {
            bounds.center *= precision;
            bounds.center += centerOfTexture;
            bounds.size *= precision;

            return bounds;
        }
    }
}