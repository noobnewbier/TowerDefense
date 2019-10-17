using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(menuName = "Data/BulletData")]
    public class BulletData : ScriptableObject
    {
        [SerializeField] private int damage;

        [SerializeField] private float range;

        [SerializeField] private float speed;
        public int Damage => damage;
        public float Speed => speed;
        public float Range => range;
    }
}