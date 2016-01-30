using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Navigation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.usr.local
{
    class GameOver : MenuState
    {
        public GameOver(Menu menu) :
            base(menu)
        {
            CMasterControl.mapManager.unloadAllMaps();
            CMasterControl.camera.jump(Vector3.Zero);
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["bgm:gameOver"],-1);
        }
    }
}
