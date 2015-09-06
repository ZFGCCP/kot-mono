using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.HUD.counters
{
    class CBombCounter : CBaseCounter
    {
        private const int _BOM_BAG_CAPACITY = 10;
        private const int _BIG_BOMB_BAG_CAPACITY = 50;
        private const int _GIANT_BOMB_BAG_CAPACITY = 99;

        public CBombCounter()
            : base(_BOM_BAG_CAPACITY, 0)
        {
            _fixedPosition.X = 35;
            _fixedPosition.Y = 140;

            _textOffset.X = 15;
            _textOffset.Y = 1;

            _imageIndex.Add(_ICON, new Graphics.CSprite(Graphics.CTextures.HUD_BOMB_COUNTER));
            swapImage(_ICON);
        }
    }
}
