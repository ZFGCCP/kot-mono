using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Microsoft.Xna.Framework.Storage;

using Gears.Cloud._Debug;
using Gears.Cloud.Input;

namespace Gears.Cloud.Input
{
    public class DefaultInput : InputHandler
    {
        private static bool _enabled = false;

        internal KeyboardState OldKeyboardState
        {
            get { return _oldKeyboardState; }
        }
        internal KeyboardState CurrentKeyboardState
        {
            get { return _currentKeyboardState; }
        }

        public bool GetInputFlag()
        {
            return _enabled;
        }
        public void EnableInput()
        {
            _enabled = true;
        }
        public void DisableInput()
        {
            _enabled = false;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateKeyboardStates();

            base.Update(gameTime);
        }

        private void UpdateKeyboardStates()
        {
            _oldKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
        }

    }
}
