using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Items.decoration
{
    class CDemoDoor : CDoor
    {

        public CDemoDoor() :
            base()
        {
            _imageIndex.Add(Graphics.CTextures.GREEN_DOOR, new Graphics.CSprite(Graphics.CTextures.GREEN_DOOR));
            swapImage(Graphics.CTextures.GREEN_DOOR);
        }

        protected override void _addCollidables() { }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            if (NPC.Enemies.Rope.CBaseRope.ropeCount <= 1)
                _killMe = true;
        }
    }
}
