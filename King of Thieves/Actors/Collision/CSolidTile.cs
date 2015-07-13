using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Collision
{
    class CSolidTile : CActor
    {
        public CSolidTile() : base()
        {
            _hitBox = new CHitBox(this, 0, 0, 16, 16);
            _followRoot = false;
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            if (additional.Length >= 2)
                _hitBox = new CHitBox(this, 0, 0, Convert.ToInt32(additional[0]), Convert.ToInt32(additional[1]));
        }

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }
    }
}
