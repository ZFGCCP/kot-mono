using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using King_of_Thieves.Input;
using Gears.Cloud;

namespace King_of_Thieves.Actors
{
    class CActorTest : CActor
    {

        public CActorTest(string name) :
            base()
        {
            image = new Graphics.CSprite("mcDungeon2",Graphics.CTextures.textures["mcDungeon2"]);
        }

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(CActorTest));
        }

        public override void create(object sender)
        {
            //throw new NotImplementedException();
        }

        public override void destroy(object sender)
        {
            //throw new NotImplementedException();
        }

        public override void frame(object sender)
        {
            //throw new NotImplementedException();
            
        }

        public override void animationEnd(object sender)
        {
            throw new NotImplementedException();
        }

        public override void keyDown(object sender)
        {
            if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).getInputDown(Microsoft.Xna.Framework.Input.Keys.D))
                _position.X += 1;

            
        }

        public override void keyRelease(object sender)
        {
            
        }

        public override void draw(object sender)
        {
            //throw new NotImplementedException();
        }
    }
}
