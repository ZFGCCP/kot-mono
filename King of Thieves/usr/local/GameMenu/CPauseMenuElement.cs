using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Navigation;
using Microsoft.Xna.Framework;
using King_of_Thieves.Graphics;
using King_of_Thieves.Actors.HUD.buttons;

namespace King_of_Thieves.usr.local.GameMenu
{
    class CPauseMenuElement : MenuElement 
    {
        private int _rightNeighbor = _NO_NEIGHBOR;
        private int _upNeighbor = _NO_NEIGHBOR;
        private int _downNeighbor = _NO_NEIGHBOR;
        private int _leftNeighbor = _NO_NEIGHBOR;

        private const int _NO_NEIGHBOR = -1;
        private Vector2 _cursorPosition = Vector2.Zero;
        public CSprite sprite = null;
        public HUDOPTIONS hudOptions;

        public CPauseMenuElement(Vector2 cursorPosition, int rightNeighbor, int leftNeighbor, int upNeighbor, int downNeighbor) :
            base()
        {
            _rightNeighbor = rightNeighbor;
            _leftNeighbor = leftNeighbor;
            _upNeighbor = upNeighbor;
            _downNeighbor = downNeighbor;
            _cursorPosition = cursorPosition;

        }

        public bool hasItem
        {
            get
            {
                return sprite != null;
            }
        }

        public Vector2 cursorPosition
        {
            get
            {
                return _cursorPosition;
            }
        }

        public int rightNeighbor
        {
            get
            {
                return _rightNeighbor;
            }
        }

        public int leftNeighbor
        {
            get
            {
                return _leftNeighbor;
            }
        }

        public int upNeighbor
        {
            get
            {
                return _upNeighbor;
            }
        }

        public int downNeighbor
        {
            get
            {
                return _downNeighbor;
            }
        }
    }
}
