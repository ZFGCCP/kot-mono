using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.HUD.counters
{
    class CArrowCounter : CBaseCounter
    {
        private const int _QUIVER_CAPACITY = 10;
        private const int _BIG_QUIVER_CAPACITY = 50;
        private const int _GIANT_QUIVER_CAPACITY = 99;

        public CArrowCounter()
            : base(_QUIVER_CAPACITY, 0)
        {
            _fixedPosition.X = 35;
            _fixedPosition.Y = 210;

            _textOffset.X = 15;
            _textOffset.Y = 1;

            _imageIndex.Add(_ICON, new Graphics.CSprite(Graphics.CTextures.HUD_ARROW_COUNTER));
            swapImage(_ICON);
        }
  
    }
}
