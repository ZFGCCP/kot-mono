using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Navigation;
using King_of_Thieves.Actors;
using Gears.Cloud;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.usr.local.GameMenu
{
    class CPauseMenu : MenuReadyGameState
    {
        private Actors.Menu.CPauseBackdrop[] _pauseBackdrops = new Actors.Menu.CPauseBackdrop[2];
        private Texture2D _backDrop = null;

        public CPauseMenu() :
            base()
        {
            _pauseBackdrops[0] = new Actors.Menu.CPauseBackdrop(Graphics.CTextures.HUD_ITEM_SCREEN, 0);
            _pauseBackdrops[1] = new Actors.Menu.CPauseBackdrop(Graphics.CTextures.HUD_QUEST_SCREEN, -1);
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["menu:openMenu"]);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Input.CInput input = Master.GetInputManager().GetCurrentInputHandler() as Input.CInput;
            if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["menu:closeMenu"]);
                Master.Pop();
            }

            for (int i = 0; i < 2; i++)
                _pauseBackdrops[i].update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 2; i++)
                _pauseBackdrops[i].drawMe();
        }
        
    }
}
