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

namespace Gears.Cloud
{
    //underscore values are dev/test environments.
    internal enum CollisionType
    {
        Basic,
        _Debug, //test environment
        Isometric,
        SidescrollingBySAT,
        SidescrollingByMatrix

    }
    internal class CollisionManager
    {
        //private SomeDataStructure forWorkingWith;
        private CollisionType ct; //currently implemented as dev variable. can be made more modular.
        private bool Running = false;


        internal CollisionManager()
        {
            ct = CollisionType._Debug; //defaulting to debug for now
        }

        internal void Update(GameTime gameTime)
        {
            if (Running)
            {
                ManageCollision(gameTime);
            }
        }

        private void ManageCollision(GameTime gameTime)
        {
            switch (ct)
            {
                case CollisionType._Debug:
                    break;
                default:
                    break;
            }
        }

        //RegisterObject(...);
        //UnregisterObject(...);
        //ClearAll();
        //RegisterZone(); //for the map
        //UnregisterZone();
        //CheckObject(...); //calculate if collision or not and returns parties involved.

        //ThrowCollisionResponseEvent(...); //great for units, and should be customizable. show this in macros.

        //example category macros for collision response events
        //ThrowMapCollisionResponseEvent(...); //
        //ThrowEnemyUnitCollisionResponseEvent(...);//
        //ThrowNeutralUnitCollisionResponseEvent(...);//
    }
}
