﻿using System;
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
        public readonly static Dictionary<string, Map.CAnimatedTile> animatedTiles = new Dictionary<string, Map.CAnimatedTile>();
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
        public const string DOORS = ITEMS + "doors:";
        public const string SHIELDS = ITEMS + "shields:";
        public const string SWORDS = ITEMS + "swords:";
        public const string TRANSITIONS = "transition:";

        public const string EFFECT_HOOKSHOT = EFFECTS + "hookshot:";

        public const string TILESET = "tileset:";
        public const string TILESET_OUTDOORS = TILESET + "outdoors:";
        public const string TILESET_INDOORS = TILESET + "indoors:";

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
        public const string EFFECT_SPARKLE = EFFECTS + "sparkle";
        public const string EFFECT_HOOKSHOT_TIP = EFFECT_HOOKSHOT + "tip";
        public const string EFFECT_HOOKSHOT_TIP_UP = EFFECT_HOOKSHOT + "tipUp";
        public const string EFFECT_HOOKSHOT_TIP_DOWN = EFFECT_HOOKSHOT + "tipDown";
        public const string EFFECT_HOOKSHOT_TIP_LEFT = EFFECT_HOOKSHOT + "tipLeft";
        public const string EFFECT_HOOKSHOT_TIP_RIGHT = EFFECT_HOOKSHOT + "tipRight";
        public const string EFFECT_HOOKSHOT_CHAIN = EFFECT_HOOKSHOT + "chain";

        public const string DROPS_HEART = DROPS + "heart";
        public const string DROPS_RUPEE_GREEN = DROPS + "rupeeGreen";
        public const string DROPS_RUPEE_BLUE = DROPS + "rupeeBlue";
        public const string DROPS_RUPEE_ORANGE = DROPS + "rupeeOrange";
        public const string DROPS_RUPEE_PURPLE = DROPS + "rupeePurple";
        public const string DROPS_ARROW = DROPS + "arrows";

        public const string PLAYER_CHARGE_ARROW_LEFT = PLAYER + "chargeArrowLeft";
        public const string PLAYER_HOLD_ARROW_LEFT = PLAYER + "holdArrowLeft";
        public const string PLAYER_SHOOT_ARROW_LEFT = PLAYER + "shootArrowLeft";
        public const string PLAYER_CHARGE_ARROW_RIGHT = PLAYER + "chargeArrowRight";
        public const string PLAYER_HOLD_ARROW_RIGHT = PLAYER + "holdArrowRight";
        public const string PLAYER_SHOOT_ARROW_RIGHT = PLAYER + "shootArrowRight";
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
        public const string PLAYER_HOLD_CANNON_UP = PLAYER + "holdCannonUp";
        public const string PLAYER_SHOOT_CANNON_UP = PLAYER + "shootCannonUp";
        public const string PLAYER_HOLD_CANNON_LEFT = PLAYER + "holdCannonLeft";
        public const string PLAYER_SHOOT_CANNON_LEFT = PLAYER + "shootCannonLeft";
        public const string PLAYER_HOLD_CANNON_RIGHT = PLAYER + "holdCannonRight";
        public const string PLAYER_SHOOT_CANNON_RIGHT = PLAYER + "shootCannonRight";
        public const string PLAYER_THROW_BOOMERANG_UP = PLAYER + "throwBoomerangUp";
        public const string PLAYER_THROW_BOOMERANG_DOWN = PLAYER + "throwBoomerangDown";
        public const string PLAYER_THROW_BOOMERANG_LEFT = PLAYER + "throwBoomerangLeft";
        public const string PLAYER_GOT_ITEM = PLAYER + "gotItem";
        public const string PLAYER_WALKDOWN = PLAYER + "WalkDown";
        public const string PLAYER_WALKLEFT = PLAYER + "WalkLeft";
        public const string PLAYER_WALKRIGHT = PLAYER + "WalkRight";
        public const string PLAYER_WALKUP = PLAYER + "WalkUp";
        public const string PLAYER_IDLEDOWN = PLAYER + "IdleDown";
        public const string PLAYER_IDLEUP = PLAYER + "IdleUp";
        public const string PLAYER_IDLELEFT = PLAYER + "IdleLeft";
        public const string PLAYER_IDLERIGHT = PLAYER + "IdleRight";
        public const string PLAYER_SWINGUP = PLAYER + "SwingUp";
        public const string PLAYER_SWINGDOWN = PLAYER + "SwingDown";
        public const string PLAYER_SWINGLEFT = PLAYER + "SwingLeft";
        public const string PLAYER_SWINGRIGHT = PLAYER + "SwingRight";
        public const string PLAYER_ROLLDOWN = PLAYER + "RollDown";
        public const string PLAYER_ROLLUP = PLAYER + "RollUp";
        public const string PLAYER_ROLLLEFT = PLAYER + "RollLeft";
        public const string PLAYER_ROLLRIGHT = PLAYER + "RollRight";
        public const string PLAYER_LIFTDOWN = PLAYER + "LiftDown";
        public const string PLAYER_LIFTUP = PLAYER + "LiftUp";
        public const string PLAYER_LIFTLEFT = PLAYER + "LiftLeft";
        public const string PLAYER_LIFTRIGHT = PLAYER + "LiftRight";
        public const string PLAYER_CARRYDOWN = PLAYER + "CarryDown";
        public const string PLAYER_CARRYUP = PLAYER + "CarryUp";
        public const string PLAYER_CARRYLEFT = PLAYER + "CarryLeft";
        public const string PLAYER_CARRYRIGHT = PLAYER + "CarryRight";
        public const string PLAYER_LIFTIDLEDOWN = PLAYER + "LiftIdleDown";
        public const string PLAYER_LIFTIDLEUP = PLAYER + "LiftIdleUp";
        public const string PLAYER_LIFTIDLELEFT = PLAYER + "LiftIdleLeft";
        public const string PLAYER_LIFTIDLERIGHT = PLAYER + "LiftIdleRight";
        public const string PLAYER_THROWDOWN = PLAYER + "ThrowDown";
        public const string PLAYER_THROWUP = PLAYER + "ThrowUp";
        public const string PLAYER_THROWLEFT = PLAYER + "ThrowLeft";
        public const string PLAYER_THROWRIGHT = PLAYER + "ThrowRight";
        public const string PLAYER_SHOCKDOWN = PLAYER + "ShockDown";
        public const string PLAYER_SHOCKLEFT = PLAYER + "ShockLeft";
        public const string PLAYER_SHOCKRIGHT = PLAYER + "ShockRight";
        public const string PLAYER_SHOCKUP = PLAYER + "ShockUp";
        public const string PLAYER_FREEZEDOWN = PLAYER + "FreezeDown";
        public const string PLAYER_FREEZEUP = PLAYER + "FreezeUp";
        public const string PLAYER_FREEZELEFT = PLAYER + "FreezeLeft";
        public const string PLAYER_FREEZERIGHT = PLAYER + "FreezeRight";
        public const string PLAYER_SHADOWLEFT = PLAYER + "ShadowLeft";
        public const string PLAYER_SHADOWRIGHT = PLAYER + "ShadowRight";
        public const string PLAYER_SHADOWUP = PLAYER + "ShadowUp";
        public const string PLAYER_SHADOWDOWN = PLAYER + "ShadowDown";
        public const string PLAYER_PULL_SWORD_UP = PLAYER + "PullSwordUp";
        public const string PLAYER_PULL_SWORD_DOWN = PLAYER + "PullSwordDown";
        public const string PLAYER_PULL_SWORD_LEFT = PLAYER + "PullSwordLeft";
        public const string PLAYER_PULL_SWORD_RIGHT = PLAYER + "PullSwordRight";
        public const string PLAYER_CHARGE_SWORD_IDLE_UP = PLAYER + "ChargeSwordIdleUp";
        public const string PLAYER_CHARGE_SWORD_IDLE_DOWN = PLAYER + "ChargeSwordIdleDown";
        public const string PLAYER_CHARGE_SWORD_IDLE_LEFT = PLAYER + "ChargeSwordIdleLeft";
        public const string PLAYER_CHARGE_SWORD_IDLE_RIGHT = PLAYER + "ChargeSwordIdleRight";
        public const string PLAYER_CHARGE_SWORD_UP = PLAYER + "ChargeSwordUp";
        public const string PLAYER_CHARGE_SWORD_DOWN = PLAYER + "ChargeSwordDown";
        public const string PLAYER_CHARGE_SWORD_LEFT = PLAYER + "ChargeSwordLeft";
        public const string PLAYER_CHARGE_SWORD_RIGHT = PLAYER + "ChargeSwordRight";
        public const string PLAYER_SPIN_ATTACK_UP = PLAYER + "SpinAttackUp";
        public const string PLAYER_SPIN_ATTACK_DOWN = PLAYER + "SpinAttackDown";
        public const string PLAYER_SPIN_ATTACK_LEFT = PLAYER + "SpinAttackLeft";
        public const string PLAYER_SPIN_ATTACK_RIGHT = PLAYER + "SpinAttackRight";
        public const string PLAYER_VAULT_UP = PLAYER + "VaultUp";
        public const string PLAYER_VAULT_LEFT = PLAYER + "VaultLeft";
        public const string PLAYER_VAULT_RIGHT = PLAYER + "VaultRight";
        public const string PLAYER_VAULT_DOWN = PLAYER + "VaultDown";
        public const string PLAYER_VAULT_IDLE_LEFT = PLAYER + "VaultIdleLeft";
        public const string PLAYER_VAULT_IDLE_RIGHT = PLAYER + "VaultIdleRight";
        public const string PLAYER_VAULT_IDLE_UP = PLAYER + "VaultIdleUp";
        public const string PLAYER_VAULT_IDLE_DOWN = PLAYER + "VaultIdleDown";
        public const string PLAYER_CLIMB_IDLE = PLAYER + "ClimbIdle";
        public const string PLAYER_CLIMB = PLAYER + "Climb";
        public const string PLAYER_CLIMB_UP = PLAYER + "ClimbUp";
        public const string PLAYER_DIE_SPIN = PLAYER + "DieSpin";
        public const string PLAYER_DIE_FALL = PLAYER + "DieFall";
        public const string PLAYER_DEAD = PLAYER + "Dead";
        public const string PLAYER_WIGGLE = PLAYER + "Wiggle";
        public const string PLAYER_FALL_ON_ASS = PLAYER + "FallOnAss";
        public const string PLAYER_PULL_UP_DOWN = PLAYER + "pullUpDown";
        public const string PLAYER_PULL_UP_UP = PLAYER + "pullUpUp";
        public const string PLAYER_PULL_UP_LEFT = PLAYER + "pullUpLeft";
        public const string PLAYER_PULL_UP_RIGHT = PLAYER + "pullUpRight";
        public const string PLAYER_PULL_DOWN_HOLD = PLAYER + "pullDownHold";
        public const string PLAYER_PUDDLE = PLAYER + "puddle";
        public const string PLAYER_DROWN_DOWN = PLAYER + "drownDown";
        public const string PLAYER_DROWN_LEFT = PLAYER + "drownLeft";
        public const string PLAYER_DROWN_RIGHT = PLAYER + "drownRight";
        public const string PLAYER_DROWN_UP = PLAYER + "drownUp";
        public const string PLAYER_JUMP_UP = PLAYER + "jumpUp";
        public const string PLAYER_JUMP_LEFT = PLAYER + "jumpLeft";
        public const string PLAYER_JUMP_RIGHT = PLAYER + "jumpRight";
        public const string PLAYER_JUMP_DOWN = PLAYER + "jumpDown";
        public const string PLAYER_FLY_START_DOWN = PLAYER + "flyStartDown";
        public const string PLAYER_FLY_START_LEFT = PLAYER + "flyStartLeft";
        public const string PLAYER_FLY_START_RIGHT = PLAYER + "flyStartRight";
        public const string PLAYER_FLY_START_UP = PLAYER + "flyStartUp";
        public const string PLAYER_FLY_DOWN = PLAYER + "flyDown";
        public const string PLAYER_FLY_RIGHT = PLAYER + "flyRight";
        public const string PLAYER_FLY_LEFT = PLAYER + "flyLeft";
        public const string PLAYER_FLY_UP = PLAYER + "flyUp";
        public const string PLAYER_FLY_LAND_LEFT = PLAYER + "flyLandLeft";
        public const string PLAYER_FLY_LAND_RIGHT = PLAYER + "flyLandRight";
        public const string PLAYER_FLY_LAND_UP = PLAYER + "flyLandUp";
        public const string PLAYER_FLY_LAND_DOWN = PLAYER + "flyLandDown";
        public const string PLAYER_HOOKSHOT_DOWN = PLAYER + "hookShotDown";
        public const string PLAYER_HOOKSHOT_UP = PLAYER + "hookShotUp";
        public const string PLAYER_HOOKSHOT_LEFT = PLAYER + "hookShotLeft";
        public const string PLAYER_HOOKSHOT_RIGHT = PLAYER + "hookShotRight";
        public const string PLAYER_HOOKSHOT_IDLE_DOWN = PLAYER + "hookShotIdleDown";
        public const string PLAYER_HOOKSHOT_IDLE_UP = PLAYER + "hookShotIdleUp";
        public const string PLAYER_HOOKSHOT_IDLE_LEFT = PLAYER + "hookShotIdleLeft";
        public const string PLAYER_HOOKSHOT_IDLE_RIGHT = PLAYER + "hookShotIdleRight";
        public const string PLAYER_HOOKSHOT_WOOSH_UP = PLAYER + "hookShotWooshUp";
        public const string PLAYER_HOOKSHOT_WOOSH_DOWN = PLAYER + "hookShotWooshDown";
        public const string PLAYER_HOOKSHOT_WOOSH_LEFT = PLAYER + "hookShotWooshLeft";
        public const string PLAYER_HOOKSHOT_WOOSH_RIGHT = PLAYER + "hookShotWooshRight";

        public const string HUD_ARROWS = HUD + "arrows";
        public const string HUD_ARROWS_FIRE = HUD + "arrowsFire";
        public const string HUD_ARROWS_ICE = HUD + "arrowsIce";
        public const string HUD_BOMB_CANNON = HUD + "bombCannon";
        public const string HUD_RUPEES = HUD + "rupees";
        public const string HUD_ARROW_COUNTER = HUD + "arrowCounter";
        public const string HUD_BOMB_COUNTER = HUD + "bombCounter";
        public const string HUD_ACTION = HUD + "action";
        public const string HUD_PAUSE_CURSOR = HUD + "pauseCursor";
        public const string HUD_MAGIC_METER = HUD + "magicMeter";
        public const string HUD_NOTORIETY_MEDIUM = HUD + "notorietyMedium";
        public const string HUD_PICKPOCKET_ICON_PETTY = HUD + "pickpocketIconPetty";
        public const string HUD_EMPTY_BOTTLE = HUD + "emptyBottle";
        public const string HUD_RED_POTION = HUD + "redPotion";
        public const string HUD_GREEN_POTION = HUD + "greenPotion";
        public const string HUD_BLUE_POTION = HUD + "bluePotion";
        public const string HUD_SHADOW_MEDALLION = HUD + "shadowMedallion";
        public const string HUD_ROCS_CAPE = HUD + "rocsCape";
        public const string HUD_HOOKSHIT = HUD + "hookshot";

        public const string HUD_TEXTBOX = HUD + "text:textBox";
        public const string HUD_HEALTH0 = HUD + "health0";
        public const string HUD_HEALTH1 = HUD + "health1";
        public const string HUD_HEALTH2 = HUD + "health2";
        public const string HUD_HEALTH3 = HUD + "health3";
        public const string HUD_HEALTH4 = HUD + "health4";
        public const string HUD_HEALTH5 = HUD + "health5";
        public const string HUD_BUTTON_LEFT = HUD + "buttonLeft";
        public const string HUD_BUTTON_RIGHT = HUD + "buttonRight";
        public const string HUD_BUTTON_UP = HUD + "buttonUp";

        public const string HUD_ITEM_SCREEN = HUD + "itemScreen";
        public const string HUD_QUEST_SCREEN = HUD + "questScreen";

        public const string WOOD_SHIELD_ENGAGE_DOWN = SHIELDS + "woodEngageDown";

        public const string WOOD_SHIELD_DISENGAGE_DOWN = SHIELDS + "woodDisengageDown";

        public const string WOOD_SHIELD_DOWN = SHIELDS + "woodDown";
        public const string WOOD_SHIELD_LEFT = SHIELDS + "woodLeft";
        public const string WOOD_SHIELD_RIGHT = SHIELDS + "woodRight";
        public const string WOOD_SHIELD_UP = SHIELDS + "woodUp";

        public const string GERUDO_SWORD_UP = SWORDS + "gerudoSwordUp";
        public const string GERUDO_SWORD_DOWN = SWORDS + "gerudoSworDown";
        public const string GERUDO_SWORD_LEFT = SWORDS + "gerudoSwordLeft";
        public const string GERUDO_SWORD_RIGHT = SWORDS + "gerudoSwordRight";

        public const string GREEN_DOOR = DOORS + "greenDoor";

        public const string TRANSITION_RUMPLE = TRANSITIONS + "rumple";
        public const string TRANSITION_FADE_TO_BLACK = TRANSITIONS + "fadeToBlack";
       
        //source images
        public const string SOURCE_PLAYER = "Player";
        public const string SOURCE_SHIELDS = "shields";
        public const string SOURCE_HUD = "hud";
        public const string SOURCE_MENU = "menu";
        public const string SOURCE_MAGIC_METER = "magicMeter";
        public const string SOURCE_HUD_BUTTONS = "hudButtons";
        public const string SOURCE_DOORS = "doors";
        

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
            textures.Add(PLAYER_WALKDOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:1", "9:1", 25));
            textures.Add(PLAYER_WALKLEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:0", "15:1", 25));
            textures.Add(PLAYER_WALKUP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:0", "21:1", 25));
            textures.Add(PLAYER_IDLEDOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:0", "0:0", 0));
            textures.Add(PLAYER_IDLEUP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "2:0", "2:0", 0));
            textures.Add(PLAYER_IDLELEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "1:0", "1:0", 0));
            textures.Add(PLAYER_SWINGUP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:2", "33:2", 45));
            textures.Add(PLAYER_SWINGDOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:0", "33:0", 45));
            textures.Add(PLAYER_SWINGLEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:1", "32:1", 45));
            textures.Add(PLAYER_ROLLDOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "40:12", "47:12", 25));
            textures.Add(PLAYER_ROLLUP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "40:13", "47:13", 25));
            textures.Add(PLAYER_ROLLLEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "40:14", "46:14", 25));
            textures.Add(PLAYER_LIFTDOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:29", "4:29", 20));
            textures.Add(PLAYER_LIFTUP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:31", "4:31", 20));
            textures.Add(PLAYER_LIFTLEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:30", "4:30", 20));
            textures.Add(PLAYER_CARRYDOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:32", "9:32", 25));
            textures.Add(PLAYER_CARRYUP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:34", "9:34", 25));
            textures.Add(PLAYER_CARRYLEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:33", "9:33", 25));
            textures.Add(PLAYER_LIFTIDLEDOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "4:29", "4:29", 0));
            textures.Add(PLAYER_LIFTIDLEUP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "4:31", "4:31", 0));
            textures.Add(PLAYER_LIFTIDLELEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "4:30", "4:30", 0));
            textures.Add(PLAYER_THROWDOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "5:29", "8:29", 10));
            textures.Add(PLAYER_THROWUP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "5:31", "8:31", 10));
            textures.Add(PLAYER_THROWLEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "5:30", "8:30", 20));
            textures.Add(PLAYER_SHOCKDOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "37:30", "38:30", 30));
            textures.Add(PLAYER_SHOCKLEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "40:30", "41:30", 30));
            textures.Add(PLAYER_SHOCKUP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "37:31", "38:31", 30));
            textures.Add(PLAYER_FREEZEDOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:4", "11:4", 0));
            textures.Add(PLAYER_FREEZEUP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "13:4", "13:4", 0));
            textures.Add(PLAYER_FREEZELEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "12:4", "12:4", 0));
            textures.Add(PLAYER_CHARGE_ARROW_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:7", "25:7", 15));
            textures.Add(PLAYER_CHARGE_ARROW_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:6", "25:6", 15));
            textures.Add(PLAYER_CHARGE_ARROW_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:8", "25:8", 15));
            textures.Add(PLAYER_HOLD_ARROW_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "25:7", "25:7", 0));
            textures.Add(PLAYER_HOLD_ARROW_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "25:6", "25:6", 0));
            textures.Add(PLAYER_HOLD_ARROW_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "25:8", "25:8", 0));
            textures.Add(PLAYER_SHIELD_ENGAGE_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:6", "4:6", 30));
            textures.Add(PLAYER_SHIELD_DISENGAGE_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:6", "4:6", 30, false, false, false, true));
            textures.Add(PLAYER_SHIELD_WALK_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "6:6", "13:6", 10));
            textures.Add(PLAYER_SHIELD_IDLE_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "6:6", "6:6", 0));
            textures.Add(PLAYER_SHIELD_ENGAGE_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:7", "3:7", 30));
            textures.Add(PLAYER_SHIELD_DISENGAGE_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:7", "3:7", 30, false, false, false, true));
            textures.Add(PLAYER_SHIELD_WALK_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "6:7", "13:7", 10));
            textures.Add(PLAYER_SHIELD_IDLE_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "6:7", "6:7", 0));
            textures.Add(PLAYER_SHIELD_ENGAGE_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:8", "4:8", 30));
            textures.Add(PLAYER_SHIELD_DISENGAGE_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "0:8", "4:8", 30, false, false, false, true));
            textures.Add(PLAYER_SHIELD_WALK_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "6:8", "13:8", 10));
            textures.Add(PLAYER_SHIELD_IDLE_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "6:8", "6:8", 0));
            textures.Add(PLAYER_SHOOT_ARROW_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "26:7", "28:7", 50));
            textures.Add(PLAYER_SHOOT_ARROW_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "26:6", "28:6", 50));
            textures.Add(PLAYER_SHOOT_ARROW_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "26:8", "28:8", 50));
            textures.Add(PLAYER_HOLD_CANNON_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "14:42", "20:42", 25));
            textures.Add(PLAYER_SHOOT_CANNON_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:42", "13:42", 25));
            textures.Add(PLAYER_HOLD_CANNON_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:43", "15:43", 25));
            textures.Add(PLAYER_SHOOT_CANNON_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:43", "15:43", 25));
            textures.Add(PLAYER_HOLD_CANNON_RIGHT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:44", "12:44", 25));
            textures.Add(PLAYER_SHOOT_CANNON_RIGHT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:44", "12:44", 25));
            textures.Add(PLAYER_THROW_BOOMERANG_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:21", "22:21", 15));
            textures.Add(PLAYER_THROW_BOOMERANG_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:23", "22:23", 15));
            textures.Add(PLAYER_THROW_BOOMERANG_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:22", "22:22", 15));
            textures.Add(PLAYER_GOT_ITEM, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "21:2", "21:2", 0));
            textures.Add(PLAYER_SHADOWDOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "10:2", "10:2", 0));
            textures.Add(PLAYER_SHADOWLEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:2", "11:2", 0));
            textures.Add(PLAYER_SHADOWUP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "12:2", "12:2", 0));
            textures.Add(PLAYER_PULL_SWORD_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "29:3", "31:3", 15));
            textures.Add(PLAYER_PULL_SWORD_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "29:4", "31:4", 15));
            textures.Add(PLAYER_PULL_SWORD_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "29:5", "31:5", 15));
            textures.Add(PLAYER_CHARGE_SWORD_IDLE_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "25:3", "25:3", 0));
            textures.Add(PLAYER_CHARGE_SWORD_IDLE_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "25:4", "25:4", 0));
            textures.Add(PLAYER_CHARGE_SWORD_IDLE_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "25:5", "25:5", 0));
            textures.Add(PLAYER_CHARGE_SWORD_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:3", "27:3", 15));
            textures.Add(PLAYER_CHARGE_SWORD_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:4", "27:4", 15));
            textures.Add(PLAYER_CHARGE_SWORD_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:5", "27:5", 15));
            textures.Add(PLAYER_SPIN_ATTACK_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "35:0", "43:0", 20));
            textures.Add(PLAYER_SPIN_ATTACK_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "35:1", "43:1", 20));
            textures.Add(PLAYER_SPIN_ATTACK_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "35:2", "43:2", 20));
            textures.Add(PLAYER_VAULT_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "2:9", "7:9", 20));
            textures.Add(PLAYER_VAULT_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "2:10", "7:10", 20));
            textures.Add(PLAYER_VAULT_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "2:11", "7:11", 20));
            textures.Add(PLAYER_VAULT_IDLE_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "7:9", "7:9", 0));
            textures.Add(PLAYER_VAULT_IDLE_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "7:10", "7:10", 0));
            textures.Add(PLAYER_VAULT_IDLE_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "7:11", "7:11", 0));
            textures.Add(PLAYER_CLIMB, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "37:16", "44:16", 20));
            textures.Add(PLAYER_CLIMB_IDLE, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "44:16", "44:16", 0));
            textures.Add(PLAYER_CLIMB_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "45:16", "49:16", 18));
            textures.Add(PLAYER_DIE_SPIN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "15:5", "16:6", 4));
            textures.Add(PLAYER_DIE_FALL, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:8", "21:8", 15));
            textures.Add(PLAYER_DEAD, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "21:8", "21:8", 0));
            textures.Add(PLAYER_WIGGLE, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "23:11", "27:11", 15));
            textures.Add(PLAYER_FALL_ON_ASS, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "29:11", "38:11", 15));
            textures.Add(PLAYER_PULL_UP_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "10:28", "11:28", 10));
            textures.Add(PLAYER_PULL_UP_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "16:28", "17:28", 10));
            textures.Add(PLAYER_PULL_UP_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "13:28", "14:28", 10));
            textures.Add(PLAYER_PULL_DOWN_HOLD, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "11:28", "11:28", 10));
            textures.Add(PLAYER_PUDDLE, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "45:26", "46:26", 2));
            textures.Add(PLAYER_DROWN_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "4:42", "7:42", 10));
            textures.Add(PLAYER_DROWN_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "4:43", "7:43", 10));
            textures.Add(PLAYER_DROWN_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "4:44", "7:44", 10));
            textures.Add(PLAYER_JUMP_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "39:17", "43:17", 14));
            textures.Add(PLAYER_JUMP_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "39:18", "43:18", 14));
            textures.Add(PLAYER_JUMP_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "39:19", "43:19", 14));
            textures.Add(PLAYER_FLY_START_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "29:21", "31:21", 14));
            textures.Add(PLAYER_FLY_START_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "29:22", "31:22", 14));
            textures.Add(PLAYER_FLY_START_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "29:23", "31:23", 14));
            textures.Add(PLAYER_FLY_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "31:21", "34:21", 14));
            textures.Add(PLAYER_FLY_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "31:22", "34:22", 14));
            textures.Add(PLAYER_FLY_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "31:23", "34:23", 14));
            textures.Add(PLAYER_FLY_LAND_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "35:21", "35:21", 3));
            textures.Add(PLAYER_FLY_LAND_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "35:22", "35:22", 3));
            textures.Add(PLAYER_FLY_LAND_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "35:23", "35:23", 3));
            textures.Add(PLAYER_HOOKSHOT_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:24", "19:24", 8));
            textures.Add(PLAYER_HOOKSHOT_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:26", "19:26", 8));
            textures.Add(PLAYER_HOOKSHOT_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "17:25", "19:25", 8));
            textures.Add(PLAYER_HOOKSHOT_IDLE_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "19:24", "19:24", 0));
            textures.Add(PLAYER_HOOKSHOT_IDLE_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "19:26", "19:26", 0));
            textures.Add(PLAYER_HOOKSHOT_IDLE_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "19:25", "19:25", 0));
            textures.Add(PLAYER_HOOKSHOT_WOOSH_LEFT, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "40:17", "40:17", 0));
            textures.Add(PLAYER_HOOKSHOT_WOOSH_UP, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "40:18", "40:18", 0));
            textures.Add(PLAYER_HOOKSHOT_WOOSH_DOWN, new CTextureAtlas(SOURCE_PLAYER, 32, 32, 1, "40:19", "40:19", 0));

            textures.Add(GERUDO_SWORD_DOWN, new CTextureAtlas("Swords", 64, 64, 1, "0:0", "7:0", 45));
            textures.Add(GERUDO_SWORD_UP, new CTextureAtlas("Swords", 64, 64, 1, "0:1", "7:1", 45));
            textures.Add(GERUDO_SWORD_RIGHT, new CTextureAtlas("Swords", 64, 64, 1, "0:2", "7:2", 45));

            textures.Add(WOOD_SHIELD_DOWN, new CTextureAtlas(SOURCE_SHIELDS, 32, 32, 1, "2:0", "2:0", 0));
            textures.Add(WOOD_SHIELD_ENGAGE_DOWN, new CTextureAtlas(SOURCE_SHIELDS, 32, 32, 1, "0:0", "2:0", 15));
            textures.Add(WOOD_SHIELD_DISENGAGE_DOWN, new CTextureAtlas(SOURCE_SHIELDS, 32, 32, 1, "0:1", "2:1", 15));
            textures.Add(WOOD_SHIELD_LEFT, new CTextureAtlas(SOURCE_SHIELDS, 32, 32, 1, "0:2", "0:2", 0));
            textures.Add(WOOD_SHIELD_UP, new CTextureAtlas(SOURCE_SHIELDS, 32, 32, 1, "1:2", "1:2", 0));

            textures.Add("effects:Fire", new CTextureAtlas("effects", 32, 32, 1, "0:0", "3:0", 15));
            textures.Add("effects:Ice", new CTextureAtlas("effects", 32, 32, 1, "0:1", "3:1", 8));
            textures.Add("effects:Shadow", new CTextureAtlas("effects", 32, 32, 1, "0:2", "3:2", 15));
            textures.Add("effects:Thunder", new CTextureAtlas("effects", 32, 32, 1, "0:3", "2:3", 10));

            //tilesets
            textures.Add("tileset:test", new CTextureAtlas("test", 16, 16, 0, "0:0", "15:15",0,false,false,true));
            textures.Add("tileset:ages", new CTextureAtlas("ages", 16, 16, 0, "0:0", "15:15", 0, false, false, true));
            textures.Add("tileset:outdoors:hyruleCastleTown", new CTextureAtlas("tileset:outdoors:hyruleCastleTown", 16, 16, 0, "0:0", "31:17", 0, false, false, true));
            textures.Add("tileset:indoors:house", new CTextureAtlas("tileset:indoors:house", 16, 16, 0, "0:0", "24:16", 0, false, false, true));
            textures.Add("tileset:smallLantern", new CTextureAtlas("tileset:items:smallItems", 16, 16, 1, "0:0", "8:0", 5, false, false, false));
            textures.Add("tileset:items:chests-small", new CTextureAtlas("tileset:items:chests-small", 16, 16, 1, "0:0", "1:2", 0, false, false, false));
            textures.Add("tileset:outdoors:demoTiles", new CTextureAtlas("tileset:outdoors:demoTiles", 16, 16, 0, "0:0", "49:41", 0, false, false, true));
            textures.Add("tileset:outdoors:castleTown", new CTextureAtlas("tileset:outdoors:castleTown", 16, 16, 0, "0:0", "54:46", 0, false, false, true));
            textures.Add("tileset:outdoors:kokiriForest", new CTextureAtlas("tileset:outdoors:kokiriForest", 16, 16, 0, "0:0", "24:22", 0, false, false, true));
            textures.Add("tileset:indoors:sewer", new CTextureAtlas("tileset:indoors:sewer", 16, 16, 0, "0:0", "18:22", 0, false, false, true));
            textures.Add("tileset:indoors:castleTown", new CTextureAtlas("tileset:indoors:castleTown", 16, 16, 0, "0:0", "49:44", 0, false, false, true));
            textures.Add("tileset:indoors:rumpBattle", new CTextureAtlas("tileset:indoors:rumpBattle", 16, 16, 0, "0:0", "11:6", 0, false, false, true));
            textures.Add(TILESET_OUTDOORS + "dekuSwamp", new CTextureAtlas(TILESET_OUTDOORS + "dekuSwamp", 16, 16, 0, "0:0", "15:27", 0, false, false, true));

            //items and shit
            textures.Add("items:decor:potSmall", new CTextureAtlas("potSmall", 48, 48, 1, "0:0", "0:0"));
            textures.Add("items:decor:potSmallBreak", new CTextureAtlas("potSmall", 48, 48, 1, "1:0", "8:0",10));
            textures.Add(GREEN_DOOR, new CTextureAtlas(SOURCE_DOORS, 24, 15, 0, "0:0", "0:0",0));

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
            textures.Add(EFFECT_SPARKLE, new CTextureAtlas("effects:various", 32, 32, 0, "4:4", "5:5", 4));
            textures.Add(EFFECT_HOOKSHOT_TIP_UP, new CTextureAtlas("effects:various", 32, 32, 0, "0:5", "0:5", 0));
            textures.Add(EFFECT_HOOKSHOT_TIP_RIGHT, new CTextureAtlas("effects:various", 32, 32, 0, "1:5", "1:5", 0));
            textures.Add(EFFECT_HOOKSHOT_CHAIN, new CTextureAtlas("effects:various", 32, 32, 0, "2:5", "2:5", 0));

            //HUD
            textures.Add(HUD_TEXTBOX, new CTextureAtlas("hud", 303, 74, 0, "0:0", "0:0", 0));
            textures.Add(HUD_HEALTH0, new CTextureAtlas("health", 16, 16, 1, "0:0", "0:0", 0));
            textures.Add(HUD_HEALTH1, new CTextureAtlas("health", 16, 16, 1, "1:0", "1:0", 0));
            textures.Add(HUD_HEALTH2, new CTextureAtlas("health", 16, 16, 1, "2:0", "2:0", 0));
            textures.Add(HUD_HEALTH3, new CTextureAtlas("health", 16, 16, 1, "3:0", "3:0", 0));
            textures.Add(HUD_HEALTH4, new CTextureAtlas("health", 16, 16, 1, "4:0", "4:0", 0));
            textures.Add(HUD_HEALTH5, new CTextureAtlas("health", 16, 16, 1, "5:0", "5:0", 0));
            textures.Add(HUD_BUTTON_LEFT, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "4:1", "4:1", 0));
            textures.Add(HUD_BUTTON_RIGHT, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "5:1", "5:1", 0));
            textures.Add(HUD_BUTTON_UP, new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 1, "2:0", "2:0", 0));
            textures.Add(HUD_ARROWS, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "0:0", "0:0", 0));
            textures.Add(HUD_ARROWS_FIRE, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "0:0", "0:0", 0));
            textures.Add(HUD_ARROWS_ICE, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "0:0", "0:0", 0));
            textures.Add(HUD_ACTION, new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 1, "3:0", "3:0", 0));
            textures.Add(HUD_BOMB_CANNON, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "1:1", "1:1", 0));
            textures.Add(HUD_RUPEES, new CTextureAtlas("health", 16, 16, 1, "0:1", "0:1", 0));
            textures.Add(HUD_ARROW_COUNTER, new CTextureAtlas("health", 16, 16, 1, "2:1", "2:1"));
            textures.Add(HUD_BOMB_COUNTER, new CTextureAtlas("health", 16, 16, 1, "1:1", "1:1", 0));
            textures.Add(HUD_ITEM_SCREEN, new CTextureAtlas(SOURCE_MENU, 240, 160, 0, "0:0", "0:0", 0));
            textures.Add(HUD_QUEST_SCREEN, new CTextureAtlas(SOURCE_MENU, 240, 160, 0, "0:0", "0:0", 0));
            textures.Add(HUD_PAUSE_CURSOR, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "3:2", "3:2", 0));
            textures.Add(HUD_MAGIC_METER, new CTextureAtlas(SOURCE_MAGIC_METER, 80, 6, 0, "0:0", "0:0", 0));
            textures.Add(HUD_NOTORIETY_MEDIUM, new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 1, "2:1", "2:1", 0)); 
            textures.Add(HUD_PICKPOCKET_ICON_PETTY, new CTextureAtlas(SOURCE_HUD_BUTTONS, 32, 32, 1, "3:2", "3:2", 0));
            textures.Add(HUD_EMPTY_BOTTLE, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "0:2", "0:2", 0));
            textures.Add(HUD_RED_POTION, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "1:2", "1:2", 0));
            textures.Add(HUD_GREEN_POTION, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "1:2", "1:2", 0));
            textures.Add(HUD_BLUE_POTION, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "1:2", "1:2", 0));
            textures.Add(HUD_SHADOW_MEDALLION, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "1:0", "1:0", 0));
            textures.Add(HUD_ROCS_CAPE, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "2:0", "2:0", 0));
            textures.Add(HUD_HOOKSHIT, new CTextureAtlas(SOURCE_HUD_BUTTONS, 24, 24, 1, "2:1", "2:1", 0));

            //transitions
            textures.Add(TRANSITION_RUMPLE, new CTextureAtlas(TRANSITION_RUMPLE, 240, 160, 0, "0:0", "3:2", 15));
            textures.Add(TRANSITION_FADE_TO_BLACK, new CTextureAtlas(TRANSITION_FADE_TO_BLACK, 240, 160, 0, "0:0", "1:4", 15));

            //drops
            textures.Add(DROPS_HEART, new CTextureAtlas("drops:drops01", 16, 16, 1, "0:0", "0:0"));
            textures.Add(DROPS_RUPEE_GREEN, new CTextureAtlas("drops:drops01", 16, 16, 1, "0:1", "0:1"));
            textures.Add(DROPS_RUPEE_BLUE, new CTextureAtlas("drops:drops01", 16, 16, 1, "1:1", "1:1"));
            textures.Add(DROPS_RUPEE_ORANGE, new CTextureAtlas("drops:drops01", 16, 16, 1, "2:1", "2:1"));
            textures.Add(DROPS_RUPEE_PURPLE, new CTextureAtlas("drops:drops01", 16, 16, 1, "3:1", "3:1"));
            textures.Add(DROPS_ARROW, new CTextureAtlas("drops:drops01", 16, 16, 1, "2:0", "2:0"));

            //debug
            textures.Add("debug:redBox", new CTextureAtlas("debug:redBox", 1, 1, 0, "0:0", "0:0"));

            //splash
            textures.Add("splash:link", new CTextureAtlas("splash:link", 16, 24, 1, "0:0", "6:0", 15));
        }

        public static void addRawTexture(string textureName, string fileNameNoExt)
        {
            rawTextures.Add(textureName, _content.Load<Texture2D>(fileNameNoExt));
        }

        public static void addTexture(string textureName, CTextureAtlas atlas)
        {
            if (!textures.ContainsKey(textureName))
                textures.Add(textureName, atlas);
        }

        private static void _prepareTextures()
        {
            rawTextures.Add("Player", _content.Load<Texture2D>("Player"));
            rawTextures.Add("Swords", _content.Load<Texture2D>("Swords"));
            rawTextures.Add("keese", _content.Load<Texture2D>("keese"));
            rawTextures.Add("test", _content.Load<Texture2D>("tiletest"));
            rawTextures.Add("ages", _content.Load<Texture2D>("AGES_TILESET_1F"));
            rawTextures.Add("icons", _content.Load<Texture2D>("icons"));
            rawTextures.Add("pointer", _content.Load<Texture2D>("pointer"));
            rawTextures.Add("effects", _content.Load<Texture2D>("effects"));
            rawTextures.Add("potSmall", _content.Load<Texture2D>("potSmall"));
            rawTextures.Add("maple", _content.Load<Texture2D>("maple"));

            rawTextures.Add(SOURCE_SHIELDS, _content.Load<Texture2D>("sprites/items/shields"));
            rawTextures.Add(SOURCE_DOORS, _content.Load<Texture2D>("tilesets/indoors/jim_morrison"));

            rawTextures.Add("splash:link", _content.Load<Texture2D>("sprites/other/splash"));

            //drops
            rawTextures.Add("drops:drops01", _content.Load<Texture2D>("sprites/drops/drops01"));
            
            //tilesets
            rawTextures.Add(TILESET_INDOORS + "house", _content.Load<Texture2D>(@"tilesets/indoors/house"));
            rawTextures.Add("tileset:items:chests-small", _content.Load<Texture2D>(@"tilesets/items/chests-small"));
            rawTextures.Add("tileset:items:smallItems", _content.Load<Texture2D>(@"tilesets/items/smallItems"));
            rawTextures.Add(TILESET_OUTDOORS + "hyruleCastleTown", _content.Load<Texture2D>(@"tilesets/outdoors/hyruleCastleTown"));
            rawTextures.Add(TILESET_OUTDOORS + "demoTiles", _content.Load<Texture2D>(@"tilesets/outdoors/demoTiles"));
            rawTextures.Add(TILESET_OUTDOORS + "castleTown", _content.Load<Texture2D>(@"tilesets/outdoors/castleTown"));
            rawTextures.Add(TILESET_OUTDOORS + "kokiriForest", _content.Load<Texture2D>(@"tilesets/outdoors/kokiriForest"));
            rawTextures.Add(TILESET_OUTDOORS + "dekuSwamp", _content.Load<Texture2D>(@"tilesets/outdoors/dekuSwamp"));
            rawTextures.Add(TILESET_INDOORS + "sewer", _content.Load<Texture2D>(@"tilesets/indoors/sewer"));
            rawTextures.Add(TILESET_INDOORS + "castleTown", _content.Load<Texture2D>(@"tilesets/indoors/castleTownIndoors"));
            rawTextures.Add(TILESET_INDOORS + "rumpBattle", _content.Load<Texture2D>(@"tilesets/indoors/rumpFightRoom"));

            //effects
            rawTextures.Add("effects:explosion", _content.Load<Texture2D>(@"effects/bomb-explosion"));
            rawTextures.Add("effects:various", _content.Load<Texture2D>(@"effects/various-effects"));

            //transitions
            rawTextures.Add(TRANSITION_RUMPLE, _content.Load<Texture2D>(@"sprites/transitions/rumpleTransition"));
            rawTextures.Add(TRANSITION_FADE_TO_BLACK, _content.Load<Texture2D>(@"sprites/transitions/fadeToBlack"));

            //hud
            rawTextures.Add("hud", _content.Load<Texture2D>("hud/textbox2"));
            rawTextures.Add("health", _content.Load<Texture2D>("hud/hearts"));
            rawTextures.Add(SOURCE_HUD_BUTTONS, _content.Load<Texture2D>("hud/hud"));
            rawTextures.Add(SOURCE_MENU, _content.Load<Texture2D>("hud/menu"));
            rawTextures.Add(SOURCE_MAGIC_METER, _content.Load<Texture2D>("hud/magicMeter"));

            Texture2D texture = new Texture2D(CGraphics.GPU, 1, 1, false, SurfaceFormat.Color);
            Color[] c = new Color[1];
            c[0] = Color.FromNonPremultiplied(255, 255, 255, 200);
            texture.SetData<Color>(c);

            rawTextures.Add("debug:redBox", texture);
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
