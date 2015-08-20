using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Items.Drops
{
    class CFireHydrant : CDroppable
    {

        public CFireHydrant() :
            base()
        {
            _capacity = 0;
            _yieldMessage = "You got a...! Fire hydrant..? You should bring it to the dog, you promised after all.";
            _name = "hydrant";

            _imageIndex.Add(Graphics.CTextures.DROPS_FIRE_HYDRANT, new Graphics.CSprite(Graphics.CTextures.DROPS_FIRE_HYDRANT));
            swapImage(Graphics.CTextures.DROPS_FIRE_HYDRANT);
            _state = ACTOR_STATES.INVISIBLE;
        }

        protected override void _yieldToPlayer(bool fromChest = false)
        {
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["HUD:rupees:rupeeGet"]);
            CMasterControl.buttonController.playerHasHydrant = true;
            base._yieldToPlayer(true);
        }
    }
}
