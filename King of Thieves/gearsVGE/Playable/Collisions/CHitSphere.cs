using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gears.Cloud.Collisions
{
    class CHitSphere : CHitShape
    {
        public float radius;

        public CHitSphere(float radius, Vector2 origin) :
            base(shapeType.circle, origin)
        {
            this.radius = radius;
        }

        public override bool contains(CHitShape otherShape)
        {
            switch (otherShape.shape)
            {
                case shapeType.circle:

                    if ( (Math.Sqrt(Math.Pow(position.X - otherShape.position.X,2) + Math.Pow(position.Y - otherShape.position.Y,2))) <
                        radius + ((CHitSphere)otherShape).radius)
                    {
                        return true;
                    }
                    break;

                case shapeType.box:
                    break;
            }

            return false;
        }

        protected override void _scale(int percentage)
        {
            float percentAsDec = percentage * .01f;

            radius *= percentAsDec;
        }
    }
}
