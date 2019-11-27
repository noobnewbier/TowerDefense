namespace Movement.InputSource
{
    public class AiMovementInputService : MovementInputSource
    {
        private float _horizontal;
        private float _vertical;

        public void UpdateVertical(float vertical)
        {
            _vertical = vertical;
        }

        public void UpdateHorizontal(float horizontal)
        {
            _horizontal = horizontal;
        }

        public override float Vertical()
        {
            return _vertical;
        }

        public override float Horizontal()
        {
            return _horizontal;
        }
    }
}