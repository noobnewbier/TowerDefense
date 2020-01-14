namespace Elements.Turret.Upgrade.UI.Option.Entry
{
    public struct UpgradeOptionModelUpdateEvent
    {
        public UpgradeOptionModelUpdateEvent(IUpgradeOptionModel updatedModel)
        {
            UpdatedModel = updatedModel;
        }

        public IUpgradeOptionModel UpdatedModel { get; }
    }
}