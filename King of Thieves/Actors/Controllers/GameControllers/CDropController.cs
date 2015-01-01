using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Controllers.GameControllers
{
    class CDropController : CActor
    {
        public CDropController() :
            base()
        {
            init("dropController", Vector2.Zero, "King_of_Thieves.Actors.Controllers.GameControllers.CDropController", CReservedAddresses.DROP_CONTROLLER);
        }
    }
}
