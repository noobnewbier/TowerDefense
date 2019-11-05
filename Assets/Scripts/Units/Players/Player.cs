using Common.Events;
using Units.UnitCommon;

namespace Units.Players
{
    public class Player : Unit
    {
        protected override void Dies()
        {
            base.Dies();

            EventAggregator.Publish(new PlayerDeadEvent());
        }
    }
}