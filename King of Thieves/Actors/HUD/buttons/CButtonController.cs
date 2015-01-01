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

        public void update(GameTime gameTime)
        {
            _buttonRight.update(gameTime);
            _buttonLeft.update(gameTime);
        }

        public void drawMe(SpriteBatch spriteBatch)
        {
            _buttonLeft.drawMe();
            _buttonRight.drawMe();
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
