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


        public CGoldFairyDust() :
            base()
        {
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
            pawnAlerter.init("pawnShopAlterter", _position - new Microsoft.Xna.Framework.Vector2(0, 30), "", CReservedAddresses.NON_ASSIGNED);
            pawnAlerter.layer = 0;
            Map.CMapManager.addComponent(pawnAlerter, new Dictionary<string, CActor>());
            _killMe = true;
        }
    }
}
