using UnityEngine;

namespace Movement
{
    public abstract class MovementHandler : MonoBehaviour
    {
        [SerializeField] private MovementInputSource inputSource;

        protected void FixedUpdate()
        {
            MoveVertical(inputSource.Vertical());
            MoveHorizontal(inputSource.Horizontal());
        }

        protected abstract void MoveVertical(float value);
        protected abstract void MoveHorizontal(float value);
    }
}