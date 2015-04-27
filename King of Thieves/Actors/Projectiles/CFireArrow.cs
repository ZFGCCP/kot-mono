using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Projectiles
{
    class CFireArrow : CArrow
    {
        public CFireArrow(DIRECTION direction, Vector2 velocity, Vector2 position)
            : base(direction, velocity, position)
        {

        }
    }
}
