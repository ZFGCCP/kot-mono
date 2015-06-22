using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Items.weapons
{
    class CShadowMedallion : CActor
    {
        private static bool _isActive = false;
        private bool _cloneExists = false;

        public CShadowMedallion() :
            base()
        {

        }

        public static bool isActive
        {
            get
            {
                return _isActive;
            }
        }

        private void _createPlayerClone()
        {
            _cloneExists = true;
        }
    }
}
