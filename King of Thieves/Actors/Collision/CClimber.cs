using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Collision
{
    class CClimber : CCollidable
    {
        public CClimber()
        {

        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            CClimberFloor floor = new CClimberFloor();
            CClimberLanding landing = new CClimberLanding();

            floor.init(name + "Floor", new Vector2(position.X, position.Y + height), "", compAddress, additional);
            landing.init(name + "Landing", new Vector2(position.X, position.Y - 8), "", compAddress, additional);

            //_queueActorForRegistration(floor);
            //_queueActorForRegistration(landing);
        }


    }
}
