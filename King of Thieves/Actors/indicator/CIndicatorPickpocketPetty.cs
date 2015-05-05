using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.indicator
{
    class CIndicatorPickpocketPetty : CActor
    {
        public CIndicatorPickpocketPetty() :
            base()
        {
            _imageIndex.Add(Graphics.CTextures.HUD_PICKPOCKET_ICON_PETTY, new Graphics.CSprite(Graphics.CTextures.HUD_PICKPOCKET_ICON_PETTY));

            swapImage(Graphics.CTextures.HUD_PICKPOCKET_ICON_PETTY);
            _drawDepth = 10;
        }
    }
}
