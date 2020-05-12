using System;
using UnityEngine;

namespace Common.Class.ObjectDrawer
{
    [CreateAssetMenu(menuName = "Drawer/BlockDrawer")]
    public class BlockDrawer : Drawer
    {
        public override void DrawObjectWithPriority(
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
                try
                {
                    if (coordinatesWithPriority[x, y] > priority) continue;

                    if (shouldWritePriority) coordinatesWithPriority[x, y] = priority;

                    texture2D.SetPixel(x, y, color);
                }
                catch (IndexOutOfRangeException)
                {
#if UNITY_EDITOR
                    Debug.Log($"Out of bounds when drawing: {bounds}");
#endif
                }
        }
    }
}