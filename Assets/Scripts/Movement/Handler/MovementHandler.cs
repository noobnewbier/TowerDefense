using Movement.InputSource;
using Units.UnitCommon;
using UnityEngine;

namespace Movement.Handler
{
    [DefaultExecutionOrder (10)]
    public abstract class MovementHandler : MonoBehaviour
    {
        [SerializeField] protected MovementInputSource inputSource;
        [SerializeField] protected Unit unit;
    }
}