using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Actors.NPC.Enemies.Rope
{
    enum ROPEETYPE
    {
        NORMAL = 0
    
    }

    class CBaseRope : CBaseEnemy
    {
        protected const string SPRITE_NAMESPACE = "npc:rope";
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
            if (!Graphics.CTextures.rawTextures.ContainsKey(SPRITE_NAMESPACE))
            {

                Graphics.CTextures.rawTextures.Add(SPRITE_NAMESPACE, CMasterControl.glblContent.Load<Texture2D>(@"sprites/npc/rope"));


                _ropeCount += 1;


                Graphics.CTextures.addTexture(_SLITHER_DOWN, new Graphics.CTextureAtlas(SPRITE_NAMESPACE, 32, 32, 1, "0:1", "3:1", 2));
                Graphics.CTextures.addTexture(_SLITHER_UP, new Graphics.CTextureAtlas(SPRITE_NAMESPACE, 32, 32, 1, "0:2", "3:2", 2));
                Graphics.CTextures.addTexture(_SLITHER_LEFT, new Graphics.CTextureAtlas(SPRITE_NAMESPACE, 32, 32, 1, "0:0", "0:3", 2));
               


                Graphics.CTextures.addTexture(_FAST_SLITHER_DOWN, new Graphics.CTextureAtlas(SPRITE_NAMESPACE, 32, 32, 1, "0:1", "3:1", 4));
                Graphics.CTextures.addTexture(_FAST_SLITHER_UP, new Graphics.CTextureAtlas(SPRITE_NAMESPACE, 32, 32, 1, "0:2", "3:2", 4));
                Graphics.CTextures.addTexture(_FAST_SLITHER_LEFT, new Graphics.CTextureAtlas(SPRITE_NAMESPACE, 32, 32, 1, "0:0", "0:3", 4));
                

            }


            _imageIndex.Add(_SLITHER_DOWN, new Graphics.CSprite(_SLITHER_DOWN));
            _imageIndex.Add(_SLITHER_UP, new Graphics.CSprite(_SLITHER_UP));
            _imageIndex.Add(_SLITHER_LEFT, new Graphics.CSprite(_SLITHER_LEFT));
            _imageIndex.Add(_SLITHER_RIGHT, new Graphics.CSprite(_SLITHER_LEFT,true));


            _imageIndex.Add(_FAST_SLITHER_DOWN, new Graphics.CSprite(_FAST_SLITHER_DOWN));
            _imageIndex.Add(_FAST_SLITHER_UP, new Graphics.CSprite(_FAST_SLITHER_UP));
            _imageIndex.Add(_FAST_SLITHER_LEFT, new Graphics.CSprite(_FAST_SLITHER_LEFT));
            _imageIndex.Add(_FAST_SLITHER_RIGHT, new Graphics.CSprite(_FAST_SLITHER_LEFT,true));

        }




              public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            switch (additional[0])
            {
                case "G":
                    
                   
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
        {

        }

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
