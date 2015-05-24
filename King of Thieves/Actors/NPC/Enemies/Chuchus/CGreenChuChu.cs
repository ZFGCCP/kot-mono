using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Chuchus
{
    public class CGreenChuChu : CBaseChuChu
    {
        private static int _greenChuChuCount = 0;

        public CGreenChuChu(int sight, float fov, int foh, params dropRate[] drops)
            : base(sight, fov, foh, drops)
        {
            _position.X = _randNum.Next(0,200);
            _position.Y = _randNum.Next(0, 200);

            _WOBBLE += "G";
            _POPUP += "G";
            _IDLE += "G";
            _HOP += "G";
            _POPDOWN += "G";
            

            if (_greenChuChuCount <= 0)
            {
                Graphics.CTextures.addTexture(_WOBBLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:0", "15:0", 12));
                Graphics.CTextures.addTexture(_POPUP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "8:1", "10:1", 5));
                Graphics.CTextures.addTexture(_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:1", "7:1", 5));
                Graphics.CTextures.addTexture(_HOP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:2", "6:2", 7));
                Graphics.CTextures.addTexture(_POPDOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "7:2", "9:2", 5));
            }

            _greenChuChuCount += 1;

            _imageIndex.Add(_WOBBLE, new Graphics.CSprite(_WOBBLE));
            _imageIndex.Add(_POPUP, new Graphics.CSprite(_POPUP));
            _imageIndex.Add(_IDLE, new Graphics.CSprite(_IDLE));
            _imageIndex.Add(_HOP, new Graphics.CSprite(_HOP));
            _imageIndex.Add(_POPDOWN, new Graphics.CSprite(_POPDOWN));
        }

        public override void destroy(object sender)
        {
            _greenChuChuCount -= 1;
            _doNpcCountCheck(ref _greenChuChuCount);
            base.destroy(sender);
        }
    }
}
