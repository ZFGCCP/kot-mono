using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using King_of_Thieves.Map;

namespace King_of_Thieves.Actors.NPC.Enemies.MoldormTail
{
    class CMoldormTailTail : CMoldormTailPiece
    {
        public CMoldormTailTail() :
            base(true)
        {
            _imageIndex.Add(_TAIL_UP, new Graphics.CSprite(_TAIL_UP,false,true));
            _imageIndex.Add(_TAIL_DOWN, new Graphics.CSprite(_TAIL_UP));
            _imageIndex.Add(_TAIL_LEFT, new Graphics.CSprite(_TAIL_RIGHT));
            _imageIndex.Add(_TAIL_RIGHT, new Graphics.CSprite(_TAIL_RIGHT,true,false));

            _imageIndex.Add(_TAIL_DLEFT, new Graphics.CSprite(_TAIL_DRIGHT, true, false));
            _imageIndex.Add(_TAIL_DRIGHT, new Graphics.CSprite(_TAIL_DRIGHT));
            _imageIndex.Add(_TAIL_ULEFT, new Graphics.CSprite(_TAIL_URIGHT, true, false));
            _imageIndex.Add(_TAIL_URIGHT, new Graphics.CSprite(_TAIL_URIGHT));

            startTimer0(1);
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            _follow = (Vector2)Map.CMapManager.propertyGetterFromComponent(this.componentAddress, _prev, EActorProperties.OLD_POSITION);
            moveToPoint(_follow.X, _follow.Y, 1.0f);
            _directionChangeExt(_angle);
            startTimer0(15);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            moveToPoint(_follow.X, _follow.Y, 1.0f);
            _changeSpriteDirection();
            base.update(gameTime);
        }

        private void _changeSpriteDirection()
        {
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(_TAIL_DOWN);
                    break;

                case DIRECTION.LEFT:
                    swapImage(_TAIL_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(_TAIL_RIGHT);
                    break;

                case DIRECTION.UP:
                    swapImage(_TAIL_UP);
                    break;

                case DIRECTION.DLEFT:
                    swapImage(_TAIL_DLEFT);
                    break;

                case DIRECTION.DRIGHT:
                    swapImage(_TAIL_DRIGHT);
                    break;

                case DIRECTION.URIGHT:
                    swapImage(_TAIL_URIGHT);
                    break;

                case DIRECTION.ULEFT:
                    swapImage(_TAIL_ULEFT);
                    break;
            }
        }
    }
}
