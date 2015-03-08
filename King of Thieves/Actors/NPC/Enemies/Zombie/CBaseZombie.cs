using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);

            switch (_state)
            {
                case ACTOR_STATES.IDLE:
                    if (_checkLineofSight(playerPos.X, playerPos.Y))
                    {

                    }
                    else if (isPointInHearingRange(playerPos))
                        moveToPoint2(playerPos.X, playerPos.Y, .25f);

                    break;

                default:
                    break;
            }
        }
    }
}
