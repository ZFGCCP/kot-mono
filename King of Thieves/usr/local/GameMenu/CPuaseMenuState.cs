using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Navigation;
using Microsoft.Xna.Framework.Input;

namespace King_of_Thieves.usr.local.GameMenu
{
    class CPuaseMenuState : MenuState 
    {
        public CPuaseMenuState(Menu menu) :
            base(menu)
        {

        }

        protected override void KeyDown(ref Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, ref Microsoft.Xna.Framework.Input.KeyboardState oldKeyboardState)
        {
            int menuIndex = activeMenuIndex;

            if (currentKeyboardState.IsKeyDown(Keys.Down) &&
                currentKeyboardState.IsKeyDown(Keys.Down) != oldKeyboardState.IsKeyDown(Keys.Down))
            {
                
            }

        }
    }
}
