using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Rope
{
    class CRope : CBaseRope
    {

        public CRope()
            : base(60, new dropRate(new Items.Drops.CHeartDrop(), 100))
        {
            _type = ROPETYPE.NORMAL;
        }

    }
}
