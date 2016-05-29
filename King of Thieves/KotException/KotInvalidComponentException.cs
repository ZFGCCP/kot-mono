using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.KotException
{
    class KotInvalidComponentException : System.Exception
    {
        public KotInvalidComponentException(int componentAddress) : 
            base("Invalid component at address: " + componentAddress)
        { }
    }
}
