using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.OvergrownKeese
{
    class COvergrownKeese : CBaseEnemy
    {
        private static int _overgrownKeeseCount = 0;
        private static string _SPRITE_NAMESPACE = "npc:overgrownKeese";
        private static string _IDLE = _SPRITE_NAMESPACE + ":idle";
        private static string _IDLE_STARE = _SPRITE_NAMESPACE + ":idleStare";
        private static string _FLY = _SPRITE_NAMESPACE + ":fly";
        private static string _KNOCKED_DOWN = _SPRITE_NAMESPACE + ":knockedDown";
        private static string _SWOOP = _SPRITE_NAMESPACE + ":swoop";

        public COvergrownKeese() :
            base()
        {
            if (_overgrownKeeseCount <= 0)
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/overgrownKeese");

                Graphics.CTextures.addTexture(_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "1:2", "1:2", 0));
                Graphics.CTextures.addTexture(_IDLE_STARE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "0:2", "0:2", 0));
                Graphics.CTextures.addTexture(_FLY, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "0:0", "3:1", 4));
                Graphics.CTextures.addTexture(_KNOCKED_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "2:2", "2:2", 0));
                Graphics.CTextures.addTexture(_SWOOP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 52, 47, 1, "0:1", "0:1", 0));
            }

            _imageIndex.Add(_IDLE, new Graphics.CSprite(_IDLE));
            _imageIndex.Add(_IDLE_STARE, new Graphics.CSprite(_IDLE_STARE));
            _imageIndex.Add(_FLY, new Graphics.CSprite(_FLY));
            _imageIndex.Add(_KNOCKED_DOWN, new Graphics.CSprite(_KNOCKED_DOWN));
            _imageIndex.Add(_SWOOP, new Graphics.CSprite(_SWOOP));

            _overgrownKeeseCount++;
        }

        protected override void cleanUp()
        {
            Graphics.CTextures.cleanUp(_SPRITE_NAMESPACE);
            _overgrownKeeseCount = 0;
            base.cleanUp();
        }

        public override void destroy(object sender)
        {
            _overgrownKeeseCount--;

            if (_overgrownKeeseCount <= 0)
            {
                cleanUp();
                _overgrownKeeseCount = 0;
            }

            base.destroy(sender);
        }
    }
}
