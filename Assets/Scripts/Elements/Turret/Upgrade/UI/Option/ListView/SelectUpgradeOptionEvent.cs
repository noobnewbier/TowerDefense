using Elements.Turret.Upgrade.UI.Option.Entry;

namespace Elements.Turret.Upgrade.UI.Option.ListView
{
    public struct SelectUpgradeOptionEvent
    {
        public SelectUpgradeOptionEvent(IUpgradeOptionModel newSelectedOption)
        {
            NewSelectedOption = newSelectedOption;
        }

        public IUpgradeOptionModel NewSelectedOption { get; }
    }
}