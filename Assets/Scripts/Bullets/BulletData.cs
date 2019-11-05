using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(menuName = "Data/BulletData")]
    public class BulletData : ScriptableObject
    {
        [SerializeField] private GameObject afterEffect;
        [SerializeField] private int damage;
        [SerializeField] private float range;
        [SerializeField] private float speed;

        public GameObject AfterEffect => afterEffect;
        public int Damage => damage;
        public float Speed => speed;
        public float Range => range;
    }
}