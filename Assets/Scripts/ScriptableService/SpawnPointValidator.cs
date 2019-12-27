using Common.Constant;
using UnityEngine;

namespace ScriptableService
{
    public static class SpawnPointValidator
    {
        private static readonly int LayerMask = UnityEngine.LayerMask.GetMask(LayerNames.Obstacle, LayerNames.AiDamageTaker, LayerNames.PlayerDamageTaker);

        public static bool IsSpawnPointValid
            (Vector3 center, Vector3 halfExtents, Quaternion orientation) => Physics.CheckBox(center, halfExtents, orientation, LayerMask);
    }
}