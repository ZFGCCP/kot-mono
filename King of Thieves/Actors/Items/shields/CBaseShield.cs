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
        private Vector2 _leftOffset = new Vector2(10, 10);
        private Vector2 _rightOffset = new Vector2(20, 10);
        private Vector2 _upOffset = new Vector2(6, 9);

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
                    _position.Y += 5;
                    break;

                case DIRECTION.LEFT:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_LEFT);
                    _position.X -= 4;
                    _position.Y += 3;
                    break;

                case DIRECTION.RIGHT:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_RIGHT);
                    _position.X += 5;
                    _position.Y += 3;
                    break;

                case DIRECTION.UP:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_UP);
                    _position.X += 3;
                    _position.Y += 3;
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

                case DIRECTION.LEFT:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_RIGHT);
                    break;

                case DIRECTION.UP:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_UP);
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

                case DIRECTION.LEFT:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_LEFT);
                    _hitBox = _sideBox;
                    _hitBox.offset = _leftOffset;
                    break;

                case DIRECTION.RIGHT:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_RIGHT);
                    _hitBox = _sideBox;
                    _hitBox.offset = _rightOffset;
                    break;

                case DIRECTION.UP:
                    swapImage(Graphics.CTextures.WOOD_SHIELD_UP);
                    _hitBox = _downBox;
                    _hitBox.offset = _upOffset;
                    break;
            }
        }
    }
}
