using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Items.weapons.Hookshot
{
    class CHookShotTip : CHookShotPiece
    {
        public CHookShotTip(Vector2 velocity, DIRECTION direction, Vector2 position) :
            base(velocity,direction)
        {
            //create 3 chain pieces
            CHookShotPiece[] _chain = new CHookShotPiece[2];

            for (int i = 0; i < 2; i++)
            {
                _chain[i] = new CHookShotPiece(velocity / (2 + i*2), direction);
                _chain[i].init("hookshotChain" + i, position, "", CReservedAddresses.NON_ASSIGNED);
                this.component.addActor(_chain[i], "hookshotChain" + i);
            }
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Actors.Collision.CSolidTile));
        }

        public override void collide(object sender, CActor collider)
        {
            int timeLeft = stopTimer0();
            _state = ACTOR_STATES.RETRACT;
            _retract(timeLeft);
        }

        protected override void _registerUserEvents()
        {
            
        }
    }
}
