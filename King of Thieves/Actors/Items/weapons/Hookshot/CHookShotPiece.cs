using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Items.weapons.Hookshot
{
    class CHookShotPiece : CActor
    {
        public CHookShotPiece(Vector2 speed, DIRECTION direction)
        {
            _direction = direction;
            _velocity = speed;
        }

        public override void create(object sender)
        {
            _state = ACTOR_STATES.EXTEND;
            startTimer0(60);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            moveInDirection(_velocity);
        }

        public override void timer0(object sender)
        {
            if (_state == ACTOR_STATES.EXTEND)
                _retract(60);
            else
                _killMe = true;
        }

        protected void _retract(int retractTime)
        {
            _state = ACTOR_STATES.RETRACT;
            startTimer0(retractTime);
            _velocity *= -1;
        }

        private void _retractEvent(object sender)
        {
            _retract((int)userParams[0]);
        }

        protected override void _registerUserEvents()
        {
            base._registerUserEvents();
            _userEvents.Add(0, _retractEvent);
        }
    }
}
