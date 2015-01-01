using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Graphics;

namespace King_of_Thieves.Actors.Collision
{
    public class CHitBox
    {
        private Vector2 _halfSize;
        private Vector2 _center;
        private CActor actor; //Actor for this hitbox 

        //Texture for drawing hitbox
        Texture2D texture;

        public CHitBox(CActor actor, float offsetx, float offsety, float width, float height)
        {
            _halfSize = new Vector2(width * .5f, height * .5f);
            _center = new Vector2(offsetx+_halfSize.X, offsety+_halfSize.Y);
            this.actor = actor;

            //Prepare texture for rendering hitbox when needed(Only done for the first hitbox, as they share the texture)
            if (texture == null)
            {
                //Make a single pixel with transparency that we later stretch to the size of our hitbox
                texture = new Texture2D(CGraphics.GPU, 1, 1, false, SurfaceFormat.Color);
                Color[] c = new Color[1];
                c[0] = Color.FromNonPremultiplied(255, 0, 0, 200);
                texture.SetData<Color>(c);
            }
        }

        public bool checkCollision(CActor sender)
        {
            
            float distance = 0;
            float length = 0;
            CHitBox otherBox = sender.hitBox;

            distance = Math.Abs((actor.position.X + _center.X) - (sender.position.X + otherBox._center.X));
            length = _halfSize.X + otherBox._halfSize.X;

            if (distance < length)
            {
                distance = Math.Abs((actor.position.Y + _center.Y) - (sender.position.Y + otherBox._center.Y));
                length = _halfSize.Y + otherBox._halfSize.Y;

                return distance < length;
            }

            return false;
        }

        public bool checkCollision(Vector2 point)
        {
            float distance = 0;
            float length = 0;


            if (actor == null)
                distance = Math.Abs((offset.X) - (point.X));
            else
                distance = Math.Abs((actor.position.X + _center.X) - (point.X));
            length = _halfSize.X * 2;

            if (distance < length)
            {

                if (actor == null)
                    distance = Math.Abs((offset.Y) - (point.Y));
                else
                    distance = Math.Abs((actor.position.Y + _center.Y) - (point.Y));

                length = _halfSize.Y * 2;

                return distance < length;
            }

            return false;
        }

        public void draw()
        {
            if (texture != null)
            {
                CGraphics.spriteBatch.Draw(texture, new Rectangle((int)(offset.X+actor.position.X), (int)(offset.Y+actor.position.Y),
                                            (int)_halfSize.X*2, (int)_halfSize.Y*2), null, Color.White);
            }
        }

        public void destroy()
        {
            actor = null;
        }

        public float halfWidth
        {
            get
            {
                return _halfSize.X;
            }
        }

        public float halfHeight
        {
            get
            {
                return _halfSize.Y;
            }
        }

        public Vector2 center
        {
            get
            {
                return _center;
            }
        }

        public Vector2 size
        {
            get
            {
                return _halfSize * 2;
            }
            set
            {
                //center - halfsize = offset
                _center.X = (_center.X - _halfSize.X) + value.X;
                _center.Y = (_center.Y - _halfSize.Y) + value.Y;
                _halfSize = value * .5f;
            }
        }

        public Vector2 offset
        {
            get
            {
                return new Vector2(_center.X-_halfSize.X, _center.Y-_halfSize.Y);
            }
            set
            {
                _center.X = _halfSize.X + value.X;
                _center.Y = _halfSize.Y + value.Y;
            }
        }
    }
}
