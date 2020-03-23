using Newtonsoft.Json.Converters;

namespace Common.Enum
{
    //don't reorder them, you are serializing these crap, you will absolutely hate it when you have to redo the inspector crap
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum InterestCategory
    {
        Observer,
        Target,
        Player,
        Obstacle,
        Ground,
        NullArea,
        Enemy,
        TurretRange,
        Turret,
        System
    }
}