using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    class CRect : CRenderable
    {
        private Color _color;
        private Texture2D _texColor = null;
        private Rectangle _destRect;

        public CRect(int width, int height, int x, int y, Color color, Effect shader = null, params VertexPositionColor[] vertices) :
            base(shader, vertices)
        {
            _position.X = x;
            _position.Y = y;
            _color = color;
            _size.Width = width;
            _size.Height = height;

            _texColor = new Texture2D(Graphics.CGraphics.GPU, 16, 16);
            Color[] data = new Color[256];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;

            _texColor.SetData(data);
        }

        public override bool draw(int x, int y, bool useOverlay = false, SpriteBatch spriteBatch = null)
        {
            _position.X = x;
            _position.Y = y;
            _destRect.X = (int)_position.X;
            _destRect.Y = (int)_position.Y;
            _destRect.Width = width;
            _destRect.Height = height;

            Graphics.CGraphics.spriteBatch.Draw(_texColor, _destRect, Color.White);
            return true;
        }
    }
}
