using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Bokoblin
{
    class CBokoblin : CBaseEnemy
    {
        private const string _SPRITE_NAMESPACE = "npc:bokoblin";

        private const string _WALK_DOWN = _SPRITE_NAMESPACE + ":walkDown";
        private const string _WALK_UP = _SPRITE_NAMESPACE + ":walkUp";
        private const string _WALK_LEFT = _SPRITE_NAMESPACE + ":walkLeft";
        private const string _WALK_RIGHT = _SPRITE_NAMESPACE + ":walkRight";

        private const string _IDLE_DOWN = _SPRITE_NAMESPACE + ":idleDown";
        private const string _IDLE_UP = _SPRITE_NAMESPACE + ":idleUp";
        private const string _IDLE_LEFT = _SPRITE_NAMESPACE + ":idleLeft";
        private const string _IDLE_RIGHT = _SPRITE_NAMESPACE + ":idleRight";

        private const string _ATTACK_DOWN = _SPRITE_NAMESPACE + ":attackDown";
        private const string _ATTACK_UP = _SPRITE_NAMESPACE + ":attackUp";
        private const string _ATTACK_LEFT = _SPRITE_NAMESPACE + ":attackLeft";
        private const string _ATTACK_RIGHT = _SPRITE_NAMESPACE + ":attackRight";

        private static int _bokoblinCount = 0;
        private const int _ATTACK_RADIUS = 40;

        public CBokoblin() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/bokoblin");

                Graphics.CTextures.addTexture(_WALK_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41,1,"0:0","3:0",20));
                Graphics.CTextures.addTexture(_WALK_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41, 1, "0:4", "3:4", 20));
                Graphics.CTextures.addTexture(_WALK_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41, 1, "0:2", "3:2", 20));

                Graphics.CTextures.addTexture(_IDLE_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41, 1, "0:0", "0:0", 0));
                Graphics.CTextures.addTexture(_IDLE_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41, 1, "0:4", "0:4", 0));
                Graphics.CTextures.addTexture(_IDLE_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41, 1, "0:2", "0:2", 0));

                Graphics.CTextures.addTexture(_ATTACK_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41, 1, "0:1", "2:1", 0));
                Graphics.CTextures.addTexture(_ATTACK_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41, 1, "0:5", "2:5", 0));
                Graphics.CTextures.addTexture(_ATTACK_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41, 1, "0:3", "2:3", 0));
            }

            _imageIndex.Add(_WALK_DOWN, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_UP, new Graphics.CSprite(_WALK_UP));
            _imageIndex.Add(_WALK_RIGHT, new Graphics.CSprite(_WALK_RIGHT));
            _imageIndex.Add(_WALK_LEFT, new Graphics.CSprite(_WALK_RIGHT,true));

            _imageIndex.Add(_IDLE_DOWN, new Graphics.CSprite(_IDLE_DOWN));
            _imageIndex.Add(_IDLE_UP, new Graphics.CSprite(_IDLE_UP));
            _imageIndex.Add(_IDLE_RIGHT, new Graphics.CSprite(_IDLE_RIGHT));
            _imageIndex.Add(_IDLE_LEFT, new Graphics.CSprite(_IDLE_RIGHT, true));

            _imageIndex.Add(_ATTACK_DOWN, new Graphics.CSprite(_ATTACK_DOWN));
            _imageIndex.Add(_ATTACK_UP, new Graphics.CSprite(_ATTACK_UP));
            _imageIndex.Add(_ATTACK_RIGHT, new Graphics.CSprite(_ATTACK_RIGHT));
            _imageIndex.Add(_ATTACK_LEFT, new Graphics.CSprite(_ATTACK_RIGHT, true));

            _bokoblinCount += 1;
            _goIdle();
            _lineOfSight = 50;
            _visionRange = 60;
            _hearingRadius = 100;
        }

        public override void destroy(object sender)
        {
            _bokoblinCount -= 1;
            _doNpcCountCheck(ref _bokoblinCount);
            base.destroy(sender);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case ACTOR_STATES.MOVING:
                    moveInDirection(_velocity);
                    _searchForPlayer();
                    break;

                case ACTOR_STATES.CHASE:
                    _chasePlayer();
                    break;
            }
        }

        private void _goIdle()
        {
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    _angle = 270;
                    swapImage(_IDLE_DOWN);
                    break;

                case DIRECTION.LEFT:
                    _angle = 180;
                    swapImage(_IDLE_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    _angle = 0;
                    swapImage(_IDLE_RIGHT);
                    break;

                case DIRECTION.UP:
                    _angle = 90;
                    swapImage(_IDLE_UP);
                    break;

                default:
                    break;
            }

            startTimer0(120);
            _state = ACTOR_STATES.IDLE;
        }

        public override void timer0(object sender)
        {
            _state = ACTOR_STATES.MOVING;
            _chooseDirection();
            startTimer1(150);
        }

        public override void timer1(object sender)
        {
            _goIdle();
        }

        private void _chooseDirection()
        {
            DIRECTION oldDirection = _direction;
            _direction = (DIRECTION)_randNum.Next(0, 4);

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    _velocity = new Microsoft.Xna.Framework.Vector2(0, .25f);
                    _angle = 270;
                    swapImage(_WALK_DOWN);
                    break;

                case DIRECTION.LEFT:
                    _velocity = new Microsoft.Xna.Framework.Vector2(-.25f, 0);
                    _angle = 180;
                    swapImage(_WALK_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    _velocity = new Microsoft.Xna.Framework.Vector2(.25f, 0);
                    _angle = 0;
                    swapImage(_WALK_RIGHT);
                    break;

                case DIRECTION.UP:
                    _velocity = new Microsoft.Xna.Framework.Vector2(0, -.25f);
                    _angle = 90;
                    swapImage(_WALK_UP);
                    break;

                default:
                    break;
            }
        }

        private void _searchForPlayer()
        {
            Vector2 playerPos = Vector2.Zero;

            playerPos.X = Player.CPlayer.glblX;
            playerPos.Y = Player.CPlayer.glblY;

            if (isPointInHearingRange(playerPos))
                _state = ACTOR_STATES.CHASE;
            else
            {
                if (_state == ACTOR_STATES.CHASE)
                    _goIdle();
            }
        }

        private void _chasePlayer()
        {
            Vector2 playerPos = Vector2.Zero;

            playerPos.X = Player.CPlayer.glblX;
            playerPos.Y = Player.CPlayer.glblY;

            _direction = moveToPoint2(playerPos.X, playerPos.Y, 1);

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(_WALK_DOWN);
                    break;

                case DIRECTION.LEFT:
                    swapImage(_WALK_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(_WALK_RIGHT);
                    break;

                case DIRECTION.UP:
                    swapImage(_WALK_UP);
                    break;
            }

            if (MathExt.MathExt.checkPointInCircle(_position, playerPos, _ATTACK_RADIUS))
            {
                _state = ACTOR_STATES.ALERT;
                startTimer2(60);
            }
        }

        public override void timer2(object sender)
        {
            _attack();
        }

        private void _attack()
        {
            _state = ACTOR_STATES.ATTACK;

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(_ATTACK_DOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(_ATTACK_UP);
                    break;

                case DIRECTION.LEFT:
                    swapImage(_ATTACK_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(_ATTACK_RIGHT);
                    break;
            }
        }

        public override void animationEnd(object sender)
        {
            if (_state == ACTOR_STATES.ATTACK)
                _goIdle();
        }
    }
}
