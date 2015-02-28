using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors.Items.decoration
{
    enum CHEST_STATES
    {
        LOCKED = 0,
        UNLOCKED,
        HIDDEN
    }

    enum CHEST_TYPES
    {
        BIG_CHEST = 0,
        SMALL_CHEST,
        BIG_KEY
    }

    enum ITEMS_INSIDE
    {
        HEART = 0,
        HEART_PIECE,
        HEART_CONTAINER,
        MAGIC,
        RUPEE_1,
        RUPEE_5,
        RUPEE_10,
        RUPEE_20,
        RUPEE_50,
        RUPEE_100,
        RUPEE_200,
        BOMB_5,
        BOMB_10,
        ARROW_5,
        ARROW_10,
        BOMB_CANNON,
        BIG_BOMB_BAG,
        SEASHELL,
        BOOMERANG,
        BOW,
        DEKU_NUTS_5,
        DEKU_NUTS_10,
        HOOKSHOT,
        MAGNETIC_GLOVES,
        METAL_SHIELD,
        MIRROR_SHIELD,
        ROC_CAPE,
        SHADOW_CLOAK,
        BRACERS,
        KEY,
        BIG_KEY_DEKU,
        BIG_KEY_GORON,
        BIG_KEY_ZORA,
        BIG_KEY_SHIEKAH,
        BIG_KEY_CLOCK,
        MAP_DEKU,
        MAP_GORON,
        MAP_ZORA,
        MAP_SHIEKAH,
        MAP_CLOCK,
        COMPASS_DEKU,
        COMPASS_GORON,
        COMPASS_ZORA,
        COMPASS_SHIEKAH,
        COMPASS_CLOCK,
        MOON_PEARL
    }

    class CChest : CActor
    {
        private CHEST_STATES _chestState;
        private CHEST_TYPES _chestType;
        private ITEMS_INSIDE _itemInside;

       private const string _CHESTS_SMALL = "chests-small";


       public CChest() :
           base()
       {
           _hearingRadius = 10;
           _lineOfSight = 10;
           _visionRange = 20;
           _direction = DIRECTION.DOWN;
           _angle = 270;
       }

       //PARAMETERS
       //0: ChestType
       //1: ChestState
       //2: Item Inside
        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            short chestState, chestType, item;

            chestState = Convert.ToInt16(additional[0]);
            chestType =  Convert.ToInt16(additional[1]);
            item =       Convert.ToInt16(additional[2]);

            _chestState = (CHEST_STATES)chestState;
            _chestType = (CHEST_TYPES)chestType;
            _itemInside = (ITEMS_INSIDE)item;

            _imageIndex.Add(_CHESTS_SMALL, new Graphics.CSprite("tileset:items:chests-small"));
            _loadChest();

            switch (_chestType)
            {
                case CHEST_TYPES.BIG_CHEST:
                    break;

                case CHEST_TYPES.SMALL_CHEST:
                    image = _imageIndex[_CHESTS_SMALL];

                    if (_chestState == CHEST_STATES.LOCKED)
                        image.setFrame(0, 0);
                    else if (_chestState == CHEST_STATES.UNLOCKED)
                        image.setFrame(0, 1);

                    break;
            }

            base.init(name, position, dataType, compAddress, additional);
        }

        private void _loadChest()
        {
            switch (_itemInside)
            {
                case ITEMS_INSIDE.HEART:
                    Map.CMapManager.addActorToComponent(new Actors.Items.Drops.CHeartDrop(), this.componentAddress);
                    break;

                case ITEMS_INSIDE.RUPEE_1:
                    Items.Drops.CRupeeDrop rupee = new Drops.CRupeeDrop();
                    rupee.init(this.name + "loadedItem", _position, "", this.componentAddress, "G");
                    Map.CMapManager.addActorToComponent(rupee, this.componentAddress);
                    break;

                default:
                    break;
            }
            _state = ACTOR_STATES.LOCKED;
        }

        private void _unloadChest()
        {
            _triggerUserEvent(0, this.name + "loadedItem");
            _state = ACTOR_STATES.UNLOCKED;

            CMasterControl.buttonController.createTextBox("You got a thing!");
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            if (_checkIfPointInView(playerPos))
                CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.OPEN);
        }

        public override void keyRelease(object sender)
        {
            base.keyRelease(sender);

            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;
            
            if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.C) && 
                _checkIfPointInView(playerPos) && 
                (DIRECTION)Map.CMapManager.propertyGetter("player", Map.EActorProperties.DIRECTION) == DIRECTION.UP)
            {
                _unloadChest();
            }
        }
    }
}
