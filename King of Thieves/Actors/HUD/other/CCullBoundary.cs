using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.HUD.other
{
    class CCullBoundary : CHUDElement
    {
        private int _width = 304;
        private int _height = 224;

        public CCullBoundary() :
            base()
        {
            _fixedPosition.X = -32;
            _fixedPosition.Y = -32;
        }

        public bool checkPointWithinBoundary(Vector2 point, Vector2 dimensions)
        {
            point *= dimensions;
            return !(point.X < position.X ||
                   point.Y < position.Y ||
                   point.X > position.X + _width ||
                   point.Y > position.Y + _height);

        }
    }
}
