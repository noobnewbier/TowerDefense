namespace Elements.Turret.Upgrade.UI.TurretDescription
{
    public struct TurretDescriptionTextUpdateEvent
    {
        public TurretDescriptionTextUpdateEvent(ITurretDescriptionModel updatedDescription)
        {
            UpdatedDescription = updatedDescription;
        }

        public ITurretDescriptionModel UpdatedDescription { get; }
    }
}