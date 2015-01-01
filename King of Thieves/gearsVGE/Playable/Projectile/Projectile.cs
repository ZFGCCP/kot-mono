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

//IN DEVELOPMENT. NOT APPROVED FOR USE IN VERSION 0.1.0
namespace Gears.Playable._INTRANSIT
{
    //TODO: This class is scheduled to become abstract to support polymorphic behavior.
    class Projectile
    {

        //
        // Fields
        //
        
        // the texture for this particular projectile
        private Texture2D _texture;
        // the color of the projectile
        private Color _color;
        // the current position of the projectile
        private Vector2 _position;
        // the rotation of the projectile
        private float _rotation;
        // the scale of the projectile
        private float _scale;
        // the layer depth of the projectile
        private float _depth;
        // number of seconds the projectile will exist for
        private float _lifetime;

        /// <summary>
        /// Loads the graphical content for the Projectile.
        /// </summary>
        /// <param name="game">The game in which the content is being loaded for. Typically passed from the state above.</param>
        /// <param name="parentDir">The parent directory in which the projectile sits in. Typically passed from the ProjectileManager.</param>
        private void LoadContent(Game game, string parentDir)
        {
            _texture = game.Content.Load<Texture2D>(parentDir + @"");

            //debug -- remove
            //_textureCapsule = game.Content.Load<Texture2D>(parentDir + @"\Projectile\capsule");
            //_textureCircle = game.Content.Load<Texture2D>(parentDir + @"\Projectile\circle");
            //_textureOval = game.Content.Load<Texture2D>(parentDir + @"\Projectile\ovular");
        }
        /// <summary>
        /// Updates the status of the projectile.
        /// </summary>
        /// <param name="gameTime">
        /// Snapshot of the game timing values which can be used for
        /// time-based calculations and representations.
        /// </param>
        public void Update(GameTime gameTime)
        {
            //KINEMATICS
            //COLLISION
            //LIFETIME
        }
        /// <summary>
        /// Draws the projectile.
        /// </summary>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, _color, _rotation, Vector2.Zero, _scale, SpriteEffects.None, _depth);
        }
    }
}
