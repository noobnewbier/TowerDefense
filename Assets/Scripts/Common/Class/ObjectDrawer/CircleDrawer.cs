using System;
using Common.Interface;
using UnityEngine;

namespace Common.Class.ObjectDrawer
{
    public class CircleDrawer : IDrawObjectWithPriority
    {
        public static readonly CircleDrawer Instance = new CircleDrawer();

        private CircleDrawer()
        {
        }

        //if the texture is too large and is slow(because of SetPixel), use set pixels instead. For now leave it alone
        public void DrawObjectWithPriority(Texture2D texture2D, Bounds bounds, Color color, int[,] coordinatesWithPriority,
            int priority, bool shouldWritePriority)
        {
            var radius = bounds.extents.x;
            var x = bounds.center.x;
            var z = bounds.center.z;
            var rSquared = radius * radius;


            //be aware it can get out of bounds if we don't do those awkward length check            
            for (var u = Math.Max(0, x - radius); u < x + radius + 1 && u < coordinatesWithPriority.GetLength(0); u++)
            for (var v = Math.Max(0, z - radius); v < z + radius + 1 && v < coordinatesWithPriority.GetLength(1); v++)
            {
                if (!((x - u) * (x - u) + (z - v) * (z - v) < rSquared)) continue;

                var uAsInt = (int) u;
                var vAsInt = (int) v;
                if (coordinatesWithPriority[uAsInt, vAsInt] > priority) continue;
                if (shouldWritePriority) coordinatesWithPriority[uAsInt, vAsInt] = priority;

                texture2D.SetPixel(uAsInt, vAsInt, color);
            }
        }
    }
}