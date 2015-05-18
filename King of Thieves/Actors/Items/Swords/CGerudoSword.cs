using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Input;
using Gears.Cloud;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Items.Swords
{
    class CSword : CActor
    {
        private Actors.Collision.CHitBox[] _gerudoSwordBoxes = new Collision.CHitBox[4];

        public CSword() :
            base()
        {
            _gerudoSwordBoxes[0] = new Collision.CHitBox(this, 15, 17, 30, 15);
        }

        public CSword(string swordName, Vector2 position) :
            base()
        {
            _position = position;
            _name = swordName;
        }

        protected override void _registerUserEvents()
        {
            base._registerUserEvents();
            _userEvents.Add(0, userEventSwing);
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
            //use the gerudo sword for now
            _imageIndex.Add(Graphics.CTextures.GERUDO_SWORD_DOWN, new Graphics.CSprite(Graphics.CTextures.GERUDO_SWORD_DOWN));
            _imageIndex.Add(Graphics.CTextures.GERUDO_SWORD_RIGHT, new Graphics.CSprite(Graphics.CTextures.GERUDO_SWORD_RIGHT));
            _imageIndex.Add(Graphics.CTextures.GERUDO_SWORD_LEFT, new Graphics.CSprite(Graphics.CTextures.GERUDO_SWORD_RIGHT, true));
            _imageIndex.Add(Graphics.CTextures.GERUDO_SWORD_UP, new Graphics.CSprite(Graphics.CTextures.GERUDO_SWORD_UP));
        }

        public void userEventSwing(object sender)
        {
            _position = new Vector2((float)userParams[1], (float)userParams[2]);
            switch ((DIRECTION)userParams[0])
            {
                case DIRECTION.UP:
                    swapImage(Graphics.CTextures.GERUDO_SWORD_UP);
                    _hitBox = _gerudoSwordBoxes[0];
                    break;

                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.GERUDO_SWORD_DOWN);
                    break;

                case DIRECTION.LEFT:
                    swapImage(Graphics.CTextures.GERUDO_SWORD_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(Graphics.CTextures.GERUDO_SWORD_RIGHT);
                    break;

                default:
                    break;
            }
            
        }

        public override void animationEnd(object sender)
        {
            image = null;
            _hitBox = null;
        }
    }
}



