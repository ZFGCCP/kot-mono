using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.HUD.buttons
{
    public enum HUDOPTIONS
    {
        ARROWS = 1,
        BOOMERANG,
        BOMB_CANNON

    }

    public enum HUD_ACTION_OPTIONS
    {
        OPEN = 1,
        NONE
    }

    class CButton : CHUDElement
    {
        public enum HUD_BUTTON_TYPE
        {
            LEFT = 0,
            RIGHT,
            ACTION
        }

        

        private readonly HUD_BUTTON_TYPE _BUTTON_TYPE = 0;
        public HUDOPTIONS hudItem = HUDOPTIONS.ARROWS;
        public HUD_ACTION_OPTIONS actionOption = HUD_ACTION_OPTIONS.NONE;

        private const string _BUTTON_LEFT = "buttonLeft";
        private const string _BUTTON_RIGHT = "buttonRight";
        private const string _BUTTON_ACTION = "buttonAction";
        private Graphics.CSprite _itemOverlay = null;
        public CButton(HUD_BUTTON_TYPE type)
        {
            _BUTTON_TYPE = type;

            switch (_BUTTON_TYPE)
            {
                case HUD_BUTTON_TYPE.LEFT:
                    _imageIndex.Add(_BUTTON_LEFT, new Graphics.CSprite("HUD:buttonLeft"));
                    swapImage(_BUTTON_LEFT);
                    _fixedPosition = new Vector2(250, 10);
                    changeItemOverlay(HUDOPTIONS.ARROWS);
                    break;

                case HUD_BUTTON_TYPE.RIGHT:
                    _imageIndex.Add(_BUTTON_RIGHT, new Graphics.CSprite("HUD:buttonRight"));
                    swapImage(_BUTTON_RIGHT);
                    _fixedPosition = new Vector2(283, 10);
                    changeItemOverlay(HUDOPTIONS.BOOMERANG);
                    break;

                case HUD_BUTTON_TYPE.ACTION:
                    _imageIndex.Add(_BUTTON_ACTION, new Graphics.CSprite(Graphics.CTextures.HUD_ACTION));
                    swapImage(_BUTTON_ACTION);
                    _fixedPosition = new Vector2(217, 11);
                    break;
            }
        }

        public override void drawMe(bool useOverlay = false)
        {
            base.drawMe(useOverlay);

            if (_itemOverlay != null)
                _itemOverlay.draw((int)_position.X, (int)_position.Y);
        }

        public void changeItemOverlay(HUDOPTIONS option)
        {
            hudItem = option;
            switch (hudItem)
            {
                case HUDOPTIONS.ARROWS:
                    _itemOverlay = new Graphics.CSprite(Graphics.CTextures.HUD_ARROWS);
                    hudItem = HUDOPTIONS.ARROWS;
                    break;

                case HUDOPTIONS.BOMB_CANNON:
                    _itemOverlay = new Graphics.CSprite(Graphics.CTextures.HUD_BOMB_CANNON);
                    hudItem = HUDOPTIONS.BOMB_CANNON;
                    break;

                //place holder for now
                case HUDOPTIONS.BOOMERANG:
                    _itemOverlay = new Graphics.CSprite(Graphics.CTextures.HUD_BOMB_CANNON);
                    hudItem = HUDOPTIONS.BOOMERANG;
                    break;
            }
        }

        public void changeAction(HUD_ACTION_OPTIONS option)
        {
            actionOption = option;
            switch (actionOption)
            {
                case HUD_ACTION_OPTIONS.OPEN:
                    break;

                case HUD_ACTION_OPTIONS.NONE:
                    break;
            }
        }
    }
}
