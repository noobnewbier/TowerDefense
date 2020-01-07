using Common.Class.ObjectDrawer;
using Common.Interface;
using UnityEngine;

namespace Common.Enum
{
    public class AiInterestCategory
    {
        public static readonly AiInterestCategory Observer = new AiInterestCategory(Color.white, 13, BlockDrawer.Instance);
        public static readonly AiInterestCategory Target = new AiInterestCategory(Color.magenta, 12, BlockDrawer.Instance);
        
        //assuming the player is the target
        public static readonly AiInterestCategory Player = new AiInterestCategory(Color.red, 11, BlockDrawer.Instance);
        public static readonly AiInterestCategory Enemy = new AiInterestCategory(Color.green, 10, BlockDrawer.Instance);
        public static readonly AiInterestCategory Obstacle = new AiInterestCategory(new Color(0.65f, .16f, 0.16f), 9, BlockDrawer.Instance);

        public static readonly AiInterestCategory Turret = new AiInterestCategory(Color.yellow, 8, BlockDrawer.Instance);

        //we need a lot more thinking on this
        public static readonly AiInterestCategory TurretRange = new AiInterestCategory(Color.blue, 7, CircleDrawer.Instance);
        public static readonly AiInterestCategory Ground = new AiInterestCategory(Color.grey, 5, BlockDrawer.Instance);
        public static readonly AiInterestCategory NullArea = new AiInterestCategory(Color.black, 0, BlockDrawer.Instance);

        private AiInterestCategory(Color color, int priority, IDrawObjectWithPriority drawer)
        {
            Color = color;
            Priority = priority;
            Drawer = drawer;
        }

        public Color Color { get; }

        //higher value is more important
        public int Priority { get; }
        public IDrawObjectWithPriority Drawer { get; }
    }
}