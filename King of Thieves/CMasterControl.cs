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

            CPauseMenuElement bow = new CPauseMenuElement(new Vector2(35, 35), 1, 3, 8, 4);
            bow.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_ARROWS);
            bow.MenuText = "Hero's Bow";
            bow.Selectable = true;
            menu.AddMenuElement(bow);

            CPauseMenuElement bomb = new CPauseMenuElement(new Vector2(84, 35), 2, 0, 9, 5);
            bomb.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_BOMB_CANNON);
            bomb.MenuText = "Bomb Cannon";
            bomb.Selectable = true;
            menu.AddMenuElement(bomb);

            CPauseMenuElement boomerang = new CPauseMenuElement(new Vector2(137, 35), 3, 1, 10, 6);
            boomerang.Selectable = true;
            boomerang.MenuText = "Boomerang";
            menu.AddMenuElement(boomerang);

            CPauseMenuElement dekuNut = new CPauseMenuElement(new Vector2(188, 35), 0, 2, 11, 7);
            dekuNut.Selectable = true;
            dekuNut.MenuText = "Deku Nut";
            menu.AddMenuElement(dekuNut);

            CPauseMenuElement hookshot = new CPauseMenuElement(new Vector2(35, 77), 5, 7, 0, 8);
            hookshot.Selectable = true;
            hookshot.MenuText = "Hookshot";
            menu.AddMenuElement(hookshot);

            CPauseMenuElement rocsCape = new CPauseMenuElement(new Vector2(84, 77), 6, 4, 1, 9);
            rocsCape.Selectable = true;
            rocsCape.MenuText = "Roc's Cape";
            menu.AddMenuElement(rocsCape);

            CPauseMenuElement shadowCLoak = new CPauseMenuElement(new Vector2(137, 77), 7, 5, 2, 10);
            shadowCLoak.Selectable = true;
            rocsCape.MenuText = "Shadow Cloak";
            menu.AddMenuElement(shadowCLoak);

            CPauseMenuElement magnetGloves = new CPauseMenuElement(new Vector2(188, 77), 4, 6, 3, 11);
            magnetGloves.Selectable = true;
            magnetGloves.MenuText = "Magnet Gloves";
            menu.AddMenuElement(magnetGloves);

            //***************************************
            //Bottles
            //***************************************

            CPauseMenuElement bottle1 = new CPauseMenuElement(new Vector2(35, 155), 9, 11, 4, 0);
            bottle1.Selectable = true;
            bottle1.MenuText = "Empty Bottle";
            menu.AddMenuElement(bottle1);

            CPauseMenuElement bottle2 = new CPauseMenuElement(new Vector2(84, 155), 10, 8, 5, 1);
            bottle2.Selectable = true;
            bottle2.MenuText = "Empty Bottle";
            menu.AddMenuElement(bottle2);

            CPauseMenuElement bottle3 = new CPauseMenuElement(new Vector2(137, 155), 11, 9, 6, 2);
            bottle3.Selectable = true;
            bottle3.MenuText = "Empty Bottle";
            menu.AddMenuElement(bottle3);

            CPauseMenuElement bottle4 = new CPauseMenuElement(new Vector2(188, 155), 8, 10, 7, 3);
            bottle4.Selectable = true;
            bottle4.MenuText = "Empty Bottle";
            menu.AddMenuElement(bottle4);

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
