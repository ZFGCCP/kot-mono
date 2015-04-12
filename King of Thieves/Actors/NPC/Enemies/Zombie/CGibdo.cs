using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Zombie
{
    class CGibdo : CBaseZombie
    {
        private const string _WALK_DOWN = _SPRITE_NAMESPACE + ":gibdoWalkDown";
        private const string _WALK_UP = _SPRITE_NAMESPACE + ":gibdoWalkUp";
        private const string _WALK_RIGHT = _SPRITE_NAMESPACE + ":gibdoWalkRight";
        private const string _WALK_LEFT = _SPRITE_NAMESPACE + ":gibdoWalkLeft";

        private const string _GRAB_DOWN = _SPRITE_NAMESPACE + ":gibdoGrabDown";
        private const string _GRAB_UP = _SPRITE_NAMESPACE + ":gibdoGrabUp";
        private const string _GRAB_RIGHT = _SPRITE_NAMESPACE + ":gibdoGrabRight";
        private const string _GRAB_LEFT = _SPRITE_NAMESPACE + ":gibdoGrabLeft";

        private const string _HOLD_DOWN = _SPRITE_NAMESPACE + ":gibdoHoldDown";
        private const string _HOLD_UP = _SPRITE_NAMESPACE + ":gibdoHoldUp";
        private const string _HOLD_RIGHT = _SPRITE_NAMESPACE + ":gibdoHoldRight";
        private const string _HOLD_LEFT = _SPRITE_NAMESPACE + ":gibdoHoldLeft";
        private const int _TURN_TIME = 120;

        private static int _gibdoCount = 0;

        public CGibdo()
            : base()
        {
            _state = ACTOR_STATES.MOVING;

            if (_gibdoCount <= 0)
            {
                Graphics.CTextures.addTexture(_WALK_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 64, 48, 1, "0:1", "6:1", 5));
                Graphics.CTextures.addTexture(_WALK_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 64, 48, 1, "0:0", "6:0", 5));
                Graphics.CTextures.addTexture(_WALK_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 64, 48, 1, "0:2", "6:2", 5));

                Graphics.CTextures.addTexture(_GRAB_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 64, 48, 1, "0:4", "5:4", 5));
                Graphics.CTextures.addTexture(_GRAB_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 64, 48, 1, "0:3", "5:3", 5));
                Graphics.CTextures.addTexture(_GRAB_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 64, 48, 1, "0:5", "5:5", 5));

                Graphics.CTextures.addTexture(_HOLD_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 64, 48, 1, "5:4", "5:4", 0));
                Graphics.CTextures.addTexture(_HOLD_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 64, 48, 1, "5:3", "5:3", 0));
                Graphics.CTextures.addTexture(_HOLD_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 64, 48, 1, "5:5", "5:5", 0));
            }

            _imageIndex.Add(_WALK_DOWN, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_UP, new Graphics.CSprite(_WALK_UP));
            _imageIndex.Add(_WALK_LEFT, new Graphics.CSprite(_WALK_LEFT));
            _imageIndex.Add(_WALK_RIGHT, new Graphics.CSprite(_WALK_LEFT,true));

            _imageIndex.Add(_GRAB_DOWN, new Graphics.CSprite(_GRAB_DOWN));
            _imageIndex.Add(_GRAB_UP, new Graphics.CSprite(_GRAB_UP));
            _imageIndex.Add(_GRAB_LEFT, new Graphics.CSprite(_GRAB_LEFT));
            _imageIndex.Add(_GRAB_RIGHT, new Graphics.CSprite(_GRAB_LEFT,true));

            _imageIndex.Add(_HOLD_DOWN, new Graphics.CSprite(_HOLD_DOWN));
            _imageIndex.Add(_HOLD_UP, new Graphics.CSprite(_HOLD_UP));
            _imageIndex.Add(_HOLD_LEFT, new Graphics.CSprite(_HOLD_LEFT));
            _imageIndex.Add(_HOLD_RIGHT, new Graphics.CSprite(_HOLD_LEFT, true));

            startTimer0(_TURN_TIME);
            _direction = DIRECTION.DOWN;
            _state = ACTOR_STATES.MOVING;
            swapImage(_WALK_DOWN);
            _angle = 270;
            _lineOfSight = 90;
            _visionRange = 30;
            _hitBox = new Collision.CHitBox(this, 20, 25, 25, 25);
            
        }

        public override void keyRelease(object sender)
        {
            base.keyRelease(sender);

            if (shakeOffMeter >= _shakeOffThreshold)
            {
                resetShakeOffMeter();
            }
        }

        public override void destroy(object sender)
        {
            _gibdoCount -= 1;
            base.destroy(sender);
        }

        protected override void cleanUp()
        {
            base.cleanUp();
        }

        public override void timer0(object sender)
        {
            _changeDirection();
            startTimer0(_TURN_TIME);
        }

        private void _changeDirection()
        {
            DIRECTION oldDirection = _direction;
            _direction = (DIRECTION)_randNum.Next(0, 4);

            if (oldDirection != _direction && _screecherExists)
                _killScreecher();

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

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            moveInDirection(_velocity);
        }

        public override void collide(object sender, CActor collider)
        {
            if (collider is Actors.Collision.CSolidTile)
            {
                _changeDirection();
                solidCollide(collider);
            }
            else if (collider is Actors.Player.CPlayer)
            {
                if (_state == ACTOR_STATES.MOVING)
                {
                    _state = ACTOR_STATES.ATTACK;

                    switch (_direction)
                    {
                        case DIRECTION.DOWN:
                            swapImage(_GRAB_DOWN);
                            break;

                        case DIRECTION.UP:
                            swapImage(_GRAB_UP);
                            break;

                        case DIRECTION.LEFT:
                            swapImage(_GRAB_LEFT);
                            break;

                        case DIRECTION.RIGHT:
                            swapImage(_GRAB_RIGHT);
                            break;
                    }
                }
            }
        }

        public override void animationEnd(object sender)
        {
            if (_state == ACTOR_STATES.ATTACK)
            {
                _state = ACTOR_STATES.HOLD;
                switch (_direction)
                {
                    case DIRECTION.DOWN:
                        swapImage(_HOLD_DOWN);
                        break;

                    case DIRECTION.UP:
                        swapImage(_HOLD_UP);
                        break;

                    case DIRECTION.LEFT:
                        swapImage(_HOLD_LEFT);
                        break;

                    case DIRECTION.RIGHT:
                        swapImage(_HOLD_RIGHT);
                        break;
                }
            }
        }
    }
}
