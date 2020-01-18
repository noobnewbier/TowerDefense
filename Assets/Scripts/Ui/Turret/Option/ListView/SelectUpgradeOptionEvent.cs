using Ui.Turret.Option.Entry;

namespace Ui.Turret.Option.ListView
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