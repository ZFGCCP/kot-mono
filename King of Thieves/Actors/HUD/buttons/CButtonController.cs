using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.HUD.buttons
{
    class CButtonController
    {
        private CButton _buttonLeft = new CButton(CButton.HUD_BUTTON_TYPE.LEFT);
        private CButton _buttonRight = new CButton(CButton.HUD_BUTTON_TYPE.RIGHT);
        private CButton _buttonAction = new CButton(CButton.HUD_BUTTON_TYPE.ACTION);
        private counters.CRupeeCounter _rupeeCounter = new counters.CRupeeCounter();
        private Actors.HUD.Text.CTextBox _textBoxController = new Text.CTextBox();

        public void createTextBox(string message)
        {
            _textBoxController.displayMessageBox(message);
        }

        public void update(GameTime gameTime)
        {
            _buttonRight.update(gameTime);
            _buttonLeft.update(gameTime);
            _buttonAction.update(gameTime);
            _rupeeCounter.update(gameTime);
            _textBoxController.update(gameTime);
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

        public void drawMe(SpriteBatch spriteBatch)
        {
            _buttonLeft.drawMe();
            _buttonRight.drawMe();
            _buttonAction.drawMe();
            _rupeeCounter.drawMe();
            _textBoxController.drawMe();
        }

        public void switchRightItem(HUDOPTIONS item)
        {
            _buttonRight.changeItemOverlay(item);
        }

        public void switchLeftItem(HUDOPTIONS item)
        {
            _buttonRight.changeItemOverlay(item);
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
    }
}
