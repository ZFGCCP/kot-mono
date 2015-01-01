using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gears.Cloud.Utility.Drawing
{
    public class DrawingHelper
    {
        public void DefaultDrawLine(ref SpriteBatch spriteBatch, LineSegment line, ref Texture2D texture)
        {
            spriteBatch.Draw(texture, line.GetOrigin(), null, line.GetColor(), line.GetAngle(), Vector2.Zero, line.GetScale(), SpriteEffects.None, 0);
        }

        public void DefaultDrawRectangle(ref SpriteBatch spriteBatch, Rectangle rectangle, ref Texture2D texture, Color color)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }


        public LineSegment CreateLine(float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            LineSegment line = new LineSegment();
            line.SetAngle(angle);
            line.SetColor(color);
            line.SetOrigin(point1);
            line.SetScale(new Vector2(length, width));

            return line;
        }
    }
}
