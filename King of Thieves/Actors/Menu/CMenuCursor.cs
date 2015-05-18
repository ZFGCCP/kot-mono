using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Actors.HUD;

namespace King_of_Thieves.Actors.Menu
{
    class CMenuCursor : CHUDElement
    {
        public int currentIndex;

        public CMenuCursor(Vector2 cursorPos) :
            base()
        {
            _imageIndex.Add(Graphics.CTextures.HUD_PAUSE_CURSOR, new Graphics.CSprite(Graphics.CTextures.HUD_PAUSE_CURSOR));
            swapImage(Graphics.CTextures.HUD_PAUSE_CURSOR);
            _fixedPosition = cursorPos;
            currentIndex = 0;
        }

        public Vector2 fixedPosition
        {
            set
            {
                _fixedPosition = value;
            }
        }

    }
}
