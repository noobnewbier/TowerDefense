using Movement.InputSource;
using Units.UnitCommon;
using UnityEngine;

namespace Movement.Handler
{
    public abstract class MovementHandler : MonoBehaviour
    {
        [SerializeField] protected MovementInputSource inputSource;
        [SerializeField] protected Unit unit;
    }
}