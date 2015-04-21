using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Gears.Navigation;
using King_of_Thieves.usr.local.GameMenu;

namespace King_of_Thieves
{
    static class CMasterControl
    {
        public static Actors.HUD.health.CHealthController healthController;
        public static Actors.HUD.buttons.CButtonController buttonController;
        public static Sound.CAudioPlayer audioPlayer;
        public static Map.CMapManager mapManager = new Map.CMapManager();
        public static GameTime gameTime;
        public static Dictionary<int, List<Actors.CActorPacket>> commNet = new Dictionary<int, List<Actors.CActorPacket>>();
        public static ContentManager glblContent;
        public static Input.CInput glblInput;
        public static Graphics.CCamera camera = new Graphics.CCamera();

        //menu creation functions
        public static Menu itemPauseMenu()
        {
            Menu menu = new Menu();

            CPauseMenuElement bow = new CPauseMenuElement(new Vector2(35, 35), 1, 3, -1, 4);
            bow.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_ARROWS);
            bow.MenuText = "Hero's Bow";
            bow.Selectable = true;
            menu.AddMenuElement(bow);

            CPauseMenuElement bomb = new CPauseMenuElement(new Vector2(84, 35), 2, 0, -1, 5);
            bomb.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_BOMB_CANNON);
            bomb.MenuText = "Bomb Cannon";
            bomb.Selectable = true;
            menu.AddMenuElement(bomb);

            CPauseMenuElement boomerang = new CPauseMenuElement(new Vector2(137, 35), 3, 1, -1, 6);
            boomerang.Selectable = true;
            boomerang.MenuText = "Boomerang";
            menu.AddMenuElement(boomerang);

            CPauseMenuElement dekuNut = new CPauseMenuElement(new Vector2(188, 35), 0, 2, -1, 7);
            dekuNut.Selectable = true;
            dekuNut.MenuText = "Deku Nut";
            menu.AddMenuElement(dekuNut);

            CPauseMenuElement hookshot = new CPauseMenuElement(new Vector2(35, 77), 5, 7, 0, 8);
            hookshot.Selectable = true;
            hookshot.MenuText = "Hookshot";
            menu.AddMenuElement(hookshot);

            menu.Recache();
            return menu;
        }

        //menu creation functions
        public static Menu questPauseMenu()
        {
            return new Menu();
        }
        
    }
}
