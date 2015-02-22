using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Items.Drops
{
    class CRupeeDrop : CDroppable
    {
        private const string _RUPEE_GREEN = "rupeeGreen";
        private static sbyte _heartCount = 0;

        private int _value = 0;

        public CRupeeDrop()
        {

        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            switch (additional[0])
            {
                case "G":
                    _value = 1;
                    
                    break;

                default:
                    break;
            }
        }
    }
}
