using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Other
{
    //this isn't really an npc, but it has so little functionality that it might as well be
    class CGoldFairyDust : CBaseNpc
    {
        private string[] _gotDust = { "You found a bottle of gold fairy dust!", "You better get out quick before the shop keeper notices you." };
        private static int _goldDustCount = 0;

        private const string _SPRITE_NAMESPACE = "tileset:items:smallItems:";
        private const string _GOLD_DUST_BOTTLE = _SPRITE_NAMESPACE + "goldDustBottle";


        public CGoldFairyDust() :
            base()
        {
            Graphics.CTextures.addTexture(_GOLD_DUST_BOTTLE, new Graphics.CTextureAtlas("tileset:items:smallItems", 16, 16, 0, "0:1", "0:1", 0));

            _imageIndex.Add(_GOLD_DUST_BOTTLE, new Graphics.CSprite(_GOLD_DUST_BOTTLE));
            swapImage(_GOLD_DUST_BOTTLE);

            _hearingRadius = 20;
            _goldDustCount += 1;
        }

        protected override void cleanUp()
        {
            _goldDustCount--;
            base.cleanUp();
        }

        protected override void dialogBegin(object sender)
        {
            _currentDialog = _gotDust;

            base.dialogBegin(sender);
        }

        protected override void dialogEnd(object sender)
        {
            base.dialogEnd(sender);
            Collision.GameChangers.CPawnShopAlerter pawnAlerter = new Collision.GameChangers.CPawnShopAlerter();
            pawnAlerter.init("pawnShopAlterter", _position - new Microsoft.Xna.Framework.Vector2(70, -116), "", CReservedAddresses.NON_ASSIGNED);
            pawnAlerter.layer = 0;
            Map.CMapManager.addComponent(pawnAlerter, new Dictionary<string, CActor>());
            _killMe = true;
        }
    }
}
