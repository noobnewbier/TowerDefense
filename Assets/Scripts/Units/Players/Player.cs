using Common.Events;
using Units.UnitCommon;

namespace Units.Players
{
    public class Player : Unit
    {
        protected override void Dies()
        {
            EventAggregator.Publish(new PlayerDeadEvent());
        }
    }
}