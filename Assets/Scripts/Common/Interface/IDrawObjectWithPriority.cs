using UnityEngine;

namespace Common.Interface
{
    public interface IDrawObjectWithPriority
    {
        void DrawObjectWithPriority(Texture2D texture2D, Bounds bounds, Color color, int[,] coordinatesWithPriority,
            int priority,
            bool shouldWritePriority);
    }
}