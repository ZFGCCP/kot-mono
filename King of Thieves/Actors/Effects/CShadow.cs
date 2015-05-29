using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Effects
{
    class CShadow : CActor
    {
        public CShadow()
        {
            swapImage("shadow");
        }

        protected override void applyEffects()
        {
            
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("shadow", new Graphics.CSprite("effects:Shadow"));
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _followRoot = true;
        }

        public override void destroy(object sender)
        {
        }

    }
}
