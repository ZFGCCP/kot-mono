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

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }
    }
}
