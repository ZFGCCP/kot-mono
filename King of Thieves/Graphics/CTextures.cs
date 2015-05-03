using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace King_of_Thieves.Graphics
{
    static class CTextures
    {
        public readonly static Dictionary<string, CTextureAtlas> textures = new Dictionary<string, CTextureAtlas>();
        public readonly static Dictionary<string, Texture2D> rawTextures = new Dictionary<string, Texture2D>();
        private static ContentManager _content;
        private static RenderTarget2D _tileMapGen = null;
        private static SpriteBatch _tileBatch = null;

        //==================sprite constants======================
        //flesh these out over time.  It's better than constantly creating string instances everytime we want to swap a sprite state.
        //namespaces
        public const string EFFECTS = "effects:";
        public const string PLAYER = "Player:";
        public const string DROPS = "Drops:";
        public const string HUD = "HUD:";
        public const string ITEMS = "items:";
        public const string SHIELDS = ITEMS + "shields:";

        //actual constants
        public const string EFFECT_ENERGY_BALL_SMALL = EFFECTS + "energyBallSmall";
        public const string EFFECT_ENERGY_WAVE_SMALL = EFFECTS + "energyWaveSmall";
        public const string EFFECT_ENERGY_WAVE_SMALL_LEFT = EFFECT_ENERGY_WAVE_SMALL + "Left";
        public const string EFFECT_ICE_BALL_SMALL = EFFECTS + "iceBallSmall";
        public const string EFFECT_FIRE_BALL_SMALL = EFFECTS + "fireBallSmall";
        public const string EFFECT_FIRE_BALL_SMALL_LEFT = EFFECT_FIRE_BALL_SMALL + "Left";
        public const string EFFECT_ARROW = EFFECTS + "arrow";
        public const string EFFECT_ARROW_RIGHT = EFFECTS + "arrowRight";
        public const string EFFECT_BOMB = EFFECTS + "bomb";
        public const string EFFECT_BOMB_FAST_TICK = EFFECTS + "bombFastTick";
        public const string EFFECT_EXPLOSION = EFFECTS + "explosion";
        public const string EFFECT_BOOMERANG = EFFECTS + "boomerang";
        public const string EFFECT_FIRE_SMALL = EFFECTS + "fireSmall";

        public const string DROPS_HEART = DROPS + "heart";
        public const string DROPS_RUPEE_GREEN = DROPS + "rupeeGreen";
        public const string DROPS_RUPEE_BLUE = DROPS + "rupeeBlue";
        public const string DROPS_RUPEE_ORANGE = DROPS + "rupeeOrange";
        public const string DROPS_RUPEE_PURPLE = DROPS + "rupeePurple";

        public const string PLAYER_CHARGE_ARROW_LEFT = PLAYER + "chargeArrowLeft";
        public const string PLAYER_HOLD_ARROW_LEFT = PLAYER + "holdArrowLeft";
        public const string PLAYER_SHOOT_ARROW_LEFT = PLAYER + "shootArrowLeft";
        public const string PLAYER_CHARGE_ARROW_UP = PLAYER + "chargeArrowUp";
        public const string PLAYER_HOLD_ARROW_UP = PLAYER + "holdArrowUp";
        public const string PLAYER_SHOOT_ARROW_UP = PLAYER + "shootArrowUp";
        public const string PLAYER_CHARGE_ARROW_DOWN = PLAYER + "chargeArrowDown";
        public const string PLAYER_HOLD_ARROW_DOWN = PLAYER + "holdArrowDown";
        public const string PLAYER_SHIELD_ENGAGE_DOWN = PLAYER + "shieldEngageDown";
        public const string PLAYER_SHIELD_DISENGAGE_DOWN = PLAYER + "shieldDisengageDown";
        public const string PLAYER_SHIELD_IDLE_DOWN = PLAYER + "shieldIdleDown";
        public const string PLAYER_SHIELD_WALK_DOWN = PLAYER + "shieldWalkDown";
        public const string PLAYER_SHIELD_ENGAGE_LEFT = PLAYER + "shieldEngageLeft";
        public const string PLAYER_SHIELD_DISENGAGE_LEFT = PLAYER + "shieldDisengageLeft";
        public const string PLAYER_SHIELD_IDLE_LEFT = PLAYER + "shieldIdleLeft";
        public const string PLAYER_SHIELD_WALK_LEFT = PLAYER + "shieldWalkLeft";
        public const string PLAYER_SHIELD_ENGAGE_RIGHT = PLAYER + "shieldEngageRight";
        public const string PLAYER_SHIELD_DISENGAGE_RIGHT = PLAYER + "shieldDisengageRight";
        public const string PLAYER_SHIELD_IDLE_RIGHT = PLAYER + "shieldIdleRight";
        public const string PLAYER_SHIELD_WALK_RIGHT = PLAYER + "shieldWalkRight";
        public const string PLAYER_SHIELD_ENGAGE_UP = PLAYER + "shieldEngageUp";
        public const string PLAYER_SHIELD_DISENGAGE_UP = PLAYER + "shieldDisengageUp";
        public const string PLAYER_SHIELD_IDLE_UP = PLAYER + "shieldIdleUp";
        public const string PLAYER_SHIELD_WALK_UP = PLAYER + "shieldWalkUp";
        public const string PLAYER_SHOOT_ARROW_DOWN = PLAYER + "shootArrowDown";
        public const string PLAYER_HOLD_CANNON_DOWN = PLAYER + "holdCannonDown";
        public const string PLAYER_SHOOT_CANNON_DOWN = PLAYER + "shootCannonDown";
        public const string PLAYER_THROW_BOOMERANG_UP = PLAYER + "throwBoomerangUp";
        public const string PLAYER_THROW_BOOMERANG_DOWN = PLAYER + "throwBoomerangDown";
        public const string PLAYER_THROW_BOOMERANG_LEFT = PLAYER + "throwBoomerangLeft";
        public const string PLAYER_GOT_ITEM = PLAYER + "gotItem";

        public const string HUD_ARROWS = HUD + "arrows";
        public const string HUD_ARROWS_FIRE = HUD + "arrowsFire";
        public const string HUD_ARROWS_ICE = HUD + "arrowsIce";
        public const string HUD_BOMB_CANNON = HUD + "bombCannon";
        public const string HUD_RUPEES = HUD + "rupees";
        public const string HUD_ACTION = HUD + "action";
        public const string HUD_PAUSE_CURSOR = HUD + "pauseCursor";
        public const string HUD_MAGIC_METER = HUD + "magicMeter";
        public const string HUD_PICKPOCKET_ICON_PETTY = HUD + "pickpocketIconPetty";

        public const string HUD_ITEM_SCREEN = HUD + "itemScreen";
        public const string HUD_QUEST_SCREEN = HUD + "questScreen";

        public const string WOOD_SHIELD_ENGAGE_DOWN = SHIELDS + "woodEngageDown";

        public const string WOOD_SHIELD_DISENGAGE_DOWN = SHIELDS + "woodDisengageDown";

        public const string WOOD_SHIELD_DOWN = SHIELDS + "woodDown";
        public const string WOOD_SHIELD_LEFT = SHIELDS + "woodLeft";
        public const string WOOD_SHIELD_RIGHT = SHIELDS + "woodRight";
        public const string WOOD_SHIELD_UP = SHIELDS + "woodUp";

       
        //source images
        public const string SOURCE_PLAYER = "Player";
        public const string SOURCE_SHIELDS = "shields";
        public const string SOURCE_HUD = "hud";
        public const string SOURCE_MENU = "menu";
        public const string SOURCE_MAGIC_METER = "magicMeter";
        public const string SOURCE_HUD_BUTTONS = "hudButtons";
        

        public static void init(ContentManager content)
        {
            _content = content;

            //Core textures should go here.  Things that are ALWAYS used (or small enough to not hog up memory)
            //other textures should only be loaded when they're needed and should be removed from memory asap.

            _prepareTextures();

            _tileBatch = new SpriteBatch(CGraphics.GPU);

            textures.Add("Npc:Maple:IdleDown", new CTextureAtlas("maple", 32, 40, 1, "1:1", "1:1", 0));
            textures.Add("Npc:Maple:IdleLeft", new CTextureAtlas("maple", 32, 40, 1, "1:2", "1:2", 0));
            textures.Add("Npc:Maple:IdleUp", new CTextureAtlas("maple", 32, 40, 1, "1:0", "1:0", 0));
            textures.Add("Npc:Maple:WalkDown", new CTextureAtlas("maple", 32, 40, 1, "0:1", "2:1", 10));
            textures.Add("Npc:Maple:WalkLeft", new CTextureAtlas("maple", 32, 40, 1, "0:2", "2:2", 10));
            textures.Add("Npc:Maple:WalkUp", new CTextureAtlas("maple", 32, 40, 1, "0:0", "2:0", 10));
            textures.Add("Npc:Maple:FlyDown", new CTextureAtlas("maple", 32, 40, 1, "3:1", "7:1", 14));
            textures.Add("Npc:Maple:FlyLeft", new CTextureAtlas("maple", 32, 40, 1, "3:2", "7:2", 14));
            textures.Add("Npc:Maple:FlyUp", new CTextureAtlas("maple", 32, 40, 1, "3:0", "7:0", 14));

            textures.Add("test", new CTextureAtlas(_content.Load<Texture2D>("test"), "test", 19, 23, 0));
            textures.Add("mcDungeon2", new CTextureAtlas(_content.Load<Texture2D>("mcDungeon2"), 16, 16, 0, 1));
            textures.Add("menu", new CTextureAtlas(_content.Load<Texture2D>("menu"), 288, 192, 0,0));
            textures.Add("Player:WalkDown", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:1", "9:1", 15));
            textures.Add("Player:WalkLeft", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:0", "15:1", 15));
            textures.Add("Player:WalkUp", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:0", "21:1", 15));
            textures.Add("Player:IdleDown", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:0", "0:0", 0));
            textures.Add("Player:IdleUp", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "2:0", "2:0", 0));
            textures.Add("Player:IdleLeft", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "1:0", "1:0", 0));
            textures.Add("Player:SwingUp", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:2", "33:2", 55));
            textures.Add("Player:SwingDown", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:0", "33:0", 55));
            textures.Add("Player:SwingLeft", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:1", "32:1", 55));
            textures.Add("Player:RollDown", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "40:12", "47:12", 15));
            textures.Add("Player:RollUp", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "40:13", "47:13", 15));
            textures.Add("Player:RollLeft", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "40:14", "46:14", 15));
            textures.Add("Player:LiftDown", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:29", "4:29", 10));
            textures.Add("Player:LiftUp", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:31", "4:31", 10));
            textures.Add("Player:LiftLeft", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:30", "4:30", 10));
            textures.Add("Player:CarryDown", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:32", "9:32", 15));
            textures.Add("Player:CarryUp", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:34", "9:34", 15));
            textures.Add("Player:CarryLeft", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:33", "9:33", 15));
            textures.Add("Player:LiftIdleDown", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "4:29", "4:29", 0));
            textures.Add("Player:LiftIdleUp", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "4:31", "4:31", 0));
            textures.Add("Player:LiftIdleLeft", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "4:30", "4:30", 0));
            textures.Add("Player:ThrowDown", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "5:29", "8:29", 10));
            textures.Add("Player:ThrowUp", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "5:31", "8:31", 10));
            textures.Add("Player:ThrowLeft", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "5:30", "8:30", 10));
            textures.Add("Player:ShockDown", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "37:30", "38:30", 10));
            textures.Add("Player:ShockLeft", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "40:30", "41:30", 10));
            textures.Add("Player:ShockUp", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "37:31", "38:31", 10));
            textures.Add("Player:FreezeDown", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:4", "11:4", 0));
            textures.Add("Player:FreezeUp", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "13:4", "13:4", 0));
            textures.Add("Player:FreezeLeft", new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "12:4", "12:4", 0));
            textures.Add(PLAYER_CHARGE_ARROW_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:7", "25:7", 8));
            textures.Add(PLAYER_CHARGE_ARROW_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:6", "25:6", 8));
            textures.Add(PLAYER_CHARGE_ARROW_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:8", "25:8", 8));
            textures.Add(PLAYER_HOLD_ARROW_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "25:7", "25:7", 0));
            textures.Add(PLAYER_HOLD_ARROW_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "25:6", "25:6", 0));
            textures.Add(PLAYER_HOLD_ARROW_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "25:8", "25:8", 0));
            textures.Add(PLAYER_SHIELD_ENGAGE_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "18:34", "21:34", 15));
            textures.Add(PLAYER_SHIELD_DISENGAGE_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "18:34", "21:34", 15, false, false, false, true));
            textures.Add(PLAYER_SHIELD_WALK_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "22:34", "30:34", 10));
            textures.Add(PLAYER_SHIELD_IDLE_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "22:34", "22:34", 0));
            textures.Add(PLAYER_SHIELD_ENGAGE_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "18:35", "20:35", 15));
            textures.Add(PLAYER_SHIELD_DISENGAGE_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "18:35", "20:35", 15, false, false, false, true));
            textures.Add(PLAYER_SHIELD_WALK_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "22:35", "28:35", 10));
            textures.Add(PLAYER_SHIELD_IDLE_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "22:35", "22:35", 0));
            textures.Add(PLAYER_SHIELD_ENGAGE_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "29:34", "31:34", 15));
            textures.Add(PLAYER_SHIELD_DISENGAGE_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "29:34", "31:34", 15, false, false, false, true));
            textures.Add(PLAYER_SHIELD_WALK_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "32:34", "39:34", 10));
            textures.Add(PLAYER_SHIELD_IDLE_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "32:34", "32:34", 0));
            textures.Add(PLAYER_SHOOT_ARROW_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "26:7", "28:7", 8));
            textures.Add(PLAYER_SHOOT_ARROW_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "26:6", "28:6", 8));
            textures.Add(PLAYER_SHOOT_ARROW_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "26:8", "28:8", 8));
            textures.Add(PLAYER_HOLD_CANNON_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "14:42", "20:42", 8));
            textures.Add(PLAYER_SHOOT_CANNON_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:42", "13:42", 8));
            textures.Add(PLAYER_THROW_BOOMERANG_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:21", "22:21", 8));
            textures.Add(PLAYER_THROW_BOOMERANG_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:23", "22:23", 8));
            textures.Add(PLAYER_THROW_BOOMERANG_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:22", "22:22", 8));
            textures.Add(PLAYER_GOT_ITEM, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "21:2", "21:2", 0));

            textures.Add("GerudoSword:SwingDown", new CTextureAtlas("Swords", 64, 64, 1, "0:0", "7:0", 55));
            textures.Add("GerudoSword:SwingUp", new CTextureAtlas("Swords", 64, 64, 1, "0:1", "7:1", 55));
            textures.Add("GerudoSword:SwingRight", new CTextureAtlas("Swords", 64, 64, 1, "0:2", "7:2", 55));

            textures.Add(WOOD_SHIELD_DOWN, new CTextureAtlas(SOURCE_SHIELDS, 32, 32, 1, "2:0", "2:0", 0));
            textures.Add(WOOD_SHIELD_ENGAGE_DOWN, new CTextureAtlas(SOURCE_SHIELDS, 32, 32, 1, "0:0", "2:0", 15));
            textures.Add(WOOD_SHIELD_DISENGAGE_DOWN, new CTextureAtlas(SOURCE_SHIELDS, 32, 32, 1, "0:1", "2:1", 15));
            textures.Add(WOOD_SHIELD_LEFT, new CTextureAtlas(SOURCE_SHIELDS, 32, 32, 1, "0:2", "0:2", 0));
            textures.Add(WOOD_SHIELD_UP, new CTextureAtlas(SOURCE_SHIELDS, 32, 32, 1, "1:2", "1:2", 0));

            textures.Add("chuChu:Wobble", new CTextureAtlas("chuchuGreen", 32, 32, 1, "0:0", "15:0", 12));
            textures.Add("chuChu:Idle", new CTextureAtlas("chuchuGreen", 32, 32, 1, "0:1", "7:1", 5));
            textures.Add("chuChu:PopUp", new CTextureAtlas("chuchuGreen", 32, 32, 1, "8:1", "10:1", 5));
            textures.Add("chuChu:Hop", new CTextureAtlas("chuchuGreen", 32, 32, 1, "0:2", "6:2", 7));
            textures.Add("chuChu:PopDown", new CTextureAtlas("chuchuGreen", 32, 32, 1, "7:2", "9:2", 5));

            textures.Add("keese:Idle", new CTextureAtlas("keese", 32, 32, 1, "0:0", "0:0"));
            textures.Add("keese:Fly", new CTextureAtlas("keese", 32, 32, 1, "1:0", "5:0",15));
            textures.Add("keeseFire:Idle", new CTextureAtlas("keese", 32, 32, 1, "0:1", "0:1"));
            textures.Add("keeseFire:Fly", new CTextureAtlas("keese", 32, 32, 1, "1:1", "5:1", 15));
            textures.Add("keeseIce:Idle", new CTextureAtlas("keese", 32, 32, 1, "0:2", "0:2"));
            textures.Add("keeseIce:Fly", new CTextureAtlas("keese", 32, 32, 1, "1:2", "5:2", 15));
            textures.Add("keeseShadow:Idle", new CTextureAtlas("keese", 32, 32, 1, "0:3", "0:3"));
            textures.Add("keeseShadow:Fly", new CTextureAtlas("keese", 32, 32, 1, "1:3", "5:3", 15));
            textures.Add("keeseThunder:Idle", new CTextureAtlas("keese", 32, 32, 1, "0:4", "0:4"));
            textures.Add("keeseThunder:Fly", new CTextureAtlas("keese", 32, 32, 1, "1:4", "5:4", 15));

            textures.Add("effects:Fire", new CTextureAtlas("effects", 32, 32, 1, "0:0", "3:0", 15));
            textures.Add("effects:Ice", new CTextureAtlas("effects", 32, 32, 1, "0:1", "3:1", 8));
            textures.Add("effects:Shadow", new CTextureAtlas("effects", 32, 32, 1, "0:2", "3:2", 15));
            textures.Add("effects:Thunder", new CTextureAtlas("effects", 32, 32, 1, "0:3", "2:3", 10));

            //tilesets
            textures.Add("tileset:test", new CTextureAtlas("test", 16, 16, 0, "0:0", "15:15",0,false,false,true));
            textures.Add("tileset:ages", new CTextureAtlas("ages", 16, 16, 0, "0:0", "15:15", 0, false, false, true));
            textures.Add("tileset:indoors:house", new CTextureAtlas("tileset:indoors:house", 16, 16, 0, "0:0", "24:16", 0, false, false, true));
            textures.Add("tileset:smallLantern", new CTextureAtlas("tileset:items:smallItems", 16, 16, 1, "0:0", "8:0", 5, false, false, false));
            textures.Add("tileset:items:chests-small", new CTextureAtlas("tileset:items:chests-small", 16, 16, 1, "0:0", "1:2", 0, false, false, false));

            //editor icons
            textures.Add("editor:icons", new CTextureAtlas("icons", 16, 16, 0, "0:0", "0:0"));
            textures.Add("editor:icons:open", new CTextureAtlas("icons", 16, 16, 0, "1:0", "1:0"));
            textures.Add("editor:icons:save", new CTextureAtlas("icons", 16, 16, 0, "2:0", "2:0"));

            //items and shit
            textures.Add("items:decor:potSmall", new CTextureAtlas("potSmall", 48, 48, 1, "0:0", "0:0"));
            textures.Add("items:decor:potSmallBreak", new CTextureAtlas("potSmall", 48, 48, 1, "1:0", "8:0",10));

            //effects
            textures.Add("effects:explosion", new CTextureAtlas("effects:explosion", 64, 64, 0, "0:0", "10:0", 10));
            textures.Add("effects:smokePoof", new CTextureAtlas("effects:explosion", 64, 64, 0, "2:0", "10:0", 10));
            textures.Add(EFFECT_ENERGY_BALL_SMALL, new CTextureAtlas("effects:various", 32, 32, 0, "0:0", "1:0", 5));
            textures.Add(EFFECT_FIRE_SMALL, new CTextureAtlas("effects:various", 32, 32, 0, "0:4", "3:4", 5));

            //projectiles
            textures.Add(EFFECT_ENERGY_WAVE_SMALL, new CTextureAtlas("effects:various", 32, 32, 0, "2:0", "2:0", 0));
            textures.Add(EFFECT_ENERGY_WAVE_SMALL_LEFT, new CTextureAtlas("effects:various", 32, 32, 0, "3:0", "3:0", 0));
            textures.Add(EFFECT_ICE_BALL_SMALL, new CTextureAtlas("effects:various", 32, 32, 0, "4:0", "5:0", 1));
            textures.Add(EFFECT_FIRE_BALL_SMALL, new CTextureAtlas("effects:various", 32, 32, 0, "3:1", "5:1", 5));
            textures.Add(EFFECT_FIRE_BALL_SMALL_LEFT, new CTextureAtlas("effects:various", 32, 32, 0, "0:1", "2:1", 5));
            textures.Add(EFFECT_ARROW, new CTextureAtlas("effects:various", 32, 32, 0, "1:2", "1:2", 0));
            textures.Add(EFFECT_ARROW_RIGHT, new CTextureAtlas("effects:various", 32, 32, 0, "0:2", "0:2", 0));
            textures.Add(EFFECT_BOMB, new CTextureAtlas("effects:various", 32, 32, 0, "0:3", "1:3", 1));
            textures.Add(EFFECT_BOMB_FAST_TICK, new CTextureAtlas("effects:various", 32, 32, 0, "0:3", "1:3", 10));
            textures.Add(EFFECT_BOOMERANG, new CTextureAtlas("effects:various", 32, 32, 0, "2:2", "5:3", 15));

            //HUD
            textures.Add("HUD:text:textBox", new CTextureAtlas("hud", 303, 74, 0, "0:0", "0:0", 0));
            textures.Add("HUD:health0", new CTextureAtlas("health", 16, 16, 1, "0:0", "0:0", 0));
            textures.Add("HUD:health1", new CTextureAtlas("health", 16, 16, 1, "1:0", "1:0", 0));
            textures.Add("HUD:health2", new CTextureAtlas("health", 16, 16, 1, "2:0", "2:0", 0));
            textures.Add("HUD:health3", new CTextureAtlas("health", 16, 16, 1, "3:0", "3:0", 0));
            textures.Add("HUD:health4", new CTextureAtlas("health", 16, 16, 1, "4:0", "4:0", 0));
            textures.Add("HUD:health5", new CTextureAtlas("health", 16, 16, 1, "5:0", "5:0", 0));
            textures.Add("HUD:buttonLeft", new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 0, "0:0", "0:0", 0));
            textures.Add("HUD:buttonRight", new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 0, "1:0", "1:0", 0));
            textures.Add("HUD:buttonUp", new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 0, "2:0", "2:0", 0));
            textures.Add(HUD_ARROWS, new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 0, "0:1", "0:1", 0));
            textures.Add(HUD_ARROWS_FIRE, new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 0, "0:2", "0:2", 0));
            textures.Add(HUD_ARROWS_ICE, new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 0, "1:2", "1:2", 0));
            textures.Add(HUD_ACTION, new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 0, "3:0", "3:0", 0));
            textures.Add(HUD_BOMB_CANNON, new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 0, "1:1", "1:1", 0));
            textures.Add(HUD_RUPEES, new CTextureAtlas("health", 16, 16, 1, "0:1", "0:1", 0));
            textures.Add(HUD_ITEM_SCREEN, new CTextureAtlas(SOURCE_MENU, 320, 240, 0, "0:0", "0:0", 0));
            textures.Add(HUD_QUEST_SCREEN, new CTextureAtlas(SOURCE_MENU, 320, 240, 0, "1:0", "1:0", 0));
            textures.Add(HUD_PAUSE_CURSOR, new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 0, "3:1", "3:1", 0));
            textures.Add(HUD_MAGIC_METER, new CTextureAtlas(SOURCE_MAGIC_METER, 104, 6, 0, "0:0", "0:0", 0));
            textures.Add(HUD_PICKPOCKET_ICON_PETTY, new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 0, "3:2", "3:2", 0));

            //drops
            textures.Add(DROPS_HEART, new CTextureAtlas("drops:drops01", 16, 16, 1, "0:0", "0:0"));
            textures.Add(DROPS_RUPEE_GREEN, new CTextureAtlas("drops:drops01", 16, 16, 1, "0:1", "0:1"));
            textures.Add(DROPS_RUPEE_BLUE, new CTextureAtlas("drops:drops01", 16, 16, 1, "1:1", "1:1"));
            textures.Add(DROPS_RUPEE_ORANGE, new CTextureAtlas("drops:drops01", 16, 16, 1, "2:1", "2:1"));
            textures.Add(DROPS_RUPEE_PURPLE, new CTextureAtlas("drops:drops01", 16, 16, 1, "3:1", "3:1"));
        }

        public static void addRawTexture(string textureName, string fileNameNoExt)
        {
            rawTextures.Add(textureName, _content.Load<Texture2D>(fileNameNoExt));
        }

        public static void addTexture(string textureName, CTextureAtlas atlas)
        {
            textures.Add(textureName, atlas);
        }

        private static void _prepareTextures()
        {
            rawTextures.Add("Player", _content.Load<Texture2D>("Player"));
            rawTextures.Add("Swords", _content.Load<Texture2D>("Swords"));
            rawTextures.Add("chuchuGreen", _content.Load<Texture2D>("chuchuGreen"));
            rawTextures.Add("keese", _content.Load<Texture2D>("keese"));
            rawTextures.Add("test", _content.Load<Texture2D>("tiletest"));
            rawTextures.Add("ages", _content.Load<Texture2D>("AGES_TILESET_1F"));
            rawTextures.Add("icons", _content.Load<Texture2D>("icons"));
            rawTextures.Add("pointer", _content.Load<Texture2D>("pointer"));
            rawTextures.Add("effects", _content.Load<Texture2D>("effects"));
            rawTextures.Add("potSmall", _content.Load<Texture2D>("potSmall"));
            rawTextures.Add("maple", _content.Load<Texture2D>("maple"));

            rawTextures.Add(SOURCE_SHIELDS, _content.Load<Texture2D>("sprites/items/shields"));

            //drops
            rawTextures.Add("drops:drops01", _content.Load<Texture2D>("sprites/drops/drops01"));
            
            //tilesets
            rawTextures.Add("tileset:indoors:house",_content.Load<Texture2D>(@"tilesets/indoors/house"));
            rawTextures.Add("tileset:items:chests-small", _content.Load<Texture2D>(@"tilesets/items/chests-small"));
            rawTextures.Add("tileset:items:smallItems", _content.Load<Texture2D>(@"tilesets/items/smallItems"));

            //effects
            rawTextures.Add("effects:explosion", _content.Load<Texture2D>(@"effects/bomb-explosion"));
            rawTextures.Add("effects:various", _content.Load<Texture2D>(@"effects/various-effects"));
            

            //hud
            rawTextures.Add("hud", _content.Load<Texture2D>("hud/textbox2"));
            rawTextures.Add("health", _content.Load<Texture2D>("hud/hearts"));
            rawTextures.Add(SOURCE_HUD_BUTTONS, _content.Load<Texture2D>("hud/buttons"));
            rawTextures.Add(SOURCE_MENU, _content.Load<Texture2D>("hud/menu"));
            rawTextures.Add(SOURCE_MAGIC_METER, _content.Load<Texture2D>("hud/magicMeter"));
        }

        public static void cleanUp(string nameSpace = "")
        {
            if (nameSpace == "")
            {
                textures.Clear();
                return;
            }

            var resourcesToRemove = (from pair in textures
                                     where pair.Key.Contains(nameSpace)
                                     select pair.Key).ToArray();

            foreach (string key in resourcesToRemove)
                textures.Remove(key);

            if (_tileBatch != null)
            {
                _tileBatch.Dispose();
                _tileBatch = null;
            }
        }

        public static Texture2D generateLayerImage(Map.CLayer layerToRender, Map.CTile[] tileStrip)
        {
            _tileMapGen = new RenderTarget2D(CGraphics.GPU, layerToRender.width, layerToRender.height);

            CGraphics.GPU.SetRenderTarget(_tileMapGen);
            _tileBatch.Begin();

            foreach (Map.CTile tile in tileStrip)
                _tileBatch.Draw(textures[tile.tileSet].sourceImage, tile.tileCoords, textures[tile.tileSet].getTile((int)tile.atlasCoords.X, (int)tile.atlasCoords.Y), Color.White);

            _tileBatch.End();
            CGraphics.GPU.SetRenderTarget(null);

            return (_tileMapGen);
        }


    }
}
