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
        public CMenuCursor() :
            base()
        {
            _imageIndex.Add(Graphics.CTextures.HUD_PAUSE_CURSOR, new Graphics.CSprite(Graphics.CTextures.HUD_PAUSE_CURSOR));
            swapImage(Graphics.CTextures.HUD_PAUSE_CURSOR);
            _fixedPosition = new Vector2(35, 35);
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
