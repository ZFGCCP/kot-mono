using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Items.Drops
{
    class CRupeeDrop : CDroppable
    {
        private const string _RUPEE = "rupee";

        private int _value = 0;

        public CRupeeDrop() :
            base()
        {
            _followRoot = false;
            _hitBox = new Collision.CHitBox(this, 4, 3, 6, 10);
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            switch (additional[0])
            {
                case "G":
                    _value = 1;
                    _imageIndex.Add(_RUPEE, new Graphics.CSprite(Graphics.CTextures.DROPS_RUPEE_GREEN));
                    break;

                case "B":
                    _value = 5;
                    _imageIndex.Add(_RUPEE, new Graphics.CSprite(Graphics.CTextures.DROPS_RUPEE_BLUE));
                    break;

                case "O":
                    _value = 20;
                    _imageIndex.Add(_RUPEE, new Graphics.CSprite(Graphics.CTextures.DROPS_RUPEE_ORANGE));
                    break;

                case "P":
                    _value = 50;
                    _imageIndex.Add(_RUPEE, new Graphics.CSprite(Graphics.CTextures.DROPS_RUPEE_PURPLE));
                    break;

                default:
                    break;
            }

            swapImage(_RUPEE);
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);
            _yieldToPlayer();
        }

        protected override void _yieldToPlayer()
        {
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["HUD:rupees:rupeeGet"]);
            CMasterControl.buttonController.incrementRupees(_value);
            base._yieldToPlayer();
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Actors.Player.CPlayer));
        }
    }
}
