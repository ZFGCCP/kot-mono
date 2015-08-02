using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Items.Drops
{
    class CFireHydrant : CDroppable
    {

        public CFireHydrant() :
            base()
        {
            _capacity = 0;
            _yieldMessage = "You got a...! Fire hydrant..? You should bring it to the dog, you promised after all.";
            _name = "hydrant";
        }

        protected override void _yieldToPlayer(bool fromChest = false)
        {
            base._yieldToPlayer(true);
        }
    }
}
