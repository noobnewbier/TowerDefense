using Common.Enum;
using Common.Interface;
using Elements.Turret;
using Elements.Units.Enemies;
using Elements.Units.Players;

namespace Common.Event
{
    public struct PlayerDeadEvent : IDynamicObjectDestroyedEvent
    {
        public PlayerDeadEvent(Player player)
        {
            Player = player;
        }

        public IDynamicObjectOfInterest DynamicObject => Player;
        public Player Player { get; }
    }

    public struct TurretDestroyedEvent : IDynamicObjectDestroyedEvent
    {
        public TurretDestroyedEvent(Turret turret)
        {
            Turret = turret;
        }

        public IDynamicObjectOfInterest DynamicObject => Turret;
        public Turret Turret { get; }
    }

    public struct EnemyDeadEvent : IDynamicObjectDestroyedEvent
    {
        public EnemyDeadEvent(Enemy enemy, DamageSource deathCause)
        {
            Enemy = enemy;
            DeathCause = deathCause;
        }

        public Enemy Enemy { get; }
        public DamageSource DeathCause { get; }
        public IDynamicObjectOfInterest DynamicObject => Enemy;
    }

    public struct DynamicObstacleDestroyedEvent : IDynamicObjectDestroyedEvent
    {
        public DynamicObstacleDestroyedEvent(IDynamicObjectOfInterest dynamicObject)
        {
            DynamicObject = dynamicObject;
        }

        public IDynamicObjectOfInterest DynamicObject { get; }
    }

    public interface IDynamicObjectDestroyedEvent
    {
        IDynamicObjectOfInterest DynamicObject { get; }
    }
}