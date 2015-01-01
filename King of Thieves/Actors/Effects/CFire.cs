using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Effects
{
    class CFire : CActor
    {
        public CFire()
        {
            swapImage("fire");
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("fire", new Graphics.CSprite("effects:Fire", Graphics.CTextures.textures["effects:Fire"]));
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _followRoot = true;
        }

        protected override void applyEffects()
        {
            
        }

        public override void drawMe(bool useOverlay = false)
        {
            base.drawMe();
        }

        public override void destroy(object sender)
        {
        }

    }
}
