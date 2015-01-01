using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Microsoft.Xna.Framework.Storage;

namespace Gears.Playable
{
    public abstract class PlayerManager : IManager
    {
        protected Player[] _players;

        public PlayerManager() { }

        public PlayerManager(Player[] players)
        {
            if (players != null)
            {
                _players = players;

                //foreach (Player player in _players) //******* Note that this should perhaps happen a little later instead of immediate on constructor.
                //{
                //    player.LoadContent();
                //}
            }
            else
            {
                throw new NullReferenceException("Null Player[] object passed to Gears.Playable.PlayerManager. \nPlease make sure to properly initialize PlayerManager.");
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Player player in _players)
            {
                player.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Player player in _players)
            {
                player.Draw(spriteBatch);
            }
        }

        public virtual void Activate() { }
    }
}
