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
    class CPauseMenu : MenuState
    {
        private Actors.Menu.CPauseBackdrop[] _pauseBackdrops = new Actors.Menu.CPauseBackdrop[2];
        private bool _justOpened = true;

        public CPauseMenu(Menu itemMenu, Menu questMenu) :
            base(itemMenu)
        {
            _pauseBackdrops[0] = new Actors.Menu.CPauseBackdrop(Graphics.CTextures.HUD_ITEM_SCREEN, 0, itemMenu,true);
            _pauseBackdrops[1] = new Actors.Menu.CPauseBackdrop(Graphics.CTextures.HUD_QUEST_SCREEN, -1, questMenu);
            CMasterControl.audioPlayer.stopAllSfx();
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["menu:openMenu"]);
            
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Input.CInput input = Master.GetInputManager().GetCurrentInputHandler() as Input.CInput;
            if (!_justOpened && input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["menu:closeMenu"]);
                Master.Pop();
            }

            for (int i = 0; i < 2; i++)
                _pauseBackdrops[i].update(gameTime);

            if (_justOpened)
                _justOpened = false;
        }

        protected override void KeyDown(ref Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, ref Microsoft.Xna.Framework.Input.KeyboardState oldKeyboardState)
        {
            if (!_justOpened &&currentKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["menu:closeMenu"]);
                Master.Pop();
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 2; i++)
                _pauseBackdrops[i].drawMe();
        }
        
    }
}
