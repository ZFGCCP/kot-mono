using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Items.Drops
{
    class CRupeeDrop : CDroppable
    {
        private const string _RUPEE = "rupee";
        private string _color = "";

        private int _value = 0;

        public CRupeeDrop() :
            base(true)
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
                    _color = "Green";
                    break;

                case "B":
                    _value = 5;
                    _imageIndex.Add(_RUPEE, new Graphics.CSprite(Graphics.CTextures.DROPS_RUPEE_BLUE));
                    _color = "Blue";
                    break;

                case "O":
                    _value = 20;
                    _imageIndex.Add(_RUPEE, new Graphics.CSprite(Graphics.CTextures.DROPS_RUPEE_ORANGE));
                    _color = "Orange";
                    break;

                case "P":
                    _value = 50;
                    _imageIndex.Add(_RUPEE, new Graphics.CSprite(Graphics.CTextures.DROPS_RUPEE_PURPLE));
                    _color = "Purple";
                    break;

                default:
                    break;
            }

            _yieldMessage = "You got a " + _color + " rupee!  That's worth " + _value + " rupees!";

            if (additional.Length == 2 && additional[1] == "true")
                _state = ACTOR_STATES.INVISIBLE;

            swapImage(_RUPEE);
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);
            _yieldToPlayer();
        }

        protected override void _yieldToPlayer(bool fromChest = false)
        {
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["HUD:rupees:rupeeGet"]);
            CMasterControl.buttonController.incrementRupees(_value);
            base._yieldToPlayer(fromChest);
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Actors.Player.CPlayer));
        }
    }
}
