using UnityEngine;

namespace Enemies
{
    public class DummyEnemy : MonoBehaviour, IEnemy
    {
        public UnitData UnitData { get; }
        public Transform Transform => transform;
    }
}