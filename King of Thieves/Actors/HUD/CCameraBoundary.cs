using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.HUD
{
    class CCameraBoundary : CHUDElement
    {
        private Rectangle _boundingRect;

        public CCameraBoundary(Rectangle rect)
        {
            _boundingRect = rect;
        }

        public void move(int x, int y)
        {
            _boundingRect.X += x;
            _boundingRect.Y += y;
        }

        public int right
        {
            get
            {
                return _boundingRect.Right;
            }
        }

        public int left
        {
            get
            {
                return _boundingRect.Left;
            }
        }

        public int top
        {
            get
            {
                return _boundingRect.Top;
            }
        }

        public int bottom
        {
            get
            {
                return _boundingRect.Bottom;
            }
        }
    }
}
