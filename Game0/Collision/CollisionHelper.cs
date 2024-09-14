using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game0.Collision
{
    public static class CollisionHelper
    {
        /// <summary>
        /// Detects a collision between a rectangle and a circle
        /// </summary>
        /// <param name="c">the BoundingCircle</param>
        /// <param name="r">the BoundingRectangle</param>
        /// <returns>ture for collision, false otherwise</returns>
        public static bool Collides(BoundingCircle c, BoundingRectangle r)
        {
            float nearestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);
            return Math.Pow(c.Radius, 2) >=
                Math.Pow(c.Center.X - nearestX, 2) +
                Math.Pow(c.Center.Y - nearestY, 2);
        }
    }
}
