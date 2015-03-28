using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Items.shields
{
    class CBaseShield : CActor
    {
        protected Collision.CHitBox _downBox;
        protected Collision.CHitBox _sideBox;

        public CBaseShield() :
            base()
        {
            _state = ACTOR_STATES.IDLE;
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
        }

        protected override void _registerUserEvents()
        {
            base._registerUserEvents();
            _userEvents.Add(0, _engage);
            _userEvents.Add(1, _disengage);
        }

        private void _engage(object sender)
        {
            _state = ACTOR_STATES.SHIELD_ENGAGE;
            _direction = (DIRECTION)userParams[0];
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_ENGAGE_DOWN);
                    break;

                default:
                    break;
            }
        }

        public override void animationEnd(object sender)
        {
            if (_state == ACTOR_STATES.SHIELD_ENGAGE)
            {
                _state = ACTOR_STATES.SHIELDING;
                _shieldHold();
            }
            else if (_state == ACTOR_STATES.SHIELD_DISENGAGE)
            {
                _state = ACTOR_STATES.IDLE;
                image = null;
            }
        }

        public void _disengage(object sender)
        {
            _state = ACTOR_STATES.SHIELD_DISENGAGE;
        }

        private void _shieldHold()
        {
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_DOWN);
                    _hitBox = _downBox;
                    break;
            }
        }
    }
}
