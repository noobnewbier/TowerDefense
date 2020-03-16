using Common.Interface;
using Elements.Turret;
using Elements.Units.Enemies;
using Elements.Units.Players;

namespace Common.Event
{
    public interface IDynamicObjectSpawnedEvent
    {
        IDynamicObjectOfInterest DynamicObject { get; }
    }

    public struct EnemySpawnedEvent : IDynamicObjectSpawnedEvent
    {
        public EnemySpawnedEvent(Enemy enemy)
        {
            Enemy = enemy;
        }

        public IDynamicObjectOfInterest DynamicObject => Enemy;
        public Enemy Enemy { get; }
    }

    public struct TurretSpawnedEvent : IDynamicObjectSpawnedEvent
    {
        public TurretSpawnedEvent(Turret turret)
        {
            Turret = turret;
        }

        public IDynamicObjectOfInterest DynamicObject => Turret;
        public Turret Turret { get; }
    }

    public struct PlayerSpawnedEvent : IDynamicObjectSpawnedEvent
    {
        public PlayerSpawnedEvent(Player player)
        {
            Player = player;
        }

        public IDynamicObjectOfInterest DynamicObject => Player;
        public Player Player { get; }
    }

    public struct DynamicObstacleSpawnedEvent : IDynamicObjectSpawnedEvent
    {
        public DynamicObstacleSpawnedEvent(IDynamicObjectOfInterest dynamicObject)
        {
            DynamicObject = dynamicObject;
        }

        public IDynamicObjectOfInterest DynamicObject { get; }
    }

    public struct SystemObjectSpawnedEvent : IDynamicObjectSpawnedEvent {
        public SystemObjectSpawnedEvent(IDynamicObjectOfInterest dynamicObject)
        {
            DynamicObject = dynamicObject;
        }

        public IDynamicObjectOfInterest DynamicObject { get; }
    }

}