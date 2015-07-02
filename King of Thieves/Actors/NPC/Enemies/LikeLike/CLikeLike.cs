using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors.NPC.Enemies.LikeLike
{
    class CLikeLike : CBaseEnemy
    {
        protected const string _SPRITE_NAMESPACE = "npc:likelike";

        protected const string _MOVE = _SPRITE_NAMESPACE + ":move";
        protected const string _HIDE = _SPRITE_NAMESPACE + ":hide";
        protected const string _POPUP = _SPRITE_NAMESPACE + ":popup";

        private static int _likeLikeCount = 0;
        private const int _TURN_TIME = 240;
        private int _shakeOffMeter = 0;
        protected int _shakeOffThreshold = 10;
        private Vector2 _shakeOffVelocity = Vector2.Zero;
        protected int _damagePerSec = 0;
        protected int _loseShieldTimer = 0;

        private CActor _actorToHug = null;

        public CLikeLike() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/likelike");

                Graphics.CTextures.addTexture(_MOVE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 16, 24, 1, "4:0", "9:0",10));
                Graphics.CTextures.addTexture(_HIDE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 16, 24, 1, "0:0", "0:0",0));
                Graphics.CTextures.addTexture(_POPUP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 16, 24, 1, "0:0", "4:0", 30));
            }

            _imageIndex.Add(_MOVE, new Graphics.CSprite(_MOVE));
            _imageIndex.Add(_HIDE, new Graphics.CSprite(_HIDE));
            _imageIndex.Add(_POPUP, new Graphics.CSprite(_POPUP));

            _likeLikeCount += 1;
            swapImage(_HIDE);
            _state = ACTOR_STATES.IDLE;
            _hearingRadius = 40;
        }

        public override void destroy(object sender)
        {
            _likeLikeCount -= 1;
            _doNpcCountCheck(ref _likeLikeCount);
            base.destroy(sender);
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Projectiles.CBomb));
            _collidables.Add(typeof(Items.Swords.CSword));
            _collidables.Add(typeof(Collision.CSolidTile));
            _collidables.Add(typeof(Player.CPlayer));
        }

        public override void collide(object sender, CActor collider)
        {
            if (collider is Actors.Collision.CSolidTile)
            {
                _changeDirection();
                solidCollide(collider);
            }
            else if (collider is Actors.Projectiles.CBomb && collider.state == ACTOR_STATES.EXPLODE)
            {
                _killMe = true;
            }
            else if (collider is Items.Swords.CSword)
            {
                _killMe = true;
            }
            else if (collider is Player.CPlayer)
            {
                if (_state == ACTOR_STATES.MOVING)
                {
                    _state = ACTOR_STATES.HOLD;
                    collider.stun(-1);
                    _loseShieldTimer = 0;
                    _actorToHug = collider;
                    _actorToHug.state = ACTOR_STATES.INVISIBLE;
                    startTimer2(60);
                }
            }
        }

        public override void keyRelease(object sender)
        {
            base.keyRelease(sender);

            if (_state == ACTOR_STATES.HOLD)
            {
                CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;

                if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.C))
                    _shakeOffMeter++;

                if (_shakeOffMeter >= _shakeOffThreshold)
                {
                    resetShakeOffMeter();
                    _state = ACTOR_STATES.SHOOK_OFF;
                    _setShakeOffVelo();
                    startTimer1(15);
                    _actorToHug.startTimer3(30);
                    _actorToHug = null;
                }
            }
        }

        protected void _setShakeOffVelo()
        {
            _shakeOffVelocity = Vector2.Zero;
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    _shakeOffVelocity.Y = -4;
                    break;

                case DIRECTION.UP:
                    _shakeOffVelocity.Y = 4;
                    break;

                case DIRECTION.LEFT:
                    _shakeOffVelocity.X = 4;
                    break;

                case DIRECTION.RIGHT:
                    _shakeOffVelocity.X = -4;
                    break;

                default:
                    break;
            }
        }

        protected void resetShakeOffMeter()
        {
            _shakeOffMeter = 0;
        }

        public override void timer2(object sender)
        {
            if (_actorToHug == null)
                return;

            _actorToHug.dealDamange(_damagePerSec, _actorToHug);
            _loseShieldTimer++;
            //todo: remove a random item.  For now, just make it bombs
            CMasterControl.buttonController.modifyBombs(-1);

            if (_loseShieldTimer >= 4)
            {
                //todo: remove shield if it's not the mirror shield
                _loseShieldTimer = 0;
            }

            if (_state == ACTOR_STATES.HOLD)
                startTimer2(60);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case ACTOR_STATES.IDLE:
                    Vector2 playerPos = new Vector2(Player.CPlayer.glblX,Player.CPlayer.glblY);
                    if (isPointInHearingRange(playerPos))
                    {
                        _state = ACTOR_STATES.POPUP;
                        swapImage(_POPUP);
                        _hitBox = new Collision.CHitBox(this, 0, 10, 15, 15);
                    }
                    break;

                case ACTOR_STATES.MOVING:
                    moveInDirection(_velocity);
                    break;

                case ACTOR_STATES.SHOOK_OFF:
                    _shakeOff();
                    break;
            }
        }

        public override void animationEnd(object sender)
        {
            base.animationEnd(sender);
            switch (_state)
            {
                case ACTOR_STATES.POPUP:
                    _state = ACTOR_STATES.MOVING;
                    swapImage(_MOVE);
                    startTimer0(1);
                    break;
            }
        }

        private void _changeDirection()
        {
            DIRECTION oldDirection = _direction;
            _direction = (DIRECTION)_randNum.Next(0, 4);

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    _velocity = new Microsoft.Xna.Framework.Vector2(0, .25f);
                    break;

                case DIRECTION.LEFT:
                    _velocity = new Microsoft.Xna.Framework.Vector2(-.25f, 0);
                    break;

                case DIRECTION.RIGHT:
                    _velocity = new Microsoft.Xna.Framework.Vector2(.25f, 0);
                    break;

                case DIRECTION.UP:
                    _velocity = new Microsoft.Xna.Framework.Vector2(0, -.25f);
                    break;

                default:
                    break;
            }
        }

        public override void timer0(object sender)
        {
            if (_state == ACTOR_STATES.MOVING)
                _changeDirection();

            startTimer0(_TURN_TIME);
        }

        private void _shakeOff()
        {
            moveInDirection(_shakeOffVelocity);
        }

        public override void timer1(object sender)
        {
            _state = ACTOR_STATES.MOVING;
            _changeDirection();
        }
    }
}
