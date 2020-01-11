namespace Elements.Turret.Upgrade.UI.TurretStatistic
{
    public struct TurretStatisticModelUpdatesEvent
    {
        public TurretStatisticModelUpdatesEvent(IStatisticModel updatedModel)
        {
            UpdatedModel = updatedModel;
        }

        public IStatisticModel UpdatedModel { get; }
    }
}