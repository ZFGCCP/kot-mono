using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gears.Cloud.Collisions
{

    enum shapeType
    {
        circle = 0,
        box
    }

    abstract class CHitShape
    {

        protected shapeType _shape;
        public Vector2 position;

        public abstract bool contains(CHitShape otherShape);
        protected abstract void _scale(int percentage);

        protected CHitShape(shapeType shape, Vector2 position)
        {
            _shape = shape;
            this.position = position;
        }

        public shapeType shape
        {
            get
            {
                return _shape;
            }
        }

        protected void _translate(Vector2 distance)
        {
            position += distance;
        }

        public void transform(int scale, Vector2 distance)
        {
            _scale(scale);
            _translate(distance);
        }
  
    }
}
