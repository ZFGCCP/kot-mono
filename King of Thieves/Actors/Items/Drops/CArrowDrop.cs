using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Items.Drops
{
    class CArrowDrop : CDroppable
    {
        public CArrowDrop() :
            base(false)
        {
            _followRoot = false;
            _hitBox = new Collision.CHitBox(this, 4, 3, 6, 10);
            _imageIndex.Add(Graphics.CTextures.DROPS_ARROW, new Graphics.CSprite(Graphics.CTextures.DROPS_ARROW));
            swapImage(Graphics.CTextures.DROPS_ARROW);
            _hitBox = new Collision.CHitBox(this, 0, 0, 16, 16);
        }

        public override void create(object sender)
        {

        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Actors.Player.CPlayer));
        }

        public override void collide(object sender, CActor collider)
        {
            if (_state != ACTOR_STATES.INVISIBLE)
            {
                base.collide(sender, collider);
                _yieldToPlayer();
            }
        }

        protected override void _yieldToPlayer(bool fromChest = false)
        {
            CMasterControl.buttonController.modifyArrows(1);
        }


    }
}
