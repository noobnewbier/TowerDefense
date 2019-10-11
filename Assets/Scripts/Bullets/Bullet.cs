using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletData data;

        private void FixedUpdate()
        {
            var selfTransform = transform;
            selfTransform.position += Time.fixedDeltaTime * data.Speed * selfTransform.forward;
        }
    }
}