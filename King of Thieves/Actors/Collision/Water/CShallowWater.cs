using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Collision.Water
{
    class CShallowWater : CCollidable
    {
        public CShallowWater() :
            base()
        {

        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
        }
    }
}
