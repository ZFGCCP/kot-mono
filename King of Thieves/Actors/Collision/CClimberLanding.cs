using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Collision
{
    class CClimberLanding : CCollidable
    {
        private int _moveToLayer = 0;

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            additional[1] = "16";
            _hitBox = new CHitBox(this, 0, 0, Convert.ToInt32(additional[0]), 8);
            _moveToLayer = 3;
        }

        public int moveToLayer
        {
            get
            {
                return _moveToLayer;
            }
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CPlayer));
        }

        public override void collide(object sender, CActor collider)
        {
            Map.CMapManager.switchComponentLayer(collider.component, _moveToLayer);
        }
    }
}
