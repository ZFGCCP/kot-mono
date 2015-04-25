using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Cloud;
using King_of_Thieves.usr.local.GameMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        private Menu.CMenuCursor _cursor = null;
        private static SpriteFont _sherwood = CMasterControl.glblContent.Load<SpriteFont>(@"Fonts/pauseMenuHeadings");
        private static Vector2 _menuDrawText = new Vector2(40, 10);
        private bool _focused = false;
        private bool _drawHud = false;

        public CPauseBackdrop(string background,int menuPosition,Gears.Navigation.Menu menu, bool drawHud = false) :
            base()
        {
            _shiftIndex = menuPosition;

            if (_shiftIndex == -1)
                _fixedPosition.X = -320;
            else if (_shiftIndex == 0)
            {
                _fixedPosition.X = 0;
                _focused = true;
            }
            else if (_shiftIndex == 1)
                _fixedPosition.X = 320;

            _imageIndex.Add(background,new Graphics.CSprite(background));

            swapImage(background);
            _state = ACTOR_STATES.IDLE;
            _menu = menu;
            _drawHud = drawHud;
            _cursor = new CMenuCursor(((CPauseMenuElement)_menu.GetSelectableMenuElements()[0]).cursorPosition);

           
        }

        public override void drawMe(bool useOverlay = false)
        {
            base.drawMe(useOverlay);

            if (_focused)
            {
                foreach (CPauseMenuElement menuElement in _menu.MenuElements)
                {
                    Vector2 coords = menuElement.cursorPosition;

                    if (menuElement.hasItem)
                        menuElement.sprite.draw((int)(_position.X + coords.X), (int)(_position.Y + coords.Y));
                }
                _cursor.drawMe();

                if (_menu.GetActiveMenuIndex() != -1 && ((CPauseMenuElement)_menu.MenuElements[_menu.GetActiveMenuIndex()]).hasItem)
                    Graphics.CGraphics.spriteBatch.DrawString(_sherwood, _menu.MenuElements[_menu.GetActiveMenuIndex()].MenuText, this._position + _menuDrawText, Color.White);

                if (_drawHud)
                    CMasterControl.buttonController.drawMe(Graphics.CGraphics.spriteBatch);
            }


        }

        private void _moveCursor(int neighbor)
        {
            if (neighbor != -1)
            {
                _menu.SetActiveMenuIndex(neighbor);
                CPauseMenuElement newElement = (CPauseMenuElement)_menu.MenuElements[_menu.GetActiveMenuIndex()];
                _cursor.fixedPosition = newElement.cursorPosition;
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["menu:moveCursor"]);
            }
        }

        private void _setLeftItem()
        {
            HUD.buttons.HUDOPTIONS item;
            int cursorLocation = _menu.GetActiveMenuIndex();

            if (((CPauseMenuElement)_menu.MenuElements[cursorLocation]).hasItem)
            {
                CPauseMenuElement element = ((CPauseMenuElement)_menu.MenuElements[cursorLocation]);
                item = element.hudOptions;
                CMasterControl.buttonController.switchLeftItem(item);
            }
        }

        private void _setRightItem()
        {
            HUD.buttons.HUDOPTIONS item;
            int cursorLocation = _menu.GetActiveMenuIndex();

            if (((CPauseMenuElement)_menu.MenuElements[cursorLocation]).hasItem)
            {
                item = ((CPauseMenuElement)_menu.MenuElements[cursorLocation]).hudOptions;
                CMasterControl.buttonController.switchRightItem(item);
            }
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
                else if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.Left))
                {
                    _setLeftItem();
                }
                else if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.D) && _focused)
                {
                    int activeMenuIndex = _menu.GetActiveMenuIndex();
                    if (activeMenuIndex >= 0)
                    {
                        CPauseMenuElement currentElement = (CPauseMenuElement)_menu.MenuElements[activeMenuIndex];
                        _moveCursor(currentElement.rightNeighbor);
                    }
                }
                else if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.A) && _focused)
                {
                    int activeMenuIndex = _menu.GetActiveMenuIndex();
                    if (activeMenuIndex >= 0)
                    {
                        CPauseMenuElement currentElement = (CPauseMenuElement)_menu.MenuElements[activeMenuIndex];
                        _moveCursor(currentElement.leftNeighbor);
                    }
                }
                else if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.S) && _focused)
                {
                    int activeMenuIndex = _menu.GetActiveMenuIndex();
                    if (activeMenuIndex >= 0)
                    {
                        CPauseMenuElement currentElement = (CPauseMenuElement)_menu.MenuElements[activeMenuIndex];
                        _moveCursor(currentElement.downNeighbor);
                    }
                }
                else if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.W) && _focused)
                {
                    int activeMenuIndex = _menu.GetActiveMenuIndex();
                    if (activeMenuIndex >= 0)
                    {
                        CPauseMenuElement currentElement = (CPauseMenuElement)_menu.MenuElements[activeMenuIndex];
                        _moveCursor(currentElement.upNeighbor);
                    }
                }
            }
        }

        private void _shiftLeft()
        {
            if (_shiftIndex == -1)
                _position.X = 639;

            _velocity.X = -_SHIFT_VELOCITY;
            _beginShift(ACTOR_STATES.SHIFT_LEFT);
            _shiftIndex--;
            _checkIfFocused();
        }

        private void _shiftRight()
        {
            if (_shiftIndex == 1)
                _position.X = -640;

            _velocity.X = _SHIFT_VELOCITY;
            _beginShift(ACTOR_STATES.SHIFT_RIGHT);
            _shiftIndex++;
            _checkIfFocused();
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

        private void _checkIfFocused()
        {
            if (_shiftIndex == 0)
                _focused = true;
            else
                _focused = false;
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
