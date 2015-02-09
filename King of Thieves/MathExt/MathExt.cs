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
            double result = Math.Atan2(point1.Y - point2.Y, point2.X - point1.X) * (180/Math.PI);
            if (result < 0)
                result += 360;

            return result;
        }

        public static Vector2 crossProduct2(Vector2 u, Vector2 v)
        {
            Vector2 result = Vector2.Zero;
            result = new Vector2(u.X * v.Y, - u.Y * v.X);

            return result;
        }

        public static double dotProduct2(Vector2 u, Vector2 v)
        {
            double result = 0;
            result = (u.X * v.X) + (u.X * v.Y) + (u.Y * v.X) + (u.Y * v.Y);

            return result;
        }
    }
}
