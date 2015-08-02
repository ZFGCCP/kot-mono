using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Actors.Collision;

namespace King_of_Thieves.Actors.Items.decoration
{
    class CDoor : CActor
    {
        private bool _locked = false;

        public CDoor() :
            base()
        {
            _hitBox = new CHitBox(this, 0, 0, 16, 16);
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            _locked = Convert.ToInt32(additional[0]) > 0;
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CPlayer));
        }

        public override void collide(object sender, CActor collider)
        {
            _killMe = true;
        }
    }
}
