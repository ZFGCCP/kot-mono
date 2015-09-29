using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Collision
{
    class CLayerChanger : CActor
    {
        private int _toLayer;

        public CLayerChanger()
            : base()
        {

        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _hitBox = new CHitBox(this, 0, 0, Convert.ToInt32(additional[0]), Convert.ToInt32(additional[1]));
            _toLayer = Convert.ToInt32(additional[2]);
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CPlayer));
        }

        public override void collide(object sender, CActor collider)
        {
            Map.CMapManager.switchComponentLayer(collider.component, _toLayer);
        }


    }
}
