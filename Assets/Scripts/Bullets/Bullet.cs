using Common;
using UnityEngine;
using UnityUtils;

namespace Bullets
{
    public class Bullet : PooledMonoBehaviour
    {
        private Vector3 _originPosition;
        private Rigidbody _rigidbody;

        [SerializeField] private BulletData data;
        public int Damage => data.Damage;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("DamageTaker") || !IsBulletTarget(other.tag))
            {
                SelfDestroy();
            }
        }

        private bool IsBulletTarget(string contactTag)
        {
            switch (contactTag)
            {
                case ObjectTags.Ai when CompareTag(ObjectTags.DamageAi):
                case ObjectTags.Player when CompareTag(ObjectTags.DamagePlayer):
                    return true;
                default:
                    return false;
            }
        }
    }
}