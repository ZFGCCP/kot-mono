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
        protected readonly static string _HEAD_UP = "headUp";
        protected readonly static string _HEAD_DOWN = "headDown";
        protected readonly static string _HEAD_LEFT = "headLeft";
        protected readonly static string _HEAD_RIGHT = "headRight";

        protected readonly static string _HEAD_DLEFT = "headDLeft";
        protected readonly static string _HEAD_DRIGHT = "headDRight";
        protected readonly static string _HEAD_ULEFT = "headULeft";
        protected readonly static string _HEAD_URIGHT = "headURight";

        public CMoldormTailHead()
            : base(true)
        {
            if (_moldormAndTailCount <= 0)
            {
                Graphics.CTextures.addTexture(_HEAD_UP, new Graphics.CTextureAtlas(_NPC_MOLDORM, 32, 32, 0, "0:0", "0:0"));
                Graphics.CTextures.addTexture(_HEAD_URIGHT, new Graphics.CTextureAtlas(_NPC_MOLDORM, 32, 32, 0, "1:0", "1:0"));
            }

            _imageIndex.Add(_HEAD_UP, new Graphics.CSprite(_HEAD_UP));
            _imageIndex.Add(_HEAD_DOWN, new Graphics.CSprite(_HEAD_UP,false,false,null,false,180));
            _imageIndex.Add(_HEAD_LEFT, new Graphics.CSprite(_HEAD_UP, false, false, null, false, 90));
            _imageIndex.Add(_HEAD_RIGHT, new Graphics.CSprite(_HEAD_UP, false, false, null, false, 270));

            _imageIndex.Add(_HEAD_DLEFT, new Graphics.CSprite(_HEAD_URIGHT));
            _imageIndex.Add(_HEAD_DRIGHT, new Graphics.CSprite(_HEAD_URIGHT, false, false, null, false, 180));
            _imageIndex.Add(_HEAD_ULEFT, new Graphics.CSprite(_HEAD_URIGHT, false, false, null, false, 90));
            _imageIndex.Add(_HEAD_URIGHT, new Graphics.CSprite(_HEAD_URIGHT, false, false, null, false, 270));

            _moldormAndTailCount++;
            _name = "head";
            _changeDirection();
        }


        protected virtual void _changeDirection()
        {
            int nextChangeTime = _getChangeTime(_randNum.Next(0, 6));
            _moveTowardsPoint = getRandomPointInSightRange();
            lookAt(_moveTowardsPoint);
            _changeSpriteDirection();
            startTimer0(30);
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
            }
        }

        public override void update(GameTime gameTime)
        {
            moveToPoint(_moveTowardsPoint.X, _moveTowardsPoint.Y, 3.0f);
            base.update(gameTime);

        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            _changeDirection();
        }
    }
}
