using UnityEngine;

namespace Common.Class.ObjectDrawer
{
    public abstract class Drawer : ScriptableObject
    {
        public abstract void DrawObjectWithPriority
        (
            Texture2D texture2D,
            Bounds bounds,
            Color color,
            int[,] coordinatesWithPriority,
            int priority,
            bool shouldWritePriority
        );
    }
}