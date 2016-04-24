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

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CWaterPuddle));
        }

        public override void collide(object sender, CActor collider)
        {
            collider.state = ACTOR_STATES.IDLE;
        }

        public override void collideExit(object sender, CActor collider)
        {
            collider.state = ACTOR_STATES.INVISIBLE;
        }
    }
}
