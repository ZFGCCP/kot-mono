using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Actors.Projectiles
{
    class CIceBall : CProjectile
    {
        public CIceBall(DIRECTION direction, Vector2 velocity, Vector2 position) :
            base(direction, velocity, position)
        {
            name = "iceBallSmall";
            _imageIndex.Add(PROJ_DOWN, new Graphics.CSprite(Graphics.CTextures.EFFECT_ICE_BALL_SMALL));

            _hitBox = new Collision.CHitBox(this, 6, 9, 14, 16);

            shoot();
            startTimer0(60);
        }

        public override void drawMe(bool useOverlay = false, SpriteBatch spriteBatch = null)
        {
            base.drawMe(useOverlay);
        }

        protected override void shoot()
        {
            swapImage(PROJ_DOWN);
        }

        public override void collide(object sender, CActor collider)
        {
            _killMe = true;

            if (collider is Player.CPlayer)
            {
                if (!INVINCIBLE_STATES.Contains(collider.state))
                {
                    collider.freeze();
                    collider.dealDamange(2, collider);
                }
            }
        }

    }
}
