using System;
using System.Collections.Generic;
using System.Linq;
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

        private static int _bokoblinCount = 0;

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
            }

            _imageIndex.Add(_WALK_DOWN, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_UP, new Graphics.CSprite(_WALK_UP));
            _imageIndex.Add(_WALK_RIGHT, new Graphics.CSprite(_WALK_RIGHT));
            _imageIndex.Add(_WALK_LEFT, new Graphics.CSprite(_WALK_RIGHT,true));

            _imageIndex.Add(_IDLE_DOWN, new Graphics.CSprite(_IDLE_DOWN));
            _imageIndex.Add(_IDLE_UP, new Graphics.CSprite(_IDLE_UP));
            _imageIndex.Add(_IDLE_RIGHT, new Graphics.CSprite(_IDLE_RIGHT));
            _imageIndex.Add(_IDLE_LEFT, new Graphics.CSprite(_IDLE_RIGHT, true));

            _bokoblinCount += 1;
            _goIdle();
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
    }
}
