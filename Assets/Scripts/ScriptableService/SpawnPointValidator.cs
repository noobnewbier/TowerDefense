using Common.Constant;
using UnityEngine;

namespace ScriptableService
{
    public interface ISpawnPointValidator
    {
        bool IsSpawnPointValid
        (
            Vector3 centerWorldSpace,
            Vector3 halfSize,
            Quaternion orientation,
            GameObject tobeSpawned = null
        );
    }

    [CreateAssetMenu(menuName = "ScriptableService/SpawnPointValidator")]
    public class SpawnPointValidator : ScriptableObject, ISpawnPointValidator
    {
        private static int _layerMask;

        //As long as it has 2 element, we can return false, no need to store more
        private readonly Collider[] _buffer = new Collider[2];

        private void OnEnable()
        {
            _layerMask = (1 << LayerMask.NameToLayer(LayerNames.Obstacle)) | (1 << LayerMask.NameToLayer(LayerNames.AiDamageTaker)) |
                         (1 << LayerMask.NameToLayer(LayerNames.PlayerDamageTaker)) | (1 << LayerMask.NameToLayer(LayerNames.Turret));
        }

        public bool IsSpawnPointValid
        (
            Vector3 centerWorldSpace,
            Vector3 halfSize,
            Quaternion orientation,
            GameObject tobeSpawned = null
        )
        {
            var resultSize = Physics.OverlapBoxNonAlloc(centerWorldSpace, halfSize, _buffer, orientation, _layerMask, QueryTriggerInteraction.Ignore);
            switch (resultSize)
            {
                case 0:
                    // no overlapped collider, good to go
                    return true;
                case 1:
                    // if the only element is the gameObject that is to be spawned, it is just colliding with it's new position, ignore it.
                    return _buffer[0].gameObject == tobeSpawned;
                default:
                    return false;
            }
        }
    }
}