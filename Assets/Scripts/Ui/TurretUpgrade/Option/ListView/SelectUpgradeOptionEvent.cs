using Ui.TurretUpgrade.Option.Entry;

namespace Ui.TurretUpgrade.Option.ListView
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