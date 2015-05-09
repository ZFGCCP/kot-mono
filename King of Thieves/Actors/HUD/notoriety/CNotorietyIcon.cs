using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.HUD.notoriety
{
    enum NOTORIETY_LEVEL
    {
        NONE,
        MEDIUM,
        HIGH
    }

    class CNotorietyIcon : CHUDElement
    {
        private NOTORIETY_LEVEL _notorietyLevel = NOTORIETY_LEVEL.NONE;

        public CNotorietyIcon()
        {
            _fixedPosition.X = 10;
            _fixedPosition.Y = 200;
        }

        public void resetNotoriety()
        {
            _notorietyLevel = NOTORIETY_LEVEL.NONE;
        }

        public void raiseNotoriety()
        {
            int level = (int)_notorietyLevel;
            level += 1;
            _notorietyLevel = (NOTORIETY_LEVEL)level;
        }

        public void lowerNotoriety()
        {
            int level = (int)_notorietyLevel;
            level -= 1;
            _notorietyLevel = (NOTORIETY_LEVEL)level;
        }

        public NOTORIETY_LEVEL notorietyLevel
        {
            get
            {
                return _notorietyLevel;
            }
        }
    }
}
