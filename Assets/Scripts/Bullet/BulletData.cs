using UnityEngine;

namespace Bullet
{
    [CreateAssetMenu(menuName = "Data/BulletData")]
    public class BulletData : ScriptableObject
    {
        [field: SerializeField] public GameObject AfterEffect { get; }

        [field: SerializeField] public int Damage { get; }

        [field: SerializeField] public float Speed { get; }

        [field: SerializeField] public float Range { get; }
    }
}