using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.MoldormTail
{
    class CMoldormTailHead : CMoldormTailPiece
    {
        private Vector2 _moveTowardsPoint = Vector2.Zero;

        public CMoldormTailHead()
            : base(true)
        {
            if (_moldormAndTailCount <= 0)
            {
                Graphics.CTextures.addTexture(_HEAD_UP, new Graphics.CTextureAtlas(_NPC_MOLDORM, 32, 32, 1, "0:0", "0:0"));
                Graphics.CTextures.addTexture(_HEAD_RIGHT, new Graphics.CTextureAtlas(_NPC_MOLDORM, 32, 32, 1, "0:1", "0:1"));

                Graphics.CTextures.addTexture(_HEAD_URIGHT, new Graphics.CTextureAtlas(_NPC_MOLDORM, 32, 32, 1, "1:0", "1:0"));
                Graphics.CTextures.addTexture(_HEAD_DRIGHT, new Graphics.CTextureAtlas(_NPC_MOLDORM, 32, 32, 1, "1:1", "1:1"));

                Graphics.CTextures.addTexture(_TAIL_UP, new Graphics.CTextureAtlas(_NPC_MOLDORM, 32, 32, 1, "2:0", "2:0"));
                Graphics.CTextures.addTexture(_TAIL_RIGHT, new Graphics.CTextureAtlas(_NPC_MOLDORM, 32, 32, 1, "2:1", "2:1"));

                Graphics.CTextures.addTexture(_TAIL_URIGHT, new Graphics.CTextureAtlas(_NPC_MOLDORM, 32, 32, 1, "2:2", "2:2"));
                Graphics.CTextures.addTexture(_TAIL_DRIGHT, new Graphics.CTextureAtlas(_NPC_MOLDORM, 32, 32, 1, "0:3", "0:3"));

                Graphics.CTextures.addTexture(_BODY, new Graphics.CTextureAtlas(_NPC_MOLDORM, 32, 32, 1, "0:2", "0:2"));
            }

            _imageIndex.Add(_MAP_ICON, new Graphics.CSprite(_HEAD_UP));
            _imageIndex.Add(_HEAD_UP, new Graphics.CSprite(_HEAD_UP));
            _imageIndex.Add(_HEAD_DOWN, new Graphics.CSprite(_HEAD_UP,false,true));
            _imageIndex.Add(_HEAD_LEFT, new Graphics.CSprite(_HEAD_RIGHT, true,false));
            _imageIndex.Add(_HEAD_RIGHT, new Graphics.CSprite(_HEAD_RIGHT));

            _imageIndex.Add(_HEAD_DLEFT, new Graphics.CSprite(_HEAD_DRIGHT,true,false));
            _imageIndex.Add(_HEAD_DRIGHT, new Graphics.CSprite(_HEAD_DRIGHT));
            _imageIndex.Add(_HEAD_ULEFT, new Graphics.CSprite(_HEAD_URIGHT,true,false));
            _imageIndex.Add(_HEAD_URIGHT, new Graphics.CSprite(_HEAD_URIGHT));

            _moldormAndTailCount++;
            _changeDirection();
        }


        protected void _changeDirection()
        {
            _moveTowardsPoint = getRandomPointInSightRange();
            lookAtExt(_moveTowardsPoint);
            _changeSpriteDirection();
            startTimer0(60);
        }

        private void _changeSpriteDirection()
        {
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(_HEAD_DOWN);
                    break;

                case DIRECTION.LEFT:
                    swapImage(_HEAD_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(_HEAD_RIGHT);
                    break;

                case DIRECTION.UP:
                    swapImage(_HEAD_UP);
                    break;

                case DIRECTION.DLEFT:
                    swapImage(_HEAD_DLEFT);
                    break;

                case DIRECTION.DRIGHT:
                    swapImage(_HEAD_DRIGHT);
                    break;

                case DIRECTION.URIGHT:
                    swapImage(_HEAD_URIGHT);
                    break;

                case DIRECTION.ULEFT:
                    swapImage(_HEAD_ULEFT);
                    break;
            }
        }

        public override void update(GameTime gameTime)
        {
            moveToPoint(_moveTowardsPoint.X, _moveTowardsPoint.Y, 1.0f);
            base.update(gameTime);

        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            _changeDirection();
        }
    }
}
