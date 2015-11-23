using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Collision
{
    class CCameraLimit : CActor
    {

        public CCameraLimit() : base()
        {
            _position = new Vector2(0, 0);
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            _hitBox = new CHitBox(this, 0, 0, Convert.ToInt32(additional[0]), Convert.ToInt32(additional[1]));
        }

        public float width
        {
            get
            {
                return _hitBox.size.X;
            }
        }

        public float height
        {
            get
            {
                return _hitBox.size.Y;
            }
        }

        public bool isPointWithin(Vector2 point)
        {
            return _hitBox.checkCollision(point);
        }

        public override void drawMe(bool useOverlay = false, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch = null)
        {
            if (spriteBatch != null)
                spriteBatch.Draw(Graphics.CTextures.rawTextures["debug:redBox"], new Rectangle((int)(position.X), (int)(position.Y),
                                            (int)width, (int)height), null, Color.LightSkyBlue);
            else
                base.drawMe(useOverlay, spriteBatch);
        }


    }
}
