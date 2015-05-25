using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Graphics;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    class CKeese : CBaseKeese
    {
        private static int _normalKeeseCount = 0;

        public CKeese()
            : base(60, new dropRate(new Items.Drops.CHeartDrop(), 100))
        {
            _type = KEESETYPE.NORMAL;
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            if (_normalKeeseCount <= 0)
            {
                CTextures.textures.Add(_IDLE, new CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:0", "0:0"));
                CTextures.textures.Add(_FLY, new CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "1:0", "5:0", 15));
            }

            _imageIndex.Add(_IDLE, new Graphics.CSprite(_IDLE));
            _imageIndex.Add(_FLY, new Graphics.CSprite(_FLY));
        }

        public override void destroy(object sender)
        {
            _normalKeeseCount -= 1;
            _doNpcCountCheck(ref _normalKeeseCount);
            base.destroy(sender);
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);

            if (collider is Player.CPlayer)
                if (!INVINCIBLE_STATES.Contains(collider.state))
                    collider.dealDamange(1, collider);
        }
    }
}
