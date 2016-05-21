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
            _hitBox = new Collision.CHitBox(this, 0, 0, 16, 16);

            _imageIndex.Add(Graphics.CTextures.EFFECT_HOOKSHOT_TIP_UP, new Graphics.CSprite(Graphics.CTextures.EFFECT_HOOKSHOT_TIP_UP));
            _imageIndex.Add(Graphics.CTextures.EFFECT_HOOKSHOT_TIP_DOWN, new Graphics.CSprite(Graphics.CTextures.EFFECT_HOOKSHOT_TIP_UP, false, true));
            _imageIndex.Add(Graphics.CTextures.EFFECT_HOOKSHOT_TIP_RIGHT, new Graphics.CSprite(Graphics.CTextures.EFFECT_HOOKSHOT_TIP_RIGHT));
            _imageIndex.Add(Graphics.CTextures.EFFECT_HOOKSHOT_TIP_LEFT, new Graphics.CSprite(Graphics.CTextures.EFFECT_HOOKSHOT_TIP_RIGHT, true));

            _imageSwapBasedOnDirection(_direction, Graphics.CTextures.EFFECT_HOOKSHOT_TIP_UP,
                                                   Graphics.CTextures.EFFECT_HOOKSHOT_TIP_DOWN,
                                                   Graphics.CTextures.EFFECT_HOOKSHOT_TIP_LEFT,
                                                   Graphics.CTextures.EFFECT_HOOKSHOT_TIP_RIGHT);
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Actors.Collision.CSolidTile));
        }

        public override void collide(object sender, CActor collider)
        {
            int timeLeft = 60 - stopTimer0();
            _state = ACTOR_STATES.RETRACT;
            _retract(timeLeft);
            CMasterControl.commNet[componentAddress].Add(new CActorPacket(0, "hookshotChain0", this, timeLeft));
            CMasterControl.commNet[componentAddress].Add(new CActorPacket(0, "hookshotChain1", this, timeLeft));
        }

        public override void initChildren()
        {
            //create 2 chain pieces
            CHookShotPiece[] _chain = new CHookShotPiece[2];
            for (int i = 0; i < 2; i++)
            {
                
                _chain[i] = new CHookShotPiece(_velocity / (2 + i * 2) , direction);
                _chain[i].init("hookshotChain" + i, position, "", CReservedAddresses.NON_ASSIGNED);
                component.actors.Add("hookshotChain" + i,_chain[i]);
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
