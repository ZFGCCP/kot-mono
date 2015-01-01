using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    class CVertex
    {
        public float X
        {
            get
            {
                return _coordinates.X;
            }

            set
            {
                _coordinates.X = value;
            }
        }

        public float Y
        {
            get
            {
                return _coordinates.Y;
            }

            set
            {
                _coordinates.Y = value;
            }
        }

        private Vector2 _coordinates;
        
    }
}
