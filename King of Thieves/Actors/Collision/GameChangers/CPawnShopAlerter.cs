using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Collision.GameChangers
{
    class CPawnShopAlerter : CCollidable
    {
        public CPawnShopAlerter() :
            base()
        {

        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            additional = new string[] { "64","16" };
            base.init(name, position, dataType, compAddress, additional);
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CPlayer));
        }

        public override void collide(object sender, CActor collider)
        {
            CMasterControl.mapManager.flipFlag(0);
            _killMe = true;
        }
    }
}
