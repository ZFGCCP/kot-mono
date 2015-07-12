using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Collision
{
    class CDoor : CActor
    {
        private bool _locked = false;

        public CDoor() :
            base()
        {
            _hitBox = new CHitBox(this, 0, 0, 16, 16);
        }
    }
}
