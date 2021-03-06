﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.HUD.notoriety
{
    enum NOTORIETY_LEVEL
    {
        MEDIUM,
        HIGH
    }

    class CNotorietyIcon : CHUDElement
    {
        private static NOTORIETY_LEVEL _notorietyLevel = NOTORIETY_LEVEL.MEDIUM;

        public CNotorietyIcon()
        {
            _fixedPosition.X = 10;
            _fixedPosition.Y = 200;

            _imageIndex.Add(Graphics.CTextures.HUD_NOTORIETY_MEDIUM, new Graphics.CSprite(Graphics.CTextures.HUD_NOTORIETY_MEDIUM));
            swapImage(Graphics.CTextures.HUD_NOTORIETY_MEDIUM);
        }

        public static void resetNotoriety()
        {
            _notorietyLevel = NOTORIETY_LEVEL.MEDIUM;
        }

        public static void raiseNotoriety()
        {
            _notorietyLevel = NOTORIETY_LEVEL.HIGH;
        }

        public static NOTORIETY_LEVEL notorietyLevel
        {
            get
            {
                return _notorietyLevel;
            }
        }
    }
}
