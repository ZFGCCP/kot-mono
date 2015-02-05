using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.ArmoredCrab
{
    class CArmoredCrab : CBaseEnemy
    {
        Vector2 _moveToThis = Vector2.Zero;

        public CArmoredCrab() :
            base()
        {
            _hearingRadius = 3;
            _lineOfSight = 10;
            _visionRange = 90;

            //always looks down
            _direction = DIRECTION.DOWN;
            _angle = 270;
        }

        private void _chooseNewPoint()
        {
            _moveToThis = getRandomPointInSightRange();
            _state = ACTOR_STATES.MOVING;
        }

        public override void update(GameTime gameTime)
        {
            //check if the player is in hearing range
            if (isPointInHearingRange(new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY)))
                state = ACTOR_STATES.CHASE;

            if (state == ACTOR_STATES.MOVING)
            {
                moveToPoint(_moveToThis.X, _moveToThis.Y, 5);

                if ((_position.X <= _moveToThis.X - 2 && _position.X >= _moveToThis.X + 2) &&
                    (_position.Y <= _moveToThis.Y - 2 && _position.Y >= _moveToThis.Y + 2))
                {
                    state = ACTOR_STATES.IDLE;
                    startTimer0(120);
                }
            }

            base.update(gameTime);
        }

        public override void timer0(object sender)
        {
            _chooseNewPoint();
            base.timer0(sender);
        }


    }
}
