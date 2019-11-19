namespace Movement.InputSource
{
    public class AiMovementInputService : MovementInputSource
    {
        private float _vertical;
        private float _horizontal;
        
        public void UpdateVertical(float vertical)
        {
            _vertical = vertical;
        }

        public void UpdateHorizontal(float horizontal)
        {
            _horizontal = horizontal;
        }

        public override float Vertical() => _vertical;

        public override float Horizontal() => _horizontal;
    }
}