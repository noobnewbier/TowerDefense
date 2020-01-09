using Effects;
using UnityEngine;

namespace Bullet
{
    [CreateAssetMenu(menuName = "Data/BulletData")]
    public class BulletData : ScriptableObject
    {
        [SerializeField] private GameObject afterEffect;
        [SerializeField] private Effect damageEffect;
        [SerializeField] private float range;
        [SerializeField] private float speed;

        public GameObject AfterEffect => afterEffect;
        public Effect DamageEffect => damageEffect;
        public float Speed => speed;
        public float Range => range;
    }
}