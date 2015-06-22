using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Player
{
    class CShadowClone : CActor
    {
        public CShadowClone()
        {
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHADOWUP, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHADOWUP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHADOWDOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHADOWDOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHADOWLEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHADOWLEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHADOWRIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHADOWLEFT, true));

            _hitBox = new Collision.CHitBox(this, 10, 18, 12, 15);
            startTimer0(18000);
            _followRoot = false;
            _drawDepth = 7;
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            changeDirection((DIRECTION)Convert.ToInt32(additional[0]));
        }

        public override void timer0(object sender)
        {
            _killMe = true;
            ((CPlayer)component.root).cloneExists = false;
        }

        public void changeDirection(DIRECTION direction)
        {
            _direction = direction;
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.PLAYER_SHADOWDOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(Graphics.CTextures.PLAYER_SHADOWUP);
                    break;

                case DIRECTION.LEFT:
                    swapImage(Graphics.CTextures.PLAYER_SHADOWLEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(Graphics.CTextures.PLAYER_SHADOWRIGHT);
                    break;
            }
        }
    }
}
