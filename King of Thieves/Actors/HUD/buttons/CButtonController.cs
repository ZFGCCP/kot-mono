﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using King_of_Thieves.usr.local.GameMenu;
namespace King_of_Thieves.Actors.HUD.buttons
{
    class CButtonController
    {
        private CButton _buttonLeft = new CButton(CButton.HUD_BUTTON_TYPE.LEFT);
        private CButton _buttonRight = new CButton(CButton.HUD_BUTTON_TYPE.RIGHT);
        private CButton _buttonAction = new CButton(CButton.HUD_BUTTON_TYPE.ACTION);
        private counters.CRupeeCounter _rupeeCounter = new counters.CRupeeCounter();
        private counters.CBombCounter _bombCounter = new counters.CBombCounter();
        private counters.CArrowCounter _arrowCounter = new counters.CArrowCounter();
		private info.CBenchmarkInfo _benchmarkInfo = new info.CBenchmarkInfo();
        private Actors.HUD.Text.CTextBox _textBoxController = new Text.CTextBox();
        private other.CCullBoundary _cullBoundary = new other.CCullBoundary();
        private CScreenFader _screenFader = new CScreenFader();
        public CPauseMenuElement currentElementLeft = null;
        public CPauseMenuElement currentElementRight = null;

        private HUDOPTIONS _bottleState0 = HUDOPTIONS.EMPTY_BOTTLE;
        private HUDOPTIONS _bottleState1 = HUDOPTIONS.EMPTY_BOTTLE;
        private HUDOPTIONS _bottleState2 = HUDOPTIONS.EMPTY_BOTTLE;
        private HUDOPTIONS _bottleState3 = HUDOPTIONS.EMPTY_BOTTLE;

        public CPauseMenuElement[] bottleRef = new CPauseMenuElement[4];

        public bool checkCullBoundary(Vector2 point, Vector2 dimensions)
        {
            return _cullBoundary.checkPointWithinBoundary(point, dimensions);
        }

        public void createTextBox(string message)
        {
            _textBoxController.displayMessageBox(message);
        }

        public void createTextBox(params string[] messages)
        {
            _textBoxController.displayMessageBox(messages);
        }

        public void update(GameTime gameTime)
        {
            _buttonRight.update(gameTime);
            _buttonLeft.update(gameTime);
            _buttonAction.update(gameTime);
            _rupeeCounter.update(gameTime);
            _bombCounter.update(gameTime);
            _arrowCounter.update(gameTime);
			_benchmarkInfo.update (gameTime);
            _textBoxController.update(gameTime);
            _cullBoundary.update(gameTime);
            _screenFader.update(gameTime);
        }

        public int arrowCount
        {
            get
            {
                return _arrowCounter.amount;
            }
        }

        public void changeActionIconState(HUD_ACTION_OPTIONS option)
        {
            _buttonAction.changeAction(option);
        }

        public HUD_ACTION_OPTIONS actionIconState
        {
            get
            {
                return _buttonAction.actionOption;
            }
        }

        public void incrementRupees(int amount, bool instant = false)
        {
            _rupeeCounter.increment(amount, instant);
        }

        public void modifyBombs(int amount, bool instant = false)
        {
            if (amount < 0)
                _bombCounter.decrement(Math.Abs(amount), instant);
            else
                _bombCounter.increment(amount, instant);
        }

        public void modifyArrows(int amount, bool instant = false)
        {
            if (amount < 0)
                _arrowCounter.decrement(Math.Abs(amount), instant);
            else
                _arrowCounter.increment(amount, instant);
        }

        public void drawMe(SpriteBatch spriteBatch)
        {
            _buttonLeft.drawMe();
            _buttonRight.drawMe();
            //_buttonAction.drawMe();
            _rupeeCounter.drawMe();
            _bombCounter.drawMe();
            _arrowCounter.drawMe();

            if(CActor.showHitBox)
			    _benchmarkInfo.drawMe();

            _textBoxController.drawMe();
            _screenFader.drawMe();
        }

        public void beginFade(Vector3 color)
        {
            _screenFader.beginFade(color);
        }

        public bool textBoxActive
        {
            get
            {
                return _textBoxController.active;
            }
        }

        public bool textBoxWait
        {
            get
            {
                return _textBoxController.wait;
            }
        }

        public void switchRightItem(HUDOPTIONS item)
        {
            _buttonRight.changeItemOverlay(item);
        }

        public void switchLeftItem(HUDOPTIONS item)
        {
            _buttonLeft.changeItemOverlay(item);
        }

        public HUDOPTIONS buttonLeftItem
        {
            get
            {
                return _buttonLeft.hudItem;
            }
        }

        public HUDOPTIONS buttonRightItem
        {
            get
            {
                return _buttonRight.hudItem;
            }
        }

        public void changeBottleContents(int bottleNum, HUDOPTIONS content)
        {
            switch (content)
            {
                case HUDOPTIONS.EMPTY_BOTTLE:
                    CMasterControl.buttonController.bottleRef[bottleNum].sprite = new Graphics.CSprite(Graphics.CTextures.HUD_EMPTY_BOTTLE);
                    CMasterControl.buttonController.bottleRef[bottleNum].MenuText = "Empty Bottle";
                    break;

                case HUDOPTIONS.BLUE_POTION:
                    CMasterControl.buttonController.bottleRef[bottleNum].sprite = new Graphics.CSprite(Graphics.CTextures.HUD_BLUE_POTION);
                    CMasterControl.buttonController.bottleRef[bottleNum].MenuText = "Blue Potion";
                    break;

                case HUDOPTIONS.GREEN_POTION:
                    CMasterControl.buttonController.bottleRef[bottleNum].sprite = new Graphics.CSprite(Graphics.CTextures.HUD_GREEN_POTION);
                    CMasterControl.buttonController.bottleRef[bottleNum].MenuText = "Green Potion";
                    break;

                case HUDOPTIONS.RED_POTION:
                    CMasterControl.buttonController.bottleRef[bottleNum].sprite = new Graphics.CSprite(Graphics.CTextures.HUD_RED_POTION);
                    CMasterControl.buttonController.bottleRef[bottleNum].MenuText = "Red Potion";
                    break;

                default:
                    throw new ArgumentException("Invalid bottle content added to bottle " + bottleNum);
            }

            CMasterControl.buttonController.bottleRef[bottleNum].hudOptions = content;
        }
    }
}
