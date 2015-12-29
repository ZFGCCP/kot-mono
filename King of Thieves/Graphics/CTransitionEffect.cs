using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    class CTransitionEffect : CSprite
    {
        private int _fadeInDuration = 0;
        private int _fadeOutDuration = 0;

        public CTransitionEffect(string atlasName, int fadeInDuration, int fadeOutDuration) :
            base(atlasName)
        {
            _fadeInDuration = fadeInDuration;
            _fadeOutDuration = fadeOutDuration;
        }

        public override bool draw(int x, int y, bool useOverlay = false, SpriteBatch spriteBatch = null)
        {
            _fadeInDuration -= 1;

            if (_fadeInDuration <= 0 &&_fadeOutDuration > 0)
                _fadeOutDuration -= 1;


            return base.draw(x, y, useOverlay, spriteBatch);
        }

        public bool fadeInComplete
        {
            get
            {
                return _fadeInDuration == 0;
            }
        }

        public bool fadeOutComplete
        {
            get
            {
                return _fadeOutDuration == 0;
            }
        }
    }
}
