using UnityEngine;
using UnityUtils;

namespace Bullets
{
    public class Bullet : PooledMonoBehaviour
    {
        [SerializeField] private BulletData data;
        private Vector3 _originPosition;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _originPosition = transform.position;
        }

        private void FixedUpdate()
        {
            var selfTransform = transform;
            _rigidbody.MovePosition(selfTransform.position + Time.fixedDeltaTime * data.Speed * selfTransform.forward);

            if (Vector3.Distance(_originPosition, selfTransform.position) > data.Range)
            {
                SelfDestroy();                
            }
        }

        private void SelfDestroy()
        {
            ReturnToPool();
        }
    }
}