using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(menuName = "Data/BulletData")]
    public class BulletData : ScriptableObject
    {
        [SerializeField] private int damage;
        public int Damage => damage;

        [SerializeField] private float speed;
        public float Speed => speed;

        [SerializeField] private float range;
        public float Range => range;
    }
}