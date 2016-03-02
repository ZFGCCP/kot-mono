using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Collision.Water
{
    class CDeepWater : CCollidable
    {
        public CDeepWater() :
            base()
        {
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CPlayer));
            _collidables.Add(typeof(Player.CWaterPuddle));
        }

        public override void collide(object sender, CActor collider)
        {
            if(collider is Player.CPlayer)
            {
                if(CMasterControl.hasFlippers)
                {

                }
                else
                {
                    _drownPlayer(collider);
                }
            }
        }

        private void _drownPlayer(CActor player)
        {
            player.noCollide = true;
            player.state = ACTOR_STATES.DROWN;
            player.otherColliderDirection = _hitBox.collideDirection;
        }
    }
}
