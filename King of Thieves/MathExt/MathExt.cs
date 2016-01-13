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

        public static double distance(double point1, double point2)
        {
            return Math.Sqrt(Math.Pow((point2 - point1),2));
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
            result = (u.X * v.X) + (u.Y * v.Y);

            return result;
        }

        public static bool checkPointInTriangle(Vector2 P, Vector2 A, Vector2 B, Vector2 C)
        {
            Vector2 v0 = C - A;
            Vector2 v1 = B - A;
            Vector2 v2 = P - A;

            double dot00 = dotProduct2(v0, v0);
            double dot01 = dotProduct2(v0, v1);
            double dot02 = dotProduct2(v0, v2);
            double dot11 = dotProduct2(v1, v1);
            double dot12 = dotProduct2(v1, v2);

            double invDenom = 1.0 / (dot00 * dot11 - dot01 * dot01);

            double u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            double v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            return (u >= 0) && (v >= 0) && (u + v < 1);
        }

        public static bool checkPointInCircle(Vector2 P, Vector2 A, int radius)
        {
            if (distance(A, P) <= radius)
                return true;
            
            return false;
        }

        public static double radiansToDegrees(double radians)
        {
            return radians * (180.0 / Math.PI);
        }

        public static double degreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        public static bool checkPointWithinRange(Vector2 P, Vector2 topLeft, Vector2 bottomRight)
        {
            return (P.X >= topLeft.X && P.X <= bottomRight.X) && (P.Y >= topLeft.Y && P.Y <= bottomRight.Y);
        }

        public static Vector2 choosePointOnAngle(double angle, int distance)
        {
            Vector2 vec = new Vector2();
            double theta = angle * Math.PI / 180.0;
            vec.X = (float)Math.Round(Math.Cos(theta) * distance);
            vec.Y = (float)-Math.Round(Math.Sin(theta) * distance);
            return vec;
        }

        public static Vector2 calculateVectorComponents(float magnitude, float angle)
        {
            Vector2 output = Vector2.Zero;
            double radians = degreesToRadians(angle);

            output.X = (float)(Math.Cos(radians) * magnitude);
            output.Y = -(float)(Math.Sin(radians) * magnitude);

            return output;
        }
    }
}
