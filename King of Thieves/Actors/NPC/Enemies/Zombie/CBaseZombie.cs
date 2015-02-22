using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Zombie
{
    class CBaseZombie : CBaseEnemy
    {
        private const int _STUN_TIME = 120;
        private const int _SCREECH_RADIUS = 120;

        public CBaseZombie()
            : base()
        {

        }

        protected virtual void _screech(CActor actorToFreeze)
        {
            actorToFreeze.stun(_STUN_TIME);
        }

        protected virtual void _grab()
        {

        }
    }
}
