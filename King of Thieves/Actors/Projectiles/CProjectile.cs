using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Projectiles
{
    class CProjectile : CActor
    {
        protected static readonly string PROJ_LEFT = "projLeft";
        protected static readonly string PROJ_RIGHT = "projRight";
        protected static readonly string PROJ_UP = "projUp";
        protected static readonly string PROJ_DOWN = "projDown";
        protected int _damage = 0;

        public CProjectile(DIRECTION direction, Vector2 velocity, Vector2 position) :
            base()
        {
            _direction = direction;
            _velocity = velocity;
            _position = position;
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            _position += _velocity;
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CPlayer));
        }

        protected virtual void shoot()
        {
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(PROJ_DOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(PROJ_UP);
                    break;

                case DIRECTION.LEFT:
                    swapImage(PROJ_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(PROJ_RIGHT);
                    break;
            }
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            _killMe = true;
        }
    }
}
