using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Projectiles
{
    class CBomb : CProjectile
    {
        private static int _bombCount = 0;
        private static string _TICK = "bombTick";
        private static string _FAST_TICK = "bombFastTick";
        private static string _EXPLOSION = "explosion";

        public CBomb(DIRECTION direction, Vector2 velocity, Vector2 position, int damage) :
            base(direction, velocity, position)
        {
            _bombCount += 1;
            _damage = damage;
            
            _imageIndex.Add(_TICK, new Graphics.CSprite(Graphics.CTextures.EFFECT_BOMB));
            _imageIndex.Add(_FAST_TICK, new Graphics.CSprite(Graphics.CTextures.EFFECT_BOMB_FAST_TICK));
            _imageIndex.Add(_EXPLOSION, new Graphics.CSprite(Graphics.CTextures.EFFECT_EXPLOSION));
            _name = "bomb" + _bombCount;
            _followRoot = false;

            _hitBox = new Collision.CHitBox(this, 13, 11, 10, 10);
            swapImage(_TICK);
            shoot();
            startTimer0(120);

            MathExt.CPathNode[] _pathPositions = null;
            switch(direction)
            {
                case DIRECTION.DOWN:
                    _pathPositions = _createDownPath();
                    break;

                case DIRECTION.LEFT:
                    _pathPositions = _createLeftPath();
                    break;

                case DIRECTION.RIGHT:
                    _pathPositions = _createRightPath();
                    break;

                case DIRECTION.UP:
                    _pathPositions = _createUpPath();
                    break;
            }

            _followPath(_pathPositions);
        }

        private MathExt.CPathNode[] _createDownPath()
        {
            MathExt.CPathNode[] _pathPositions = new MathExt.CPathNode[5];
            _pathPositions[0] = new MathExt.CPathNode(new Vector2(_position.X, _position.Y + velocity.Y), 1);
            _pathPositions[1] = new MathExt.CPathNode(new Vector2(_position.X, _position.Y + velocity.Y - 2), 1);
            _pathPositions[2] = new MathExt.CPathNode(new Vector2(_position.X, _position.Y + 3 + velocity.Y), .5);
            _pathPositions[3] = new MathExt.CPathNode(new Vector2(_position.X, _position.Y + 2 + velocity.Y), 1);
            _pathPositions[4] = new MathExt.CPathNode(new Vector2(_position.X, _position.Y + 5 + velocity.Y), .5);

            return _pathPositions;
        }

        private MathExt.CPathNode[] _createUpPath()
        {
            MathExt.CPathNode[] _pathPositions = new MathExt.CPathNode[5];
            _pathPositions[0] = new MathExt.CPathNode(new Vector2(_position.X, _position.Y - velocity.Y), 1);
            _pathPositions[1] = new MathExt.CPathNode(new Vector2(_position.X, _position.Y - velocity.Y - 2), 1);
            _pathPositions[2] = new MathExt.CPathNode(new Vector2(_position.X, _position.Y - 3 - velocity.Y), .5);
            _pathPositions[3] = new MathExt.CPathNode(new Vector2(_position.X, _position.Y - 2 - velocity.Y), 1);
            _pathPositions[4] = new MathExt.CPathNode(new Vector2(_position.X, _position.Y - 5 - velocity.Y), .5);

            return _pathPositions;
        }

        private MathExt.CPathNode[] _createLeftPath()
        {
            MathExt.CPathNode[] _pathPositions = new MathExt.CPathNode[5];
            _pathPositions[0] = new MathExt.CPathNode(new Vector2(_position.X - _velocity.X, _position.Y), 1);
            _pathPositions[1] = new MathExt.CPathNode(new Vector2(_position.X - _velocity.X - 2, _position.Y - 2), 1);
            _pathPositions[2] = new MathExt.CPathNode(new Vector2(_position.X - _velocity.X - 4, _position.Y), .5);
            _pathPositions[3] = new MathExt.CPathNode(new Vector2(_position.X - _velocity.X - 6, _position.Y - 2), 1);
            _pathPositions[4] = new MathExt.CPathNode(new Vector2(_position.X - _velocity.X - 7, _position.Y - 2), .5);

            return _pathPositions;
        }

        private MathExt.CPathNode[] _createRightPath()
        {
            MathExt.CPathNode[] _pathPositions = new MathExt.CPathNode[5];
            _pathPositions[0] = new MathExt.CPathNode(new Vector2(_position.X + _velocity.X, _position.Y), 1);
            _pathPositions[1] = new MathExt.CPathNode(new Vector2(_position.X + _velocity.X + 2, _position.Y - 2), 1);
            _pathPositions[2] = new MathExt.CPathNode(new Vector2(_position.X + _velocity.X + 4, _position.Y), .5);
            _pathPositions[3] = new MathExt.CPathNode(new Vector2(_position.X + _velocity.X + 6, _position.Y - 2), 1);
            _pathPositions[4] = new MathExt.CPathNode(new Vector2(_position.X + _velocity.X + 7, _position.Y - 2), .5);

            return _pathPositions;
        }

        public override void pathNextNode(object sender)
        {
            MathExt.CPathNode node = (MathExt.CPathNode)sender;

            if (node.index == 1 || node.index == 4)
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Items:bombBounce"]);
        }

        public override void timer0(object sender)
        {
            swapImage(_FAST_TICK);
            startTimer1(60);
        }

        public override void timer1(object sender)
        {
            base.timer1(sender);
            _blowUp();
        }

        public override void animationEnd(object sender)
        {
            if (_state == ACTOR_STATES.EXPLODE)
                _killMe = true;
        }

        public override void update(GameTime gameTime)
        {
            _velocity = Vector2.Zero;
            base.update(gameTime);
        }

        public override void collide(object sender, CActor collider)
        {
            _cancelPath();
            if (_state == ACTOR_STATES.EXPLODE)
            {

            }
            else
            {
                if (collider is Actors.Collision.CSolidTile || collider is Actors.NPC.Enemies.CBaseEnemy || collider is Items.shields.CBaseShield)
                    _blowUp();
            }
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Collision.CSolidTile));
            _collidables.Add(typeof(NPC.Enemies.CBaseEnemy));
        }

        private void _blowUp()
        {
            _state = ACTOR_STATES.EXPLODE;
            swapImage(_EXPLOSION,false);
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Items:explosionSmall"]);
            _position.X -= 13;
            _position.Y -= 13;
            _hitBox.destroy();
            _hitBox = null;
            _hitBox = new Collision.CHitBox(this, 16, 20, 30, 30);
            _velocity = Vector2.Zero;
        }

        protected override void shoot()
        {
            swapImage(_TICK);
        }





    }
}
