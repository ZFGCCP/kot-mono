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
            _collidables.Add(typeof(Actors.Collision.CHookShotTarget));
        }

        public override void create(object sender)
        {
            base.create(sender);
        }

        public override void collide(object sender, CActor collider)
        {
            int timeLeft = 60 - stopTimer0();
            if (collider is Actors.Collision.CSolidTile)
            {
                _state = ACTOR_STATES.RETRACT;
                _retract(timeLeft);
                CMasterControl.commNet[componentAddress].Add(new CActorPacket(0, "hookshotChain0", this, timeLeft));
                CMasterControl.commNet[componentAddress].Add(new CActorPacket(0, "hookshotChain1", this, timeLeft));
            }
            else if(collider is Actors.Collision.CHookShotTarget)
            {
                _state = ACTOR_STATES.PULLING;
                _retract(timeLeft);
                _velocity = Vector2.Zero;
                float velocityFactorOne = .25f;
                float velocityFactorTwo = .5f;
                CMasterControl.commNet[componentAddress].Add(new CActorPacket(1, "hookshotChain0", this, timeLeft, velocityFactorOne));
                CMasterControl.commNet[componentAddress].Add(new CActorPacket(1, "hookshotChain1", this, timeLeft, velocityFactorTwo));
            }
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

            _position.Y += 10;
        }

        public override void timer0(object sender)
        {
            if (_state != ACTOR_STATES.EXTEND)
                CMasterControl.commNet[CReservedAddresses.PLAYER].Add(new CActorPacket(99, "player", this));

            base.timer0(sender);
        }
    }
}
