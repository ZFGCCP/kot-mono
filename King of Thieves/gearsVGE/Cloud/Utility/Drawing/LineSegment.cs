using Microsoft.Xna.Framework;

namespace Gears.Cloud.Utility.Drawing
{
    public struct LineSegment
    {
        private float angle;
        private Vector2 origin;
        private Color color;
        private Vector2 scale;

        public float GetAngle()
        {
            return this.angle;
        }
        public void SetAngle(float angle)
        {
            this.angle = angle;
        }
        public Vector2 GetOrigin()
        {
            return this.origin;
        }
        public void SetOrigin(Vector2 origin)
        {
            this.origin = origin;
        }
        public Color GetColor()
        {
            return this.color;
        }
        public void SetColor(Color color)
        {
            this.color = color;
        }
        public Vector2 GetScale()
        {
            return this.scale;
        }
        public void SetScale(Vector2 scale)
        {
            this.scale = scale;
        }
    }
}
