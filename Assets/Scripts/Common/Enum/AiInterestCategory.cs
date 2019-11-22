using UnityEngine;

namespace Common.Enum
{
    public class AiInterestCategory
    {
        //assuming the player is the target
        public static readonly AiInterestCategory Player = new AiInterestCategory(Color.red,10);
        public static readonly AiInterestCategory Enemy = new AiInterestCategory(Color.green,9);
        public static readonly AiInterestCategory Turret = new AiInterestCategory(Color.blue,8);
        //let's keep it blue for now
        public static readonly AiInterestCategory TurretRange = new AiInterestCategory(Color.blue,7);
        public static readonly AiInterestCategory Obstacle = new AiInterestCategory(new Color(0.65f, 0.16f, 0.16f),6);
        public static readonly AiInterestCategory Ground = new AiInterestCategory(Color.grey,5);
        public static readonly AiInterestCategory NullArea = new AiInterestCategory(Color.black,0);

        private AiInterestCategory(Color color, int priority)
        {
            Color = color;
            Priority = priority;
        }

        public Color Color { get; }
        //higher value is more important
        public int Priority { get; } 
    }
}