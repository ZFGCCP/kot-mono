using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Collision
{
    class CMapChanger : CActor
    {
        private string _mapName;
        private Vector2 _playerPosition;

        public CMapChanger() :
            base()
        {
            _hitBox = new CHitBox(this, 0, 0, 16, 16);
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            double posX = Convert.ToDouble(additional[0]);
            double posY = Convert.ToDouble(additional[1]);
            _mapName = additional[2];

            _playerPosition = new Vector2((float)posX, (float)posY);

            base.init(name, position, dataType, compAddress, additional);
        }

        public override void collide(object sender, CActor collider)
        {
            CMasterControl.mapManager.swapMap(_mapName, "player",_playerPosition);
            
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Player.CPlayer));
        }

        public override void drawMe(bool useOverlay = false, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch = null)
        {
            if (spriteBatch != null)
                spriteBatch.Draw(Graphics.CTextures.rawTextures["debug:redBox"], new Rectangle((int)(position.X), (int)(position.Y),
                                            16, 16), null, Color.Gold);
            else
                base.drawMe(useOverlay, spriteBatch);
        }
    }
}
