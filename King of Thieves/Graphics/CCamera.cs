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
        public Vector3 scale;
        private Actors.CActor _actorToFollow = null;
        public Vector3 threshold;
        private Actors.HUD.CCameraBoundary _boundary = null;

        public CCamera()
        {
            reset();
        }

        public void update(GameTime gameTime)
        {
            _transformation = Matrix.CreateRotationZ(0) * Matrix.CreateScale(scale) * Matrix.CreateTranslation(position);

            if (_actorToFollow != null)
                _followObject();
        }

        public void reset()
        {
            scale = new Vector3(1, 1, 1);
            position = new Vector3(0, 0, 0);
            threshold = new Vector3(80, 30,0);


            _boundary = new Actors.HUD.CCameraBoundary(new Rectangle(80, 30, 100, 100));
        }

        private void _followObject()
        {

            if (_actorToFollow.position.X >= _boundary.right && _actorToFollow.position.X > _actorToFollow.oldPosition.X)
            {
                position.X -= (_actorToFollow.position.X - _actorToFollow.oldPosition.X);
                _boundary.move((int)(_actorToFollow.position.X - _actorToFollow.oldPosition.X), 0);
            }
            else if (_actorToFollow.position.X <= _boundary.left && _actorToFollow.position.X < _actorToFollow.oldPosition.X)
            {
                position.X -= (_actorToFollow.position.X - _actorToFollow.oldPosition.X);
                _boundary.move((int)(_actorToFollow.position.X - _actorToFollow.oldPosition.X), 0);
            }

            if (_actorToFollow.position.Y >= _boundary.bottom && _actorToFollow.position.Y > _actorToFollow.oldPosition.Y)
            {
                position.Y -= (_actorToFollow.position.Y - _actorToFollow.oldPosition.Y);
                _boundary.move(0, (int)(_actorToFollow.position.Y - _actorToFollow.oldPosition.Y));
            }
            else if (_actorToFollow.position.Y <= _boundary.top && _actorToFollow.position.Y < _actorToFollow.oldPosition.Y)
            {
                position.Y -= (_actorToFollow.position.Y - _actorToFollow.oldPosition.Y);
                _boundary.move(0, (int)(_actorToFollow.position.Y - _actorToFollow.oldPosition.Y));
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

    }
}
