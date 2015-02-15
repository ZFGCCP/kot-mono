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
        private const int _SWOOP_RADIUS = 90;
        private Vector2 _homePosition = Vector2.Zero;
        private Vector2 _swoopTarget = Vector2.Zero;
        private int _moveSpeed = 1;

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
            if (_overgrownKeeseCount <= 0)
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/overgownKeese");

                Graphics.CTextures.addTexture(_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "1:2", "1:2", 0));
                Graphics.CTextures.addTexture(_IDLE_STARE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "0:2", "0:2", 0));
                Graphics.CTextures.addTexture(_FLY, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "0:0", "3:1", 20));
                Graphics.CTextures.addTexture(_KNOCKED_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "2:2", "2:2", 0));
                Graphics.CTextures.addTexture(_SWOOP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "0:1", "0:1", 0));
            }

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
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            if (_state != ACTOR_STATES.FROZEN)
            {
                if (_state == ACTOR_STATES.GO_HOME)
                {
                    moveToPoint(_homePosition.X, _homePosition.Y, 1, false);

                    if (MathExt.MathExt.checkPointWithinRange(_position,_homePosition- new Vector2(1,1),_homePosition+ new Vector2(1,1)))
                    {
                        _state = ACTOR_STATES.IDLE;
                        swapImage(_IDLE);
                    }
                }
                else if(_state == ACTOR_STATES.ATTACK)
                {
                    moveToPoint(_swoopTarget.X,_swoopTarget.Y,2,false);

                    if ((_position.X >= _swoopTarget.X - 2 && _position.X <= _swoopTarget.X + 2) &&
                        (_position.Y >= _swoopTarget.Y - 2 && _position.Y <= _swoopTarget.Y + 2))
                    {
                        _state = ACTOR_STATES.IDLE;
                        swapImage(_IDLE);
                    }
                }

                if (isPointInHearingRange(playerPos))
                {
                    if (_state != ACTOR_STATES.CHASE && state != ACTOR_STATES.ATTACK)
                    {
                        if (MathExt.MathExt.checkPointInCircle(playerPos, _position, _ATTACK_RADIUS))
                        {
                            _state = ACTOR_STATES.CHASE;
                            swapImage(_FLY);
                        }
                        else
                        {
                            _state = ACTOR_STATES.IDLE_STARE;
                            swapImage(_IDLE_STARE);
                        }
                    }
                    else if(_state != ACTOR_STATES.ATTACK)
                    {
                        moveToPoint(Player.CPlayer.glblX, Player.CPlayer.glblY, _moveSpeed, false);

                        if (MathExt.MathExt.checkPointInCircle(playerPos, _position, _SWOOP_RADIUS))
                        {
                            _state = ACTOR_STATES.ATTACK;
                            swapImage(_SWOOP);
                            _chooseSwoopTarget();
                        }
                    }
                }
                else
                {
                    if (_state == ACTOR_STATES.CHASE)
                    {
                        _state = ACTOR_STATES.GO_HOME;
                    }
                    else if(_state != ACTOR_STATES.GO_HOME)
                    {
                        _state = ACTOR_STATES.IDLE;
                        swapImage(_IDLE);
                    }
                }
            }
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);

        }

        protected override void cleanUp()
        {
            Graphics.CTextures.cleanUp(_SPRITE_NAMESPACE);
            _overgrownKeeseCount = 0;
            base.cleanUp();
        }

        public override void destroy(object sender)
        {
            _overgrownKeeseCount--;

            if (_overgrownKeeseCount <= 0)
            {
                cleanUp();
                _overgrownKeeseCount = 0;
            }

            base.destroy(sender);
        }

        private void _chooseSwoopTarget()
        {
            Vector2 target = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            target.X = target.X - (float)(Math.Sign(target.X) * 60.0);
            target.Y = target.Y - (float)(Math.Sign(target.Y) * 60.0);
            _swoopTarget = target;
        }
    }
}
