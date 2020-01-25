using Common.Interface;
using UnityEngine;

namespace Common.Class.ObjectDrawer
{
    public class BlockDrawer : IDrawObjectWithPriority
    {
        public static readonly BlockDrawer Instance = new BlockDrawer();

        private BlockDrawer()
        {
        }

        public void DrawObjectWithPriority
        (
            Texture2D texture2D,
            Bounds bounds,
            Color color,
            int[,] coordinatesWithPriority,
            int priority,
            bool shouldWritePriority
        )
        {
            //if the bounds is too small, the pixel can disappear after rotation due to loss of information
            var size = bounds.size;
            size.x = Mathf.Max(size.x, 1f);
            size.z = Mathf.Max(size.z, 1f);
            bounds.size = size;

            for (var y = (int) bounds.min.z; y < bounds.max.z; y++)
            for (var x = (int) bounds.min.x; x < bounds.max.x; x++)
            {
                if (coordinatesWithPriority[x, y] > priority) continue;

                if (shouldWritePriority) coordinatesWithPriority[x, y] = priority;

                texture2D.SetPixel(x, y, color);
            }
        }
    }
}