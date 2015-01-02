using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Forms.Map.controls.MenuStrip
{
    class CMenuItem : CControl
    {
        private string _itemName = "";
        private List<CMenuItem> _children = new List<CMenuItem>();
        private CMenuItem _parent = null;
        private bool _expanded = false;
        private Vector2 _size = Vector2.Zero;

        public CMenuItem(string itemName, CMenuItem parent, List<CMenuItem> children)
        {
            _parent = parent;
            _children = children;
            _size.Y = 16;
            
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (_parent == null || _expanded)
            {
                drawRect(Color.Wheat, position, _size, spriteBatch);
                spriteBatch.DrawString(font, _itemName, position, Color.Black);
            }
        }
    }
}
