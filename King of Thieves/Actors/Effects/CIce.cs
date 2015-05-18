using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Effects
{
    class CIce : CActor
    {
        public CIce()
        {
            swapImage("ice");
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("ice", new Graphics.CSprite("effects:Ice"));
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _followRoot = true;
        }

        protected override void applyEffects()
        {
        }

        public override void destroy(object sender)
        {
        }

    }
}
