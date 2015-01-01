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
using Gears.Cloud._Debug;

namespace Gears.Playable
{
    public abstract class Zone
    {
        //Managers
        private List<IManager> _managers = new List<IManager>();
        private UnitManager _uManager;
        private PlayerManager _pManager;

        //private ProjectileManager projectileManager;

        public Zone()
        {
            //Initialize();
        }

        //This is currently not called.
        //private void Initialize()
        //{
        //    //projectileManager = new ProjectileManager();
        //}
        //DEPRECATED -- but still might be used for the projectile manager until a change is made.
        //private void LoadContent(Game game, string parentDir)
        //{
        //    //?Load the content for each Manager in the Zone //THIS WILL CHANGE 
        //    //projectileManager.LoadContent(game, /*contentDir*/); //THIS CALL WILL BECOME INVALID
        //}
        public void Update(GameTime gameTime)
        {
            foreach (IManager manager in _managers)
            {
                manager.Update(gameTime);
            }

            //deprecated
            if (_pManager != null)
            {
                _pManager.Update(gameTime);
            }
            if (_uManager != null)
            {
                _uManager.Update(gameTime);
            }


            //Update Projectiles
            //projectileManager.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IManager manager in _managers)
            {
                manager.Draw(spriteBatch);
            }

            //deprecated
            if (_pManager != null)
            {
                _pManager.Draw(spriteBatch);
            }
            if (_uManager != null)
            {
                _uManager.Draw(spriteBatch);
            }

            //Draw Projectiles
            //projectileManager.Draw(spriteBatch);
        }
        //TODO: register should allow an IEnumerable
        protected internal void RegisterManager(IManager manager)
        {
            if (manager != null)
            {
                _managers.Add(manager);
            }
            else
            {
                Debug.Out("Null manager passed to Zone.RegisterManager(IManager).");
            }
        }

        /// <summary>
        /// TODO
        /// DEPRECATED, SLATED FOR REMOVAL NEXT CLEANUP
        /// </summary>
        /// <param name="manager"></param>
        protected internal void Register(UnitManager manager)
        {
            _uManager = manager;
            //Initialize();
        }

        ///// <summary>
        ///// DEPRECATED
        ///// </summary>
        ///// <param name="manager"></param>
        //protected internal void RegisterPlayerManager(PlayerManager manager)
        //{
        //    _pManager = manager;
        //}

    }
}
