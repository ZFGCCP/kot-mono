using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gears.Cloud
{
    public abstract class GameState
    {
        //HandlesInput is not used currently. (but might be)
        
        protected internal bool _IsOverlay = false;
        public bool IsOverlay
        {
            get { return _IsOverlay; }
            set { if (_IsOverlay != false) _IsOverlay = value; }
        }
        //protected internal bool _HandlesInput = false;
        protected internal bool _StateIsActive = false;

        //internal bool HandlesInput()
        //{
        //    return _HandlesInput;
        //}
        public bool StateIsActive()
        {
            return _StateIsActive;
        }
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
