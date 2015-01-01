using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Gears.Cloud;

namespace King_of_Thieves.Actors.Menu
{
    //This class is the menu background itself
    class CMenu : CActor
    {
        private int _displayTime;
        private Sound.CSound _bgm = null;
        private Sound.CSound _itemSwitch = null;
        private Sound.CSound _itemSelect = null;
        private TimeSpan _startTime;
        private int _menuIndex = 0;
        private int _numberOfItems = 0;

        public CMenu(string name, Graphics.CSprite image, int displayTime, Sound.CSound BGM = null, Sound.CSound itemSwitch = null, Sound.CSound itemSelect = null):base()
        {
            base.image = image;
            _displayTime = displayTime;
            _bgm = BGM;
            _itemSelect = itemSelect;
            _itemSwitch = itemSwitch;
            _position = new Vector2(18, 18);
            _name = name;

            if (_bgm != null)
                CMasterControl.audioPlayer.addSfx(_bgm);
            
        }

        public void registerItem()
        {
            _numberOfItems++;
        }

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }

        public int numberofItems
        {
            get
            {
                return _numberOfItems;
            }
        }

        public override void animationEnd(object sender)
        {
            throw new NotImplementedException();
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }

        public override void create(object sender)
        {
            
        }

        public override void destroy(object sender)
        {
            throw new NotImplementedException();
        }

        public override void draw(object sender)
        {
            throw new NotImplementedException();
        }

        public override void frame(object sender)
        {
            //if ((_gameTime.ElapsedGameTime - _startTime).CompareTo(new TimeSpan(0, 0, _displayTime)) >= 0)
            //    destroy(this);
        }

        public override void keyDown(object sender)
        {


            if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Keys.Down))
            {
                if (++_menuIndex >= _numberOfItems)
                    _menuIndex = 0;

                if (_itemSwitch != null)
                    CMasterControl.audioPlayer.addSfx(_itemSwitch);
            }
            else if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Keys.Up))
            {
                if (--_menuIndex >= _numberOfItems)
                    _menuIndex = _numberOfItems - 1;

                if (_itemSwitch != null)
                    CMasterControl.audioPlayer.addSfx(_itemSwitch);
            }

            else if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).keysPressed.Contains(Keys.Enter))
            {
                CMasterControl.audioPlayer.addSfx(_itemSelect);
            }
            
        }

        public override void keyRelease(object sender)
        {
            throw new NotImplementedException();
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            
            
        }
    }
}
