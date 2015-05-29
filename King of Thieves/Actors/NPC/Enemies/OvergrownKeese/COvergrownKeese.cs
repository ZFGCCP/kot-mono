using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.OvergrownKeese
{
    class COvergrownKeese : CBaseEnemy
    {
        private const int _ATTACK_RADIUS = 120;
        private const int _SWOOP_RADIUS = 50;
        private Vector2 _homePosition = Vector2.Zero;
        private Vector2 _swoopTarget = Vector2.Zero;

        private static int _overgrownKeeseCount = 0;
        private static string _SPRITE_NAMESPACE = "npc:overgrownKeese";
        private static string _IDLE = _SPRITE_NAMESPACE + ":idle";
        private static string _IDLE_STARE = _SPRITE_NAMESPACE + ":idleStare";
        private static string _FLY = _SPRITE_NAMESPACE + ":fly";
        private static string _KNOCKED_DOWN = _SPRITE_NAMESPACE + ":knockedDown";
        private static string _SWOOP = _SPRITE_NAMESPACE + ":swoop";

        public COvergrownKeese() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/overgownKeese");

                Graphics.CTextures.addTexture(_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "1:2", "1:2", 0));
                Graphics.CTextures.addTexture(_IDLE_STARE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "0:2", "0:2", 0));
                Graphics.CTextures.addTexture(_FLY, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "0:0", "3:1", 20));
                Graphics.CTextures.addTexture(_KNOCKED_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "2:2", "2:2", 0));
                Graphics.CTextures.addTexture(_SWOOP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "0:1", "0:1", 0));
            }

            _imageIndex.Add(_MAP_ICON, new Graphics.CSprite(_IDLE));
            _imageIndex.Add(_IDLE, new Graphics.CSprite(_IDLE));
            _imageIndex.Add(_IDLE_STARE, new Graphics.CSprite(_IDLE_STARE));
            _imageIndex.Add(_FLY, new Graphics.CSprite(_FLY));
            _imageIndex.Add(_KNOCKED_DOWN, new Graphics.CSprite(_KNOCKED_DOWN));
            _imageIndex.Add(_SWOOP, new Graphics.CSprite(_SWOOP));

            _overgrownKeeseCount++;

            _hearingRadius = 150;
            _state = ACTOR_STATES.IDLE;
            _direction = DIRECTION.DOWN;
            _angle = 270;
            swapImage(_IDLE);
            _hitBox = new Collision.CHitBox(this, 19, 15, 15, 23);
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CPlayer));
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);

            switch (_state)
            {
                case ACTOR_STATES.IDLE:
                    if (MathExt.MathExt.checkPointInCircle(playerPos,_position,_hearingRadius))
                    {
                        swapImage(_IDLE_STARE);
                        _state = ACTOR_STATES.IDLE_STARE;
                    }
                    break;

                case ACTOR_STATES.IDLE_STARE:
                    if (MathExt.MathExt.checkPointInCircle(playerPos, _position, _ATTACK_RADIUS))
                    {
                        _state = ACTOR_STATES.CHASE;
                        swapImage(_FLY);
                    }
                    else if (!MathExt.MathExt.checkPointInCircle(playerPos, _position, _hearingRadius))
                    {
                        swapImage(_IDLE);
                        _state = ACTOR_STATES.IDLE;
                    }
                    break;

                case ACTOR_STATES.CHASE:
                    moveToPoint2(playerPos.X, playerPos.Y, .5f, false);

                    if (!MathExt.MathExt.checkPointInCircle(playerPos, _position, _hearingRadius))
                    {
                        _state = ACTOR_STATES.GO_HOME;
                    }
                    else if (MathExt.MathExt.checkPointInCircle(playerPos, _position, _SWOOP_RADIUS))
                    {
                        _state = ACTOR_STATES.ATTACK;
                        swapImage(_SWOOP);
                        _chooseSwoopTarget();
                    }
                    break;

                case ACTOR_STATES.GO_HOME:
                    moveToPoint2(_homePosition.X, _homePosition.Y, .5f, false);

                    if (MathExt.MathExt.checkPointInCircle(playerPos, _position, _hearingRadius))
                        _state = ACTOR_STATES.CHASE;
                    else if (_homePosition == _position)
                    {
                        _state = ACTOR_STATES.IDLE;
                        swapImage(_IDLE);
                    }
                    break;

                case ACTOR_STATES.ATTACK:
                    moveToPoint(_swoopTarget.X, _swoopTarget.Y, 2.0f, false);

                    if ((_position.X >= _swoopTarget.X - 2 && _position.X <= _swoopTarget.X + 2) &&
                        (_position.Y >= _swoopTarget.Y - 2 && _position.Y <= _swoopTarget.Y + 2))
                    {
                        _state = ACTOR_STATES.CHASE;
                        swapImage(_FLY);
                    }
                    break;

                default:
                    break;

            }
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);

        }

        public override void destroy(object sender)
        {
            _overgrownKeeseCount -= 1;
            _doNpcCountCheck(ref _overgrownKeeseCount);
            base.destroy(sender);
        }

        private void _chooseSwoopTarget()
        {
            Vector2 target = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            double angle = MathExt.MathExt.angle(_position, target);
            _swoopTarget = MathExt.MathExt.choosePointOnAngle(angle, 120) + _position;
        }
    }
}
