using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.KotException
{
    class KotBadArgumentException : System.Exception
    {
        public KotBadArgumentException(string message) :
            base(message)
        {
            
        }
    }
}
