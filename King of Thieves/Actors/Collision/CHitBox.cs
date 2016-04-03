using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Graphics;
using System.Security.Cryptography;
using System.Text;

namespace King_of_Thieves.Actors.Collision
{
    public class CHitBox
    {
        private Vector2 _halfSize;
        private Vector2 _center;
        private Vector2 _previousCenter;
        private CActor actor; //Actor for this hitbox 
        private static RNGCryptoServiceProvider _cryptoRand = new RNGCryptoServiceProvider();
        private static char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVW XYZ1234567890".ToCharArray();
        private DIRECTION _collideDirection = DIRECTION.DOWN;

        private double _topLeftAngle = 0;
        private double _topRightAngle = 0;
        private double _bottomLeftAngle = 0;
        private double _bottomRightAngle = 0;

        private Vector2 _topLeft;
        private Vector2 _topRight;
        private Vector2 _bottomLeft;
        private Vector2 _bottomRight;

        //Texture for drawing hitbox
        Texture2D texture;

        public CHitBox(CActor actor, float offsetx, float offsety, float width, float height)
        {
            _halfSize = new Vector2(width * .5f, height * .5f);
            _center = new Vector2(offsetx+_halfSize.X, offsety+_halfSize.Y);
            this.actor = actor;

            _topLeftAngle = MathExt.MathExt.angle(_center, offset);
            _topRightAngle = MathExt.MathExt.angle(_center, new Vector2(offset.X + halfWidth * 2, offset.Y));
            _bottomLeftAngle = _topRightAngle + 180;
            _bottomRightAngle = _topLeftAngle + 180;

            _topLeft = new Vector2(_center.X - _halfSize.X, _center.Y - _halfSize.Y);
            _topRight = new Vector2(_center.X + _halfSize.X, _center.Y - _halfSize.Y);
            _bottomLeft = new Vector2(_center.X - _halfSize.X, _center.Y + _halfSize.Y);
            _bottomRight = new Vector2(_center.X + _halfSize.X, _center.Y + _halfSize.Y);

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
                distance = (point.X) - (offset.X);
            else
                distance = (point.X) - (actor.position.X);
            length = _halfSize.X * 2;

            if (distance < length && distance > -1)
            {

                if (actor == null)
                    distance = (point.Y) - (offset.Y);
                else
                    distance = (point.Y) - (actor.position.Y);

                length = _halfSize.Y * 2;

                return distance < length && distance > -1;
            }

            return false;
        }

        //specifies where the collider came from
        public void getCollisionDirection(CActor collider)
        {
            double angleBetween = MathExt.MathExt.angle(position, collider.hitBox.position);

            if (angleBetween >= _topRightAngle && angleBetween <= _topLeftAngle)
                _collideDirection = DIRECTION.UP;
            else if (angleBetween >= _topLeftAngle && angleBetween <= _bottomLeftAngle)
                _collideDirection = DIRECTION.LEFT;
            else if (angleBetween >= _bottomLeftAngle && angleBetween <= _bottomRightAngle)
                _collideDirection = DIRECTION.DOWN;
            else
                _collideDirection = DIRECTION.RIGHT;
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

        public Vector2 topLeft
        {
            get
            {
                return _topLeft;
            }
        }

        public Vector2 topRight
        {
            get
            {
                return _topRight;
            }
        }

        public Vector2 bottomLeft
        {
            get
            {
                return _bottomLeft;
            }
        }

        public Vector2 bottomRight
        {
            get
            {
                return _bottomRight;
            }
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

        public Vector2 position
        {
            get
            {
                return new Vector2(offset.X + _halfSize.X + actor.position.X, offset.Y + _halfSize.Y + actor.position.Y);
            }
        }

        public DIRECTION collideDirection
        {
            get
            {
                return _collideDirection;
            }
        }

        public static string produceRandomName()
        {
            byte[] data = new byte[16];
            _cryptoRand.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(16);

            foreach (byte b in data)
                result.Append(chars[b % (chars.Length - 1)]);

            return result.ToString();
        }
    }
}
