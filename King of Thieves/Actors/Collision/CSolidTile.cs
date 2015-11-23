using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using King_of_Thieves.Graphics;

namespace King_of_Thieves.Actors.Collision
{
    class CSolidTile : CActor
    {
        private int _width;
        private int _height;
        public static int currentSelectedLayer = -1;

        public CSolidTile() : base()
        {
            _hitBox = new CHitBox(this, 0, 0, 16, 16);
            _followRoot = false;
            _width = 16; _height = 16;
        }

        public CSolidTile(int x, int y, int width, int height)
            : base()
        {
            _hitBox = new CHitBox(this, x, y, width, height);
            _width = width;
            _height = height;
            _followRoot = false;
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            if (additional != null)
            {
                _hitBox = new CHitBox(this, 0, 0, Convert.ToInt32(additional[0]), Convert.ToInt32(additional[1]));
                _width = (int)(_hitBox.halfWidth * 2f);
                _height = (int)(_hitBox.halfHeight * 2f);
            }

            _imageIndex.Add("debug:redBox", new Graphics.CSprite("debug:redBox"));
        }

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }

        public override void drawMe(bool useOverlay = false, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch = null)
        {
            if (spriteBatch != null && currentSelectedLayer == layer)
                spriteBatch.Draw(Graphics.CTextures.rawTextures["debug:redBox"], new Rectangle((int)(position.X), (int)(position.Y),
                                            _width, _height), null, Color.White);
            else
                base.drawMe(useOverlay, spriteBatch);
        }
    }
}
