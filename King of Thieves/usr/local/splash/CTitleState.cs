using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Navigation;
using King_of_Thieves.Actors;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.usr.local.splash
{
    class CTitleState : MenuReadyGameState
    {
        private Graphics.CSprite _background = null;
        private Graphics.CSprite _logo = null;
        private int _freezeCounter = 30;
        private bool _freeze = true;

        public CTitleState() :
            base()
        {
            //title - we can remove these after the title closed.  They're small enough that we can load them up as needed
            Graphics.CSprite.initFrameRateMapping();
            Graphics.CTextures.addRawTexture("title", "title/title-background");
            Graphics.CTextures.addRawTexture("logo", "title/title-logo");

            Graphics.CTextures.addTexture("title:background", new Graphics.CTextureAtlas("title", 320, 240, 0, "0:0", "0:0", 0, false, false, false));
            Graphics.CTextures.addTexture("title:logo", new Graphics.CTextureAtlas("logo", 320, 240, 0, "0:0", "0:0", 0, false, false, false));

            _background = new Graphics.CSprite("title:background");
            _logo = new Graphics.CSprite("title:logo");

            //CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["bgm:test"]);

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            _background.draw(0, 0);
            _logo.draw(0, 0);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            if (_freeze)
            {
                if (_freezeCounter-- <= 0)
                    _freeze = false;
            }
            else if (CMasterControl.glblInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                Master.Push(new PlayableState());
                CMasterControl.audioPlayer.stopAllMusic();
            }
        }
    }
}
