using Common.Class.ObjectDrawer;
using Common.Interface;
using UnityEngine;

namespace Common.Enum
{
    public class AiInterestCategory
    {
        // #region Distinct Colors
        //
        // public static readonly AiInterestCategory Target = new AiInterestCategory(Color.red, 12, BlockDrawer.Instance);
        // public static readonly AiInterestCategory Player = new AiInterestCategory(Color.red, 11, BlockDrawer.Instance);
        // public static readonly AiInterestCategory Obstacle = new AiInterestCategory(Color.green, 9, BlockDrawer.Instance);
        //
        // #region intentionally not rendered
        //
        // public static readonly AiInterestCategory Observer = new AiInterestCategory(Color.black, 0, BlockDrawer.Instance);
        // public static readonly AiInterestCategory Ground = new AiInterestCategory(Color.black, 0, BlockDrawer.Instance);
        // public static readonly AiInterestCategory NullArea = new AiInterestCategory(Color.black, 0, BlockDrawer.Instance);
        //
        // #endregion
        //
        // #region not rendered but needs to be in the future
        //
        // public static readonly AiInterestCategory Enemy = new AiInterestCategory(Color.black, 0, BlockDrawer.Instance);
        // public static readonly AiInterestCategory TurretRange = new AiInterestCategory(Color.black, 0, CircleDrawer.Instance);
        // public static readonly AiInterestCategory Turret = new AiInterestCategory(Color.black, 0, BlockDrawer.Instance);
        //
        // #endregion
        //
        // #endregion

        #region Coloring with similarities

        // //was 13, consider swap it back
        // public static readonly AiInterestCategory Observer = new AiInterestCategory(Color.white, 13, BlockDrawer.Instance);
        // public static readonly AiInterestCategory Target = new AiInterestCategory(Color.magenta, 12, BlockDrawer.Instance);
        //
        // //assuming the player is the target
        // public static readonly AiInterestCategory Player = new AiInterestCategory(Color.red, 11, BlockDrawer.Instance);
        // public static readonly AiInterestCategory Enemy = new AiInterestCategory(Color.green, 10, BlockDrawer.Instance);
        // public static readonly AiInterestCategory Obstacle = new AiInterestCategory(new Color(0.65f, .16f, 0.16f), 9, BlockDrawer.Instance);
        //
        // public static readonly AiInterestCategory Turret = new AiInterestCategory(Color.yellow, 8, BlockDrawer.Instance);
        //
        // //we need a lot more thinking on this
        // public static readonly AiInterestCategory TurretRange = new AiInterestCategory(Color.blue, 7, CircleDrawer.Instance);
        // public static readonly AiInterestCategory Ground = new AiInterestCategory(Color.grey, 5, BlockDrawer.Instance);
        // public static readonly AiInterestCategory NullArea = new AiInterestCategory(Color.black, 0, BlockDrawer.Instance);

        #endregion

        #region grey scale

        /*
         * Anything below 0.5 is rewarding, above 0.5 is punishing
         */

        //was 13, consider swap it back
        public static readonly AiInterestCategory Observer = new AiInterestCategory(Color.grey, 0, BlockDrawer.Instance);
        public static readonly AiInterestCategory Target = new AiInterestCategory(Color.black, 12, BlockDrawer.Instance);

        //assuming the player is the target
        public static readonly AiInterestCategory Player = new AiInterestCategory(Color.black, 11, BlockDrawer.Instance);
        public static readonly AiInterestCategory Enemy = new AiInterestCategory(new Color(0.35f, 0.35f, 0.35f), 10, BlockDrawer.Instance);
        public static readonly AiInterestCategory Obstacle = new AiInterestCategory(Color.white, 9, BlockDrawer.Instance);

        public static readonly AiInterestCategory Turret = new AiInterestCategory(new Color(0.75f, 0.75f, 0.75f), 8, BlockDrawer.Instance);

        //we need a lot more thinking on this
        public static readonly AiInterestCategory TurretRange = new AiInterestCategory(Turret.Color, 7, CircleDrawer.Instance);
        public static readonly AiInterestCategory Ground = new AiInterestCategory(Color.grey, 5, BlockDrawer.Instance);
        public static readonly AiInterestCategory NullArea = new AiInterestCategory(Color.white, 0, BlockDrawer.Instance);

        #endregion

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