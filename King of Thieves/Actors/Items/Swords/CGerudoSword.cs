using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Input;
using Gears.Cloud;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Actors.Items.Swords
{
    class CSword : CActor
    {
        private Actors.Collision.CHitBox[] _gerudoSwordBoxes = new Collision.CHitBox[4];
        private Vector2 _downOffset = new Vector2(15, 40);
        private Vector2 _leftOffset = new Vector2(12, 20);
        private Vector2 _rightOffset = new Vector2(35, 20);
        private Vector2 _upOffset = new Vector2(15, 17);

        public CSword() :
            base()
        {
            _gerudoSwordBoxes[0] = new Collision.CHitBox(this, 15, 17, 30, 15);
            _gerudoSwordBoxes[1] = new Collision.CHitBox(this, 20, 17, 15, 30);
        }

        public CSword(string swordName, Vector2 position) :
            base()
        {
            _position = position;
            _name = swordName;
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

        public void swingSword(DIRECTION direction, Vector2 position)
        {
            _position = position;
            switch (direction)
            {
                case DIRECTION.UP:
                    swapImage(Graphics.CTextures.GERUDO_SWORD_UP, false);
                    _hitBox = _gerudoSwordBoxes[0];
                    _hitBox.offset = _upOffset;
                    break;

                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.GERUDO_SWORD_DOWN, false);
                    _hitBox = _gerudoSwordBoxes[0];
                    _hitBox.offset = _downOffset;
                    break;

                case DIRECTION.LEFT:
                    swapImage(Graphics.CTextures.GERUDO_SWORD_LEFT, false);
                    _hitBox = _gerudoSwordBoxes[1];
                    _hitBox.offset = _leftOffset;
                    break;

                case DIRECTION.RIGHT:
                    swapImage(Graphics.CTextures.GERUDO_SWORD_RIGHT, false);
                    _hitBox = _gerudoSwordBoxes[1];
                    _hitBox.offset = _rightOffset;
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

        public override void shock()
        {
            component.root.shock();
        }
    }
}



