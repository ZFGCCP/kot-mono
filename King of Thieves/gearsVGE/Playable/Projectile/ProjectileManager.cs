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
    //these enums need to be organized into a separate file at some point and refactored
    enum ProjectileType
    {
        Capsule,
        Circle,
        Ovular
    }

    enum PlayerType
    {
        Enemy,
        Player1,
        Player2,
        Player3,
        Player4,
        Neutral,
        Chaos
    }
    //This class is scheduled to become abstract to support polymorphic behavior.
    class ProjectileManager
    {
        private LinkedList<Projectile> projectiles;



        //debug -- remove 
        private Texture2D _textureCapsule;
        private Texture2D _textureCircle;
        private Texture2D _textureOval;
        private Vector2 _positionCapsule = new Vector2(300.0f, 400.0f);
        private Vector2 _positionCircle = new Vector2(250.0f, 300.0f);
        private Vector2 _positionOval = new Vector2(180.0f, 200.0f);
        private Color _colorCapsule = Color.LightBlue;
        private Color _colorCircle = Color.Tomato;
        private Color _colorOval = Color.GreenYellow;
        private float _rotation = 0.0f;
        private float _scale = 0.25f;
        private float _depth = 0.0f;


        public ProjectileManager()
        {
            Initialize();
        }
        private void Initialize()
        {
            projectiles = new LinkedList<Projectile>();
        }
        public void LoadContent(Game game, string parentDir)
        {
            //load all projectile's contents
        }
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void CreateProjectile(ProjectileType type, PlayerType owner, Vector2 origin, Vector2 direction)
        {

        }
    }
}
