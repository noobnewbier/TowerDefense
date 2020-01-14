namespace Ui.TurretUpgrade
{
    public struct UserFocusOnCanvasChangedEvent
    {
        public UserFocusOnCanvasChangedEvent(bool isFocusingOnCanvas)
        {
            IsFocusingOnCanvas = isFocusingOnCanvas;
        }

        public bool IsFocusingOnCanvas { get; }
    }
}