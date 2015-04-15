using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Guards
{
    enum ALERT_LEVEL
    {
        NORMAL = 0,
        ELEVATED
    }

    class CCastleGuard : CBaseEnemy
    {
        private static ALERT_LEVEL _ALERT = ALERT_LEVEL.NORMAL;

        public CCastleGuard() :
            base()
        {
            _angle = 270;
            _direction = DIRECTION.DOWN;
        }


    }
}
