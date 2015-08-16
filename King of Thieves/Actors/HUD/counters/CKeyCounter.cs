using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.HUD.counters
{
    class CKeyCounter : CBaseCounter
    {
        public CKeyCounter()
            : base(99, 0)
        {
            _fixedPosition.X = 10;
            _fixedPosition.Y = 188;

            _textOffset.X = 15;
            _textOffset.Y = 1;

            _imageIndex.Add(_ICON, new Graphics.CSprite(Graphics.CTextures.HUD_KEYS));
            swapImage(_ICON);
        }
    }
}
