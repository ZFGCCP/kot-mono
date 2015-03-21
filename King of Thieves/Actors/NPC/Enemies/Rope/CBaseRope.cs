using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Rope
{
    enum ROPEETYPE
    {
        NORMAL = 0
    
    }

    class CBaseRope : CBaseEnemy
    {
        protected const string _NPC_ROPE = "npc:rope";
        private static int _ropeCount = 0;
        private static int _greenRopeCount = 0;

         //image index constants
        protected const string _SLITHER_DOWN = "slitherDown";
        protected const string _SLITHER_UP = "slitherUp";
        protected const string _SLITHER_LEFT = "slitherLeft";
        protected const string _SLITHER_RIGHT = "slitherRight";

        //slither fast
        protected const string _FAST_SLITHER_DOWN = "fastslitherDown";
        protected const string _FAST_SLITHER_UP = "fastslitherUp";
        protected const string _FAST_SLITHER_LEFT = "fastslitherLeft";
        protected const string _FAST_SLITHER_RIGHT = "fastslitherRight";


        public CBaseRope(int foh, params dropRate[] drops)
            : base(drops)
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_NPC_ROPE))
                Graphics.CTextures.rawTextures.Add(_NPC_ROPE, CMasterControl.glblContent.Load<Texture2D>(@"sprites/npc/rope"));


            _ropeCount += 1;
        }


              public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            switch (additional[0])
            {
                case "G":
                    _value = 1;
                    _imageIndex.Add( _SLITHER_DOWN, new Graphics.CSprite(Graphics.CTextures.DROPS_RUPEE_GREEN));
                    break;


           default:
                    break;
            }
                  {};
            swapImage( _SLITHER_DOWN);
        }




        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
        }

        public override void create(object sender)
        {
            base.create(sender);

        }

        public override void destroy(object sender)
        {
         
            base.destroy(sender);
        }

        protected override void cleanUp()

        public override void timer0(object sender)
        {
            base.timer0(sender);
        }

        public override void timer1(object sender)
        {
            base.timer1(sender);
            
        }

        public override void timer2(object sender)
        {
            base.timer2(sender);
        }

        public override void timer3(object sender)
        {
            base.timer3(sender);
        }

        public override void drawMe(bool useOverlay = false)
        {
            
                base.drawMe(useOverlay);
        }

      











    }

     
}
