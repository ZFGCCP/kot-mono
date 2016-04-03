using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.MathExt
{
    public class CTriangle
    {
        private Vector2 _A = Vector2.Zero;
        private Vector2 _B = Vector2.Zero;
        private Vector2 _C = Vector2.Zero;

        public CTriangle(Vector2 A, Vector2 B, Vector2 C)
        {
            _A = A;
            _B = B;
            _C = C;
        }

        public Vector2 A
        {
            get
            {
                return _A;
            }
        }

        public Vector2 B
        {
            get
            {
                return _B;
            }
        }

        public Vector2 C
        {
            get
            {
                return _C;
            }
        }
    }
}
