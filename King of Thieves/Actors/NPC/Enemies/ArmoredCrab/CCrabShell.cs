using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.ArmoredCrab
{
    class CCrabShell : CBaseEnemy
    {
        private const string _IDLE = CArmoredCrab.SPRITE_NAMESPACE + ":shellIdle";

        public CCrabShell()
        {
            Graphics.CTextures.addTexture(_IDLE, new Graphics.CTextureAtlas(CArmoredCrab.SPRITE_NAMESPACE, 32, 32, 1, "5:0", "9:0", 5));
            _imageIndex.Add(_IDLE, new Graphics.CSprite(_IDLE));
            swapImage(_IDLE);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
        }
    }
}
