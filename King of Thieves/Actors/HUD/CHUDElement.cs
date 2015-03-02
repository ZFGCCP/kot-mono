using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.HUD
{
    public class CHUDElement : CActor
    {
        protected Vector2 _fixedPosition = Vector2.Zero;

        public CHUDElement() : base()
        {

        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            _position.X = _fixedPosition.X - CMasterControl.camera.position.X;
            _position.Y = _fixedPosition.Y - CMasterControl.camera.position.Y;
        }

        
        
    }
}
