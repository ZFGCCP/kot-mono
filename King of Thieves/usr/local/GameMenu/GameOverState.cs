using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Navigation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.usr.local.GameMenu
{
    class GameOverState : MenuReadyGameState
    {
        public GameOverState()
        {
            Menu _menu = new Gears.Navigation.Menu();

            MenuElement optionContinue = new MenuElement();
            optionContinue.MenuText = "Cotinue";
            optionContinue.Selectable = true;
            optionContinue.Hidden = false;
            optionContinue.ActiveArea = new Rectangle(10, 30, 30, 30);
            optionContinue.ForegroundColor = new Color(225, 225, 225);
            optionContinue.ActiveForegroundColor = new Color(100, 100, 100);
            optionContinue.SpriteFont = @"Fonts\sherwood";
            _menu.AddMenuElement(optionContinue);

            MenuElement optionSave = new MenuElement();
            optionSave.MenuText = "Save and Continue";
            optionSave.Selectable = true;
            optionSave.Hidden = false;
            optionSave.ActiveArea = new Rectangle(10, 30, 30, 30);
            optionSave.ForegroundColor = new Color(225, 225, 225);
            optionSave.ActiveForegroundColor = new Color(100, 100, 100);
            optionSave.SpriteFont = @"Fonts\sherwood";
            _menu.AddMenuElement(optionSave);

            MenuElement optionQuit = new MenuElement();
            optionQuit.MenuText = "Save and Quit";
            optionQuit.Selectable = true;
            optionQuit.Hidden = false;
            optionQuit.ActiveArea = new Rectangle(10, 30, 30, 30);
            optionQuit.ForegroundColor = new Color(225, 225, 225);
            optionQuit.ActiveForegroundColor = new Color(100, 100, 100);
            optionQuit.SpriteFont = @"Fonts\sherwood";
            _menu.AddMenuElement(optionSave);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
