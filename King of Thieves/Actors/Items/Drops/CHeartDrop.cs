using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Items.Drops
{
    class CHeartDrop : CDroppable
    {
        private static readonly string _HEART = "heart";
        private static sbyte _heartCount = 0;

        public CHeartDrop() :
            base()
        {
            _capacity = 4;

            _imageIndex.Add(_HEART, new Graphics.CSprite(Graphics.CTextures.DROPS_HEART));
            swapImage(_HEART);
            _name = "heart" + _heartCount++;
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Actors.Player.CPlayer));
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);
            CMasterControl.healthController.modifyHp(_capacity);
            _heartCount--;
        }
    }
}
