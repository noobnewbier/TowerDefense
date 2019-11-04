using Movement.InputSource;
using UnityEngine;

namespace Movement.Handler
{
    public abstract class MovementHandler : MonoBehaviour
    {
        [SerializeField] protected MovementInputSource inputSource;
        
    }
}