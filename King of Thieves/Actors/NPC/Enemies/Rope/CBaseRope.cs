using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.Rope
{
    enum ROPEETYPE
    {
        NORMAL = 0
    
    }

    class CBaseRope : CBaseEnemy
    {
        protected const string _SPRITE_NAMESPACE = "npc:rope";
        private static int _ropeCount = 0;
        private static int _greenRopeCount = 0;

         //image index constants
        protected const string _SLITHER_DOWN = "slitherDown";
        protected const string _SLITHER_UP = "slitherUp";
        protected const string _SLITHER_LEFT = "slitherLeft";
        protected const string _SLITHER_RIGHT = "slitherRight";

        //slither fast
        protected const string _FAST_SLITHER_DOWN = "fastslitherDown";
        protected const string _FAST_SLITHER_UP = "fastslitherUp";
        protected const string _FAST_SLITHER_LEFT = "fastslitherLeft";
        protected const string _FAST_SLITHER_RIGHT = "fastslitherRight";

        private const int _TURN_TIME = 140;

        private float _currentSpeed = 3.0f;


        public CBaseRope()
            : base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {

                Graphics.CTextures.rawTextures.Add(_SPRITE_NAMESPACE, CMasterControl.glblContent.Load<Texture2D>(@"sprites/npc/rope"));

                Graphics.CTextures.addTexture(_SLITHER_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:1", "3:1", 2));
                Graphics.CTextures.addTexture(_SLITHER_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:2", "3:2", 2));
                Graphics.CTextures.addTexture(_SLITHER_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:0", "3:0", 2));

                Graphics.CTextures.addTexture(_FAST_SLITHER_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:1", "3:1", 4));
                Graphics.CTextures.addTexture(_FAST_SLITHER_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:2", "3:2", 4));
                Graphics.CTextures.addTexture(_FAST_SLITHER_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:0", "3:0", 4));
            }
            _imageIndex.Add(_MAP_ICON, new Graphics.CSprite(_SLITHER_DOWN));
            _imageIndex.Add(_SLITHER_DOWN, new Graphics.CSprite(_SLITHER_DOWN));
            _imageIndex.Add(_SLITHER_UP, new Graphics.CSprite(_SLITHER_UP));
            _imageIndex.Add(_SLITHER_LEFT, new Graphics.CSprite(_SLITHER_LEFT));
            _imageIndex.Add(_SLITHER_RIGHT, new Graphics.CSprite(_SLITHER_LEFT,true));


            _imageIndex.Add(_FAST_SLITHER_DOWN, new Graphics.CSprite(_FAST_SLITHER_DOWN));
            _imageIndex.Add(_FAST_SLITHER_UP, new Graphics.CSprite(_FAST_SLITHER_UP));
            _imageIndex.Add(_FAST_SLITHER_LEFT, new Graphics.CSprite(_FAST_SLITHER_LEFT));
            _imageIndex.Add(_FAST_SLITHER_RIGHT, new Graphics.CSprite(_FAST_SLITHER_LEFT,true));

            _ropeCount += 1;


        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            try
            {
                switch (additional[0])
                {
                    case "G":
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new KotException.KotBadArgumentException(ex.Message);
            }

            swapImage( _SLITHER_DOWN);
            _direction = DIRECTION.DOWN;
            _state = ACTOR_STATES.MOVING;
            _angle = 270;
            _lineOfSight = 150;
            _visionRange = 30;
            startTimer0(_TURN_TIME);

        }

       
        public override void create(object sender)
        {
            base.create(sender);

        }

        public override void timer1(object sender)
        {
            base.timer1(sender);
            
        }

        public override void timer2(object sender)
        {
            base.timer2(sender);
        }

        public override void timer3(object sender)
        {
            base.timer3(sender);
        }

        public override void drawMe(bool useOverlay = false, SpriteBatch spriteBatch = null)
        {
            
                base.drawMe(useOverlay);
        }

        
        protected override void cleanUp()
        {
            Graphics.CTextures.cleanUp(_SPRITE_NAMESPACE);
            base.cleanUp();
        }

        public override void destroy(object sender)
        {
            _ropeCount--;

            if (_ropeCount <= 0)
            {
                cleanUp();
                _ropeCount = 0;
            }

            base.destroy(sender);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            moveInDirection(_velocity);
              if (_state == ACTOR_STATES.MOVING)
                {
                    Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
                  {

                    if (_checkIfPointInView(playerPos)) {

                        _state = ACTOR_STATES.CHASE;
                        _currentSpeed = 1.0f;
                        _charge();
                    }
                }
             }
        }

        private void _charge()
        {
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    _velocity = new Microsoft.Xna.Framework.Vector2(0, 1.0f / _currentSpeed);
                    swapImage(_FAST_SLITHER_DOWN);
                    break;

                case DIRECTION.LEFT:
                    _velocity = new Microsoft.Xna.Framework.Vector2(-1.0f / _currentSpeed, 0);
                    swapImage(_FAST_SLITHER_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    _velocity = new Microsoft.Xna.Framework.Vector2(1.0f / _currentSpeed, 0);
                    swapImage(_FAST_SLITHER_RIGHT);
                    break;

                case DIRECTION.UP:
                    _velocity = new Microsoft.Xna.Framework.Vector2(0, -1.0f / _currentSpeed);
                    swapImage(_FAST_SLITHER_UP);
                    break;

                default:
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
                    _velocity = new Microsoft.Xna.Framework.Vector2(0, 1.0f / _currentSpeed);
                    _angle = 270;
                    swapImage(_SLITHER_DOWN);
                    break;

                case DIRECTION.LEFT:
                    _velocity = new Microsoft.Xna.Framework.Vector2(-1.0f / _currentSpeed, 0);
                    _angle = 180;
                    swapImage(_SLITHER_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    _velocity = new Microsoft.Xna.Framework.Vector2(1.0f / _currentSpeed, 0);
                    _angle = 0;
                    swapImage(_SLITHER_RIGHT);
                    break;

                case DIRECTION.UP:
                    _velocity = new Microsoft.Xna.Framework.Vector2(0, -1.0f / _currentSpeed);
                    _angle = 90;
                    swapImage(_SLITHER_UP);
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
    }
}