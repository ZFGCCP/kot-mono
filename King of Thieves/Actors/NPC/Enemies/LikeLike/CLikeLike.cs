using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
                    }
                    break;

                case ACTOR_STATES.MOVING:
                    moveInDirection(_velocity);
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
    }
}
