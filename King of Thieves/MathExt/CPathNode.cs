using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.MathExt
{
    public struct CPathNode
    {
        public readonly Vector2 position;
        public readonly double speed;

        public CPathNode(Vector2 position, double speed)
        {
            this.position = position;
            this.speed = speed;
        }
    }
}
