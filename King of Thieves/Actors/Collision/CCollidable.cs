using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Collision
{
    class CCollidable : CActor
    {
        private int _height;
        private int _width;

        public CCollidable() :
            base()
        {

        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _width = Convert.ToInt32(additional[0]);
            _height = Convert.ToInt32(additional[1]);
            _hitBox = new CHitBox(this, 0, 0, _width, _height);
        }

        public int height
        {
            get
            {
                return _height;
            }
        }

        public int width
        {
            get
            {
                return _width;
            }
        }
    }
}
