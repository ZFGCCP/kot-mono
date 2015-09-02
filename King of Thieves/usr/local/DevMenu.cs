using Gears.Navigation;
using King_of_Thieves.Actors;
using Gears.Cloud;
using King_of_Thieves.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.usr.local
{
    class DevMenu : MenuReadyGameState
    {
        private Gears.Navigation.Menu _menu;

        public DevMenu()
        {
			_menu = new Gears.Navigation.Menu();

            MenuElement titleMenuElement = new MenuElement();
            titleMenuElement.MenuText = "Transmission Debug";
            titleMenuElement.Selectable = false;
            titleMenuElement.Hidden = false;
            titleMenuElement.ActiveArea = new Rectangle(10, 30, 30, 30);
            titleMenuElement.ForegroundColor = new Color(225, 225, 225);
            titleMenuElement.ActiveForegroundColor = new Color(100, 100, 100);
            titleMenuElement.SpriteFont = @"Fonts\MenuFont";
			_menu.AddMenuElement(titleMenuElement);

            MenuElement playTestElement = new MenuElement();
            playTestElement.MenuText = "Play Test";
            playTestElement.Selectable = true;
            playTestElement.Hidden = false;
			playTestElement.ActiveArea = new Rectangle(10, 50, 600, 100);
            playTestElement.ForegroundColor = new Color(225, 225, 225);
            playTestElement.ActiveForegroundColor = new Color(255, 200, 200);
            playTestElement.SpriteFont = @"Fonts\MenuFont";
            playTestElement.SetThrowPushEvent(new System.Action(() =>
            {
                //Master.Push(new PlayableState());
                Master.Push(new splash.CTitleState());
            }));
			_menu.AddMenuElement(playTestElement);

			if (System.Environment.OSVersion.Platform != System.PlatformID.Unix) {
				MenuElement mapToolElement = new MenuElement ();
				mapToolElement.MenuText = "Map Editor";
				mapToolElement.Selectable = true;
				mapToolElement.Hidden = false;
				mapToolElement.ActiveArea = new Rectangle (10, 70, 600, 100);
				mapToolElement.ForegroundColor = new Color (225, 225, 225);
				mapToolElement.ActiveForegroundColor = new Color (255, 200, 200);
				mapToolElement.SpriteFont = @"Fonts\MenuFont";
				mapToolElement.SetThrowPushEvent (new System.Action (() => {
					Graphics.CGraphics.changeResolution (800, 600);
					CMasterControl.commNet.Clear ();
					Master.Push (new EditorMode ());

				}));
				_menu.AddMenuElement (mapToolElement);
			}
            //_menu.AddMenuElements(new MenuElement[] { titleMenuElement, playTestElement, mapToolElement });

            
            
            
            
            
            
        }

        public override void Update(GameTime gameTime)
        {
            //throw new System.NotImplementedException();
            Master.Push(new MenuState(_menu));
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //throw new System.NotImplementedException();
        }
    }
}
