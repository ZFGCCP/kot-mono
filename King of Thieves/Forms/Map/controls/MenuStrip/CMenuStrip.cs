using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Forms.Map.controls.MenuStrip
{
    class CMenuStrip : CControl
    {
        private List<CMenuItem> _children = new List<CMenuItem>();

        public CMenuStrip(List<CMenuItem> children)
        {
            _children = children;
            position = Vector2.Zero;
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (CMenuItem menuItem in _children)
                menuItem.draw(spriteBatch);
        }
    }
}
