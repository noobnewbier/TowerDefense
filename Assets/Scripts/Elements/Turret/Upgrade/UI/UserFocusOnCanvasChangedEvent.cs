namespace Elements.Turret.Upgrade.UI
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