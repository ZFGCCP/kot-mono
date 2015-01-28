using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.MoldormTail
{
    class CMoldormTailTail : CMoldormTailPiece
    {
        public CMoldormTailTail() :
            base(true)
        {
            _imageIndex.Add(_TAIL, new Graphics.CSprite(_TAIL));
            swapImage(_TAIL);
            startTimer0(1);
        }
    }
}
