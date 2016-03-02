using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gears.Cloud.Utility;
using Gears.Cloud;

namespace King_of_Thieves.Graphics
{
    class CCamera
    {
        private Matrix _transformation;
        public Vector3 position;
        public Vector2 _normalizedPosition = Vector2.Zero;
        public Vector3 scale;
        private Actors.CActor _actorToFollow = null;
        private Actors.HUD.CCameraBoundary _boundary = null;
        private Actors.Collision.CCameraLimit _limit = null;
        private bool _locked = false;

        public CCamera()
        {
            reset();

        }

        public void update(GameTime gameTime = null)
        {
            _transformation = Matrix.CreateRotationZ(0) * Matrix.CreateScale(scale) * Matrix.CreateTranslation(position);

            _normalizedPosition.X = 0 - position.X;
            _normalizedPosition.Y = 0 - position.Y;

            if (_actorToFollow != null && !_locked)
                _followObject();
        }

        public void lockCamera()
        {
            _locked = true;
        }

        public void unlockCamera()
        {
            _locked = false;
        }

        public void reset()
        {
            scale = new Vector3(1, 1, 1);
            position = new Vector3(0, 0, 0);


            _boundary = new Actors.HUD.CCameraBoundary(new Rectangle(128, 30, 1, 1));
        }

        public void setBoundary(Vector2 position)
        {
            _boundary = new Actors.HUD.CCameraBoundary(new Rectangle((int)position.X, (int)position.Y, 1, 1));
        }

        public void jump(Vector3 position)
        {
            this.position = position;
        }

        public void translate(Vector3 translation)
        {
            position += translation;
        }

        private void _followObject()
        {

            if (_actorToFollow.position.X >= _boundary.right && _actorToFollow.position.X > _actorToFollow.oldPosition.X)
            {
                if (_limit == null || _normalizedPosition.X + 240 < _limit.position.X + _limit.width)
                { 
                    position.X -= (_actorToFollow.position.X - _actorToFollow.oldPosition.X);
                    _boundary.move((int)(_actorToFollow.position.X - _actorToFollow.oldPosition.X), 0);
                }
            }
            else if (_actorToFollow.position.X <= _boundary.left && _actorToFollow.position.X < _actorToFollow.oldPosition.X)
            {
                if (_limit == null || _normalizedPosition.X > _limit.position.X)
                {
                    position.X -= (_actorToFollow.position.X - _actorToFollow.oldPosition.X);
                    _boundary.move((int)(_actorToFollow.position.X - _actorToFollow.oldPosition.X), 0);
                }
            }

            if (_actorToFollow.position.Y >= _boundary.bottom && _actorToFollow.position.Y > _actorToFollow.oldPosition.Y)
            {
                if (_limit == null || _normalizedPosition.Y + 160 < _limit.position.Y + _limit.height)
                {
                    position.Y -= (_actorToFollow.position.Y - _actorToFollow.oldPosition.Y);
                    _boundary.move(0, (int)(_actorToFollow.position.Y - _actorToFollow.oldPosition.Y));
                }
            }
            else if (_actorToFollow.position.Y <= _boundary.top && _actorToFollow.position.Y < _actorToFollow.oldPosition.Y)
            {
                if (_limit == null || _normalizedPosition.Y  > _limit.position.Y)
                {
                    position.Y -= (_actorToFollow.position.Y - _actorToFollow.oldPosition.Y);
                    _boundary.move(0, (int)(_actorToFollow.position.Y - _actorToFollow.oldPosition.Y));
                }
            }
        }

        public Matrix transformation
        {
            get
            {
                return _transformation;
            }
        }

        public Actors.CActor actorToFollow
        {
            get
            {
                return _actorToFollow;
            }
            set
            {
                _actorToFollow = value;
            }
        }

        public Actors.Collision.CCameraLimit cameraLimit
        {
            set
            {
                _limit = value;
            }
        }

        public Vector2 topLeftCorner
        {
            get
            {
                return _normalizedPosition;
            }
        }

        public bool locked
        {
            get
            {
                return _locked;
            }
        }
    }
}
