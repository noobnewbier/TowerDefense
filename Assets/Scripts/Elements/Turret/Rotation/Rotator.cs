using UnityEngine;

namespace Elements.Turret.Rotation
{
    public abstract class Rotator : MonoBehaviour
    {
        public abstract void LookAt(Vector3 target, float speed);
    }
}