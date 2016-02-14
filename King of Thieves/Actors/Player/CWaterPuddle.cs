using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Player
{
    class CWaterPuddle : CActor
    {
        public CWaterPuddle() :
            base()
        {

        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Collision.Water.CShallowWater));
        }


    }
}
