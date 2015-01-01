using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gears.Cloud;
using Microsoft.Xna.Framework;

namespace Gears.Navigation
{
    public abstract class MenuReadyGameState : GameState, IMenuItem
    {
        private string menuText;
        public string MenuText
        {
            get { return menuText; }
            set
            {
                //18 chars or less to fit release 2 of menu implementation
                if (value.Length <= 18)
                {
                    menuText = value;
                }
                else
                {
                    //do nothing currently
                }
            }
        }

        private bool itemColorSet = false;
        public bool ItemColorSet 
        { 
            get { return itemColorSet; } 
        }

        private Color itemColor;
        public Color ItemColor
        {
            get { return itemColor; }
            set
            {
                itemColorSet = true;
                itemColor = value;
            }
        }
        public virtual void ThrowPushEvent() { }
    }
}
