using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.usr.local.MapTests
{
    class HryuleCastleTownTest : PlayableState
    {
        protected override void _initMap()
        {
            CMasterControl.mapManager.swapMap("castleTown.xml", "player", new Vector2(400, 1000));
        }
    }
}
