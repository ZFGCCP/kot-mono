using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using King_of_Thieves.Graphics;

namespace King_of_Thieves.Actors.Collision
{
    class CLayerChanger : CActor
    {
        private int _toLayer;
        private int _height;
        private int _width;

        public CLayerChanger()
            : base()
        {

        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _height = Convert.ToInt32(additional[1]);
            _width = Convert.ToInt32(additional[0]);
            _hitBox = new CHitBox(this, 0, 0, _width, _height);
            _toLayer = Convert.ToInt32(additional[2]);
            _imageIndex.Add(_MAP_ICON, null);
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CPlayer));
        }

        public override void collide(object sender, CActor collider)
        {
            Map.CMapManager.switchComponentLayer(collider.component, _toLayer);
        }

        public override void drawMe(bool useOverlay = false, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch = null)
        {
            if (spriteBatch != null)
                spriteBatch.Draw(Graphics.CTextures.rawTextures["debug:redBox"], new Rectangle((int)(position.X), (int)(position.Y),
                                            _width, _height), null, Color.White);
            else
                base.drawMe(useOverlay, spriteBatch);
        }


    }
}
