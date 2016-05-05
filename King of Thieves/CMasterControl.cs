using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Gears.Navigation;
using King_of_Thieves.usr.local.GameMenu;
using Gears.Cloud;

namespace King_of_Thieves
{
    static class CMasterControl
    {
        public static Actors.HUD.health.CHealthController healthController;
        public static Actors.HUD.buttons.CButtonController buttonController;
        public static Actors.HUD.magic.CMagicMeter magicMeter;
        public static Actors.HUD.other.CPickPocketMeter pickPocketMeter = null;
        public static Sound.CAudioPlayer audioPlayer;
        public static Map.CMapManager mapManager = new Map.CMapManager();
        public static GameTime gameTime;
        public static Dictionary<int, List<Actors.CActorPacket>> commNet = new Dictionary<int, List<Actors.CActorPacket>>();
        public static ContentManager glblContent;
        public static Input.CInput glblInput;
        public static Graphics.CCamera camera = new Graphics.CCamera();
        public static bool hasFlippers = false;

        //menu creation functions
        public static Menu itemPauseMenu()
        {
            Menu menu = new Menu();

            CPauseMenuElement bow = new CPauseMenuElement(new Vector2(21, 35), 1, 3, 8, 4);
            bow.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_ARROWS);
            bow.MenuText = "Hero's Bow";
            bow.Selectable = true;
            bow.hudOptions = Actors.HUD.buttons.HUDOPTIONS.ARROWS;
            menu.AddMenuElement(bow);

            CPauseMenuElement bomb = new CPauseMenuElement(new Vector2(50, 35), 2, 0, 9, 5);
            bomb.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_BOMB_CANNON);
            bomb.MenuText = "Bomb Cannon";
            bomb.Selectable = true;
            bomb.hudOptions = Actors.HUD.buttons.HUDOPTIONS.BOMB_CANNON;
            menu.AddMenuElement(bomb);

            CPauseMenuElement boomerang = new CPauseMenuElement(new Vector2(79, 35), 3, 1, 10, 6);
            boomerang.Selectable = true;
            boomerang.MenuText = "Boomerang";
            boomerang.hudOptions = Actors.HUD.buttons.HUDOPTIONS.BOOMERANG;
            menu.AddMenuElement(boomerang);

            CPauseMenuElement dekuNut = new CPauseMenuElement(new Vector2(109, 35), 0, 2, 11, 7);
            dekuNut.Selectable = true;
            dekuNut.MenuText = "Deku Nut";
            menu.AddMenuElement(dekuNut);

            CPauseMenuElement hookshot = new CPauseMenuElement(new Vector2(21, 64), 5, 7, 0, 8);
            hookshot.Selectable = true;
            hookshot.MenuText = "Hookshot";
            menu.AddMenuElement(hookshot);

            CPauseMenuElement rocsCape = new CPauseMenuElement(new Vector2(50, 64), 6, 4, 1, 9);
            rocsCape.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_ROCS_CAPE);
            rocsCape.Selectable = true;
            rocsCape.MenuText = "Roc's Cape";
            rocsCape.hudOptions = Actors.HUD.buttons.HUDOPTIONS.ROCS_CAPE;
            menu.AddMenuElement(rocsCape);

            CPauseMenuElement shadowMedallion = new CPauseMenuElement(new Vector2(79, 64), 7, 5, 2, 10);
            shadowMedallion.Selectable = true;
            shadowMedallion.MenuText = "Shadow Medallion";
            shadowMedallion.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_SHADOW_MEDALLION);
            shadowMedallion.hudOptions = Actors.HUD.buttons.HUDOPTIONS.SHADOW_MEDALLION;
            menu.AddMenuElement(shadowMedallion);

            CPauseMenuElement magnetGloves = new CPauseMenuElement(new Vector2(109, 64), 4, 6, 3, 11);
            magnetGloves.Selectable = true;
            magnetGloves.MenuText = "Magnet Gloves";
            menu.AddMenuElement(magnetGloves);

            //***************************************
            //Bottles
            //***************************************

            CPauseMenuElement bottle1 = new CPauseMenuElement(new Vector2(21, 115), 9, 11, 4, 0);
            bottle1.MenuText = "Empty Bottle";
            bottle1.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_EMPTY_BOTTLE);
            bottle1.Selectable = true;
            bottle1.hudOptions = Actors.HUD.buttons.HUDOPTIONS.EMPTY_BOTTLE;
            menu.AddMenuElement(bottle1);

            CPauseMenuElement bottle2 = new CPauseMenuElement(new Vector2(50, 115), 10, 8, 5, 1);
            bottle2.MenuText = "Empty Bottle";
            bottle2.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_EMPTY_BOTTLE);
            bottle2.Selectable = true;
            bottle2.hudOptions = Actors.HUD.buttons.HUDOPTIONS.EMPTY_BOTTLE;
            menu.AddMenuElement(bottle2);

            CPauseMenuElement bottle3 = new CPauseMenuElement(new Vector2(79, 115), 11, 9, 6, 2);
            bottle3.MenuText = "Empty Bottle";
            bottle3.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_EMPTY_BOTTLE);
            bottle3.Selectable = true;
            bottle3.hudOptions = Actors.HUD.buttons.HUDOPTIONS.EMPTY_BOTTLE;
            menu.AddMenuElement(bottle3);

            CPauseMenuElement bottle4 = new CPauseMenuElement(new Vector2(109, 115), 8, 10, 7, 3);
            bottle4.MenuText = "Empty Bottle";
            bottle4.sprite = new Graphics.CSprite(Graphics.CTextures.HUD_EMPTY_BOTTLE);
            bottle4.Selectable = true;
            bottle4.hudOptions = Actors.HUD.buttons.HUDOPTIONS.EMPTY_BOTTLE;
            menu.AddMenuElement(bottle4);

            menu.Recache();

            CMasterControl.buttonController.bottleRef[0] = bottle1;
            CMasterControl.buttonController.bottleRef[1] = bottle2;
            CMasterControl.buttonController.bottleRef[2] = bottle3;
            CMasterControl.buttonController.bottleRef[3] = bottle4;

            CMasterControl.buttonController.changeBottleContents(1, Actors.HUD.buttons.HUDOPTIONS.RED_POTION);
            CMasterControl.buttonController.changeBottleContents(2, Actors.HUD.buttons.HUDOPTIONS.GREEN_POTION);
            CMasterControl.buttonController.changeBottleContents(3, Actors.HUD.buttons.HUDOPTIONS.BLUE_POTION);

            return menu;
        }

        //menu creation functions
        public static Menu questPauseMenu()
        {
            Menu menu = new Menu();
            
            CPauseMenuElement sword = new CPauseMenuElement(new Vector2(25, 125), 1, 3, 4, 4);
            sword.Selectable = true;
            sword.MenuText = "Master Sword";
            menu.AddMenuElement(sword);

            CPauseMenuElement shield = new CPauseMenuElement(new Vector2(105, 125), 2, 0, 6, 6);
            shield.Selectable = true;
            shield.MenuText = "Mirror Shield";
            menu.AddMenuElement(shield);

            CPauseMenuElement bracers = new CPauseMenuElement(new Vector2(189, 125), 3, 1, 7, 7);
            bracers.Selectable = true;
            bracers.MenuText = "Thief Bracers";
            menu.AddMenuElement(bracers);

            CPauseMenuElement flippers = new CPauseMenuElement(new Vector2(244, 125), 0, 2, 8, 8);
            flippers.Selectable = true;
            flippers.MenuText = "Zora's Flippers";
            menu.AddMenuElement(flippers);

            CPauseMenuElement seedSatchel = new CPauseMenuElement(new Vector2(25, 165), 5, 8, 0, 0);
            seedSatchel.Selectable = true;
            seedSatchel.MenuText = "Seed Satchel";
            menu.AddMenuElement(seedSatchel);

            CPauseMenuElement quiver = new CPauseMenuElement(new Vector2(65, 165), 6, 4, 0, 0);
            quiver.Selectable = true;
            quiver.MenuText = "Quiver";
            menu.AddMenuElement(quiver);

            CPauseMenuElement bombBag = new CPauseMenuElement(new Vector2(105, 165), 7, 5, 1, 1);
            bombBag.Selectable = true;
            bombBag.MenuText = "Bomb Bag";
            menu.AddMenuElement(bombBag);

            CPauseMenuElement seashells = new CPauseMenuElement(new Vector2(189, 165), 8, 6, 2, 2);
            seashells.Selectable = true;
            seashells.MenuText = "Secret Seashells";
            menu.AddMenuElement(seashells);

            CPauseMenuElement wallet = new CPauseMenuElement(new Vector2(244, 165), 4, 7, 3, 3);
            wallet.Selectable = true;
            wallet.MenuText = "Wallet";
            menu.AddMenuElement(wallet);

            menu.Recache();
            return menu;
        }
        
        public static Menu gameOverMenu()
        {
            Menu menu = new Menu();

            /*MenuElement titleMenuElement = new MenuElement();
            titleMenuElement.MenuText = "Game Over";
            titleMenuElement.Selectable = false;
            titleMenuElement.Hidden = false;
            titleMenuElement.ActiveArea = new Rectangle(90, 40, 30, 30);
            titleMenuElement.ForegroundColor = new Color(225, 225, 225);
            titleMenuElement.ActiveForegroundColor = new Color(100, 100, 100);
            titleMenuElement.SpriteFont = @"Fonts\sherwood";
            menu.AddMenuElement(titleMenuElement);*/

            MenuElement saveAndCont = new MenuElement();
            saveAndCont.MenuText = "Continue";
            saveAndCont.Selectable = true;
            saveAndCont.Hidden = false;
            saveAndCont.ActiveArea = new Rectangle(90, 70, 600, 100);
            saveAndCont.ForegroundColor = new Color(225, 225, 225);
            saveAndCont.ActiveForegroundColor = new Color(255, 200, 200);
            saveAndCont.SpriteFont = @"Fonts\sherwood";
            saveAndCont.SetThrowPushEvent(new Action(() =>
            {
                Master.Pop();
                CMasterControl.mapManager.cacheMaps(false, "castleTown.xml");
                CMasterControl.mapManager.swapMap("castleTown.xml", "player", new Vector2(400, 1024), Map.CMapManager.TRANSITION_RUMPLE_SWIRL);
            }));
            menu.AddMenuElement(saveAndCont);

            MenuElement saveAndQuit = new MenuElement();
            saveAndQuit.MenuText = "Quit";
            saveAndQuit.Selectable = true;
            saveAndQuit.Hidden = false;
            saveAndQuit.ActiveArea = new Rectangle(90, 90, 600, 100);
            saveAndQuit.ForegroundColor = new Color(225, 225, 225);
            saveAndQuit.ActiveForegroundColor = new Color(255, 200, 200);
            saveAndQuit.SpriteFont = @"Fonts\sherwood";
            saveAndQuit.SetThrowPushEvent(new Action(() =>
            {
                Master.Push(new usr.local.splash.CTitleState());   
            }));
            menu.AddMenuElement(saveAndQuit);


            menu.Recache();
            return menu;
        }
    }
}
