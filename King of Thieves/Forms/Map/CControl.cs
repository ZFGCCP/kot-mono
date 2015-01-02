using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Forms.Map
{
    class CControl
    {
        private Texture2D _rectangle = null;
        private Microsoft.Xna.Framework.Rectangle _rectCoords;
        protected static SpriteFont font = CMasterControl.glblContent.Load<SpriteFont>("Font/sherwood");

        public Vector2 position;

        public CControl()
        {
            _rectangle = new Texture2D(Graphics.CGraphics.GPU, 1,1);
            _rectangle.SetData(new System.Drawing.Color[] { System.Drawing.Color.White });
        }

        public virtual void drawRect(Microsoft.Xna.Framework.Color color, Vector2 position, Vector2 size, SpriteBatch spriteBatch)
        {
            _rectCoords.Width = (int)size.X;
            _rectCoords.Height = (int)size.Y;
            _rectCoords.X = (int)position.X;
            _rectCoords.Y = (int)position.Y;

            spriteBatch.Draw(_rectangle, _rectCoords, color);
        }

        public virtual void draw(SpriteBatch spriteBatch){}
    }
}
