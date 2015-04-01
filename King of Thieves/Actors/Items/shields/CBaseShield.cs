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
        protected Vector2 _offset = Vector2.Zero;

        private Vector2 _downOffset = new Vector2(8, 27);

        public CBaseShield() :
            base()
        {
            _state = ACTOR_STATES.IDLE;
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Projectiles.CProjectile));
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
            _position.X = (float)userParams[1];
            _position.Y = (float)userParams[2];
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
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_DISENGAGE_DOWN);
                    break;

                default:
                    break;
            }

            _hitBox = null;
        }

        private void _shieldHold()
        {
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_DOWN);
                    _hitBox = _downBox;
                    _hitBox.offset = _downOffset;
                    break;
            }
        }
    }
}
