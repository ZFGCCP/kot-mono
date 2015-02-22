using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.HUD.counters
{
    class CRupeeCounter : CBaseCounter
    {
        private const int _RUPEE_WALLET_CAPACITY = 99;
        private const int _RUPEE_ADULT_CAPACITY = 500;
        private const int _RUPEE_GIANT_CAPACITY = 1000;

        public CRupeeCounter() 
            : base(99,0)
        {

        }
    }
}
