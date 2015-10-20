using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.KotException
{
    class KotInvalidActorException : System.Exception
    {
        public KotInvalidActorException(string message) :
            base(message)
        {
            
        }
    }
}
