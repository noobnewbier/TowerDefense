using UnityEngine;

namespace Movement.InputSource
{
    public abstract class MovementInputSource : MonoBehaviour
    {
        public abstract float Vertical();
        public abstract float Horizontal();
    }
}