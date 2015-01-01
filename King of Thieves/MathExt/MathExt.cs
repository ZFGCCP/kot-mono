using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.MathExt
{
    public class MathExt
    {
        public static double distance(Vector2 point1, Vector2 point2)
        {
            return Math.Sqrt(Math.Pow((point2.X - point1.X), 2) + Math.Pow((point2.Y - point1.Y), 2));
        }

        public static double angle(Vector2 point1, Vector2 point2)
        {
            return Math.Atan2(point1.Y - point2.Y, point2.X - point1.X) * (180/Math.PI);
        }
    }
}
