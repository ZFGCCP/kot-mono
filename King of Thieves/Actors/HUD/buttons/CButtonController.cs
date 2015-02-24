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
        private counters.CRupeeCounter _rupeeCounter = new counters.CRupeeCounter();

        public void update(GameTime gameTime)
        {
            _buttonRight.update(gameTime);
            _buttonLeft.update(gameTime);
            _rupeeCounter.update(gameTime);
        }

        public void incrementRupees(int amount, bool instant = false)
        {
            _rupeeCounter.increment(amount, instant);
        }

        public void drawMe(SpriteBatch spriteBatch)
        {
            _buttonLeft.drawMe();
            _buttonRight.drawMe();
            _rupeeCounter.drawMe();
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
            set
            {
                _buttonRight.hudItem = value;
            }
        }
    }
}
