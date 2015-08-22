using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Other.DemoGuys
{
    class CTempleMusic : CActor
    {
        public CTempleMusic() :
            base()
        {

        }

        public override void roomStart(object sender)
        {
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["bgm:temple"]);
        }
    }
}
