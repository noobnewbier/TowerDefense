using UnityEngine;

namespace Movement
{
    public abstract class MovementInputSource : MonoBehaviour
    {
        public abstract float Vertical();
        public abstract float Horizontal();
    }
}