using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.HUD.counters
{
    class CRupeeCounter : CBaseCounter
    {
        private const int _RUPEE_WALLET_CAPACITY = 99;
        private const int _RUPEE_ADULT_CAPACITY = 500;
        private const int _RUPEE_GIANT_CAPACITY = 1000;

        public CRupeeCounter()
            : base(_RUPEE_WALLET_CAPACITY, 0)
        {
            _fixedPosition.X = 10;
            _fixedPosition.Y = 210;

            _textOffset.X = 15;
            _textOffset.Y = 1;

            _imageIndex.Add(_ICON, new Graphics.CSprite(Graphics.CTextures.HUD_RUPEES));
            swapImage(_ICON);
        }
    }
}
