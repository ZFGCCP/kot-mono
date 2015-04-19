using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Cloud;

namespace King_of_Thieves.Actors.Menu
{
    class CPauseBackdrop : HUD.CHUDElement
    {
        //-1: left
        // 0: center (focused)
        // 1: right
        private int _shiftIndex = 0;
        private const float _SHIFT_VELOCITY = 11.05f;
        private Gears.Navigation.Menu _menu;
        private Menu.CMenuCursor _cursor = new CMenuCursor();

        public CPauseBackdrop(string background,int menuPosition,Gears.Navigation.Menu menu) :
            base()
        {
            _shiftIndex = menuPosition;

            if (_shiftIndex == -1)
                _fixedPosition.X = -320;
            else if (_shiftIndex == 0)
                _fixedPosition.X = 0;
            else if (_shiftIndex == 1)
                _fixedPosition.X = 320;

            _imageIndex.Add(background,new Graphics.CSprite(background));

            swapImage(background);
            _state = ACTOR_STATES.IDLE;
            _menu = menu;
        }

        public override void drawMe(bool useOverlay = false)
        {
            base.drawMe(useOverlay);
            _cursor.drawMe();
        }

        public override void keyRelease(object sender)
        {
            Input.CInput input = Master.GetInputManager().GetCurrentInputHandler() as Input.CInput;

            if (_state == ACTOR_STATES.IDLE)
            {
                if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.Q))
                    _shiftLeft();
                else if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.E))
                    _shiftRight();
            }
        }

        private void _shiftLeft()
        {
            if (_shiftIndex == -1)
                _position.X = 639;

            _velocity.X = -_SHIFT_VELOCITY;
            _beginShift(ACTOR_STATES.SHIFT_LEFT);
            _shiftIndex--;
        }

        private void _shiftRight()
        {
            if (_shiftIndex == 1)
                _position.X = -640;

            _velocity.X = _SHIFT_VELOCITY;
            _beginShift(ACTOR_STATES.SHIFT_RIGHT);
            _shiftIndex++;
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            _state = ACTOR_STATES.IDLE;
            _velocity.X = 0;
        }

        private void _beginShift(ACTOR_STATES shift)
        {
            startTimer0(30);
            _state = shift;
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            _fixedPosition.X += _velocity.X;
            _cursor.update(gameTime);
        }

        public int shiftIndex
        {
            get
            {
                return _shiftIndex;
            }
        }

    }
}
