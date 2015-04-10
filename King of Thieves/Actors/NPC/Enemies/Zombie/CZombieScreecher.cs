using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.Zombie
{
    class CZombieScreecher : Projectiles.CProjectile
    {
        private const int _STUN_TIME = 120;

        public CZombieScreecher(DIRECTION direction, Vector2 velocity, Vector2 position) :
            base(direction, velocity, position)
        {
            _hitBox = new Collision.CHitBox(this, 0, 0, 20, 20);
            startTimer0(90);
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Player.CPlayer));
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            _killMe = true;
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);
            if (collider is Player.CPlayer)
            {
                _killMe = true;
                collider.stun(_STUN_TIME);
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Npc:redead:screech"]);
            }
        }
    }
}
