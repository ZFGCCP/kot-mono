using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors
{
    public class CCommNetRef
    {
        public readonly string actorName;
        public readonly int componentAddress;

        public CCommNetRef(int componentAddress, string actorName)
        {
            this.componentAddress = componentAddress;
            this.actorName = actorName;
        }

    }
}
