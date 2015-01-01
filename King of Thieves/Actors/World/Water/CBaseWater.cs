using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.World.Water
{
    class CBaseWater : CActor
    {
        public CBaseWater()
            : base()
        {

        }

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            //Create a default hitbox the size of a tile
            _hitBox = new Collision.CHitBox(this, 0, 0, 16, 16);
        }

    }
}
