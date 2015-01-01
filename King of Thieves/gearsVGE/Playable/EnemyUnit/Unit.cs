using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

using Gears.Cloud;
using Gears.Cloud.Events;
using Gears.Cloud._Debug;


namespace Gears.Playable
{
    /// <summary>
    /// TODO: Comments. Clean up. Refactor.
    /// </summary>
    public abstract class Unit : Entity
    {
        protected internal BoundingBox _boundingBox;
        protected internal Vector3[] _transformedPoints;

        protected internal Vector2 _position;
        protected internal Vector2 _imageOrigin;
        protected internal Color _color;
        protected internal float _rotation;
        protected internal float _scale = 1.0f; //may change!!!!! function it
        protected internal float _depth = 0.0f; //may change!!!!! function it

        protected internal string _textureFileName;
        protected internal Texture2D _texture;

        protected internal string _texFileLoc = null;
        protected abstract string TextureFileLocation { get; }

        /// <summary>
        /// This defaulted constructor is for debugging/testing purposes only.
        /// It is required in a regular game to use a parameterized constructor instead.
        /// </summary>
        public Unit() : this(Vector2.Zero, Color.White, 0.0f/*, "example"*/) { }
        //TODO: make unitparameters the primary constructor
        internal Unit(UnitParameters up) : this(up.Origin, up.Color, up.Rotation/*, up.TextureFileName*/) { }
        //TODO: make this constructor an alias constructor
        public Unit(Vector2 startingPosition, Color color, float rotation/*, string textureFileName*/) 
            : this(startingPosition, color, rotation, Vector2.Zero) { }
        //_texture, _position, _color, _rotation, Vector2.Zero, _scale, SpriteEffects.None, _depth
        public Unit(Vector2 startingPosition, Color color, float rotation, Vector2 imageOrigin/*, string textureFileName*/)
        {
            _position = startingPosition;
            _color = color;
            _rotation = rotation;
            _imageOrigin = imageOrigin;
            _textureFileName = TextureFileLocation;

            Initialize();
        }

        private void Initialize()
        {
            LoadContent();
        }
        private void LoadContent()
        {
            try
            {
                if (TextureFileLocation != null)
                {
                    _texture = Master.GetGame().Content.Load<Texture2D>(TextureFileLocation);
                    this.CalculateBoundingBox();
                }
                else
                {
                    HandleTextureFileLocationError(true);
                }
            }
            catch
            {
                HandleTextureFileLocationError(false);
            }
        }

        //THIS NEEDS TO BE TESTED OUT
        protected void CalculateBoundingBox()
        {
            Matrix rotMatrix = Matrix.CreateRotationZ(this._rotation);
            Matrix scaleMatrix = Matrix.CreateScale(this._scale);
            Matrix translationMatrix = Matrix.CreateTranslation(this._position.X, this._position.Y, 0);

            Matrix finalMatrix = rotMatrix * scaleMatrix * translationMatrix;

            int texheight = _texture.Height;
            int texwidth = _texture.Width;
            
            Vector2 zeroZero = new Vector2(_position.X,_position.Y);
            Vector2 zeroOne = new Vector2(_position.X + texwidth,_position.Y);
            Vector2 oneZero = new Vector2(_position.X, _position.Y + texheight);
            Vector2 oneOne = new Vector2(_position.X + texwidth,_position.Y + texheight);

            Vector2 transformed_zeroZero = Vector2.Transform(zeroZero, finalMatrix);
            Vector2 transformed_zeroOne = Vector2.Transform(zeroOne, finalMatrix);
            Vector2 transformed_oneZero = Vector2.Transform(oneZero, finalMatrix);
            Vector2 transformed_oneOne = Vector2.Transform(oneOne, finalMatrix);

            _transformedPoints = null;
            _transformedPoints = new Vector3[4];
            _transformedPoints[0] = new Vector3(transformed_zeroZero, 0);
            _transformedPoints[1] = new Vector3(transformed_zeroOne, 0);
            _transformedPoints[2] = new Vector3(transformed_oneZero, 0);
            _transformedPoints[3] = new Vector3(transformed_oneOne, 0);

            _boundingBox = BoundingBox.CreateFromPoints(_transformedPoints);
        }

        private void HandleTextureFileLocationError(bool throwException)
        {
            string __ERROR = "DEV.ERROR##Unit::TextureFileLocation not set properly.\n\t[" + TextureFileLocation + "]";
            Debug.Out(__ERROR);
            if (throwException)
            {
                throw new Exception(__ERROR);
            }
        }
        //NOTE:  The inherited function must call this at the end or beginning of it's definition.
        internal virtual void CheckUpdateEvents(GameTime gameTime)
        {
            if (CGlobalEvents.GFrameTrigger.getEvent(0).triggered == true)
            {
                onUpdate(gameTime);
            }
            //I hope it's obvious that this will be a bunch of if statements to check each event :)
        }
        //NOTE:  The inherited function must call this at the end or beginning of it's definition.
        internal virtual void CheckDrawEvents(SpriteBatch spriteBatch)
        {
            //if (CGlobalEvents.GFrameTrigger.getEvent(0).triggered == true)
            //{
                onDraw(spriteBatch);
            //}
            //I hope it's obvious that this will be a bunch of if statements to check each event :)
        }

        //NOTE:  The inherited function must call this at the end of it's definition.  
        //This is so base events are checked
        public virtual void Update(GameTime gameTime)
        {
            //movement and such here
            CheckUpdateEvents(gameTime);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            CheckDrawEvents(spriteBatch);
            spriteBatch.Draw(_texture, _position, null, _color, _rotation, _imageOrigin, _scale, SpriteEffects.None, _depth);
        }

        //event handlers
        //the prototypes for various events are declared here.
        //Define them in the specific entity you want that event to act on
        //Also note, I can't implement the other events yet since no interface for their associated functionality exists in a concrete form yet.

        //TODO: Make an interface which holds these prototypes 
        public virtual void onUpdate(GameTime gameTime) { } //global event
        public virtual void onDraw(SpriteBatch spriteBatch) { } //global event'

        public virtual void onAnimEnd() { }//local event
        public virtual void onAnimStart() { }//local event
        public virtual void onMapStart() { }//global event //onLoad  ? -> global Trigger
        public virtual void onMapEnd() { }//global event   //onUnload? -> global Trigger
    }
}
