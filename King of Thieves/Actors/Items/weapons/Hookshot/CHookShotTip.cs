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
            CMasterControl.commNet[componentAddress].Add(new CActorPacket(0, "hookshotChain0", this));
            CMasterControl.commNet[componentAddress].Add(new CActorPacket(0, "hookshotChain1", this));
        }

        public override void initChildren()
        {
            //create 2 chain pieces
            CHookShotPiece[] _chain = new CHookShotPiece[2];
            for (int i = 0; i < 2; i++)
            {
                _chain[i] = new CHookShotPiece(velocity / (2 + i*2), direction);
                _chain[i].init("hookshotChain" + i, position, "", CReservedAddresses.NON_ASSIGNED);
                this.component.addActor(_chain[i], "hookshotChain" + i);
            }
        }

        public override void timer0(object sender)
        {
            if (_state != ACTOR_STATES.EXTEND)
                CMasterControl.commNet[CReservedAddresses.PLAYER].Add(new CActorPacket(99, "player", this));

            base.timer0(sender);
        }
    }
}
