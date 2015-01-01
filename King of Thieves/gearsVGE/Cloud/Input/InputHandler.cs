using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gears.Cloud.Input
{
    public abstract class InputHandler
    {
        public delegate void KeyboardStateEvent(ref KeyboardState CURRENT_KEYBOARD_STATE, ref KeyboardState OLD_KEYBOARD_STATE);
        public delegate void MouseStateEvent(ref MouseState CURRENT_MOUSE_STATE, ref MouseState OLD_MOUSE_STATE);
        public delegate void GamePadStateEvent(ref GamePadState CURRENT_GAMEPAD_STATE, ref GamePadState OLD_GAMEPAD_STATE);

        protected event KeyboardStateEvent keyboardEventList;
        protected event MouseStateEvent mouseEventList;
        protected event GamePadStateEvent gamePadEventList;

        protected KeyboardState _oldKeyboardState;
        protected KeyboardState _currentKeyboardState;

        protected MouseState _oldMouseState;
        protected MouseState _currentMouseState;

        protected GamePadState _oldGamePadState;
        protected GamePadState _currentGamePadState;

        public virtual void Update(GameTime gameTime)
        {
            if (keyboardEventList != null)
            {
                keyboardEventList(ref _currentKeyboardState, ref _oldKeyboardState);
            }
            if (mouseEventList != null)
            {
                mouseEventList(ref _currentMouseState, ref _oldMouseState);
            }
            if (gamePadEventList != null)
            {
                gamePadEventList(ref _currentGamePadState, ref _oldGamePadState);
            }
        }
        public void ClearEventHandler()
        {
            if (keyboardEventList != null)
            {
                foreach (KeyboardStateEvent e in keyboardEventList.GetInvocationList())
                {
                    keyboardEventList -= e;
                }
            }

            if (mouseEventList != null)
            {
                foreach (MouseStateEvent e in mouseEventList.GetInvocationList())
                {
                    mouseEventList -= e;
                }
            }

            if (gamePadEventList != null)
            {
                foreach (GamePadStateEvent e in gamePadEventList.GetInvocationList())
                {
                    gamePadEventList -= e;
                }
            }
            //keyboardEventList = null;
            //mouseEventList = null;
            //gamePadEventList = null;
        }
        public void SubscribeInputHook(KeyboardStateEvent kse)
        {
            keyboardEventList += kse;
        }
        public void SubscribeInputHook(MouseStateEvent mse)
        {
            mouseEventList += mse;
        }
        public void SubscribeInputHook(GamePadStateEvent gpse)
        {
            gamePadEventList += gpse;
        }

        public void UnsubscribeInputHook(KeyboardStateEvent kse)
        {
            if (keyboardEventList.GetInvocationList().Contains(kse))
            {
                keyboardEventList -= kse;
            }
        }
        public void UnsubscribeInputHook(MouseStateEvent mse)
        {
            if (mouseEventList.GetInvocationList().Contains(mse))
            {
                mouseEventList -= mse;
            }
        }
        public void UnsubscribeInputHook(GamePadStateEvent gpse)
        {
            if (gamePadEventList.GetInvocationList().Contains(gpse))
            {
                gamePadEventList -= gpse;
            }
        }

    }
}
