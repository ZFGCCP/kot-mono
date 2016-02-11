using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gears.Cloud;
using Gears.Navigation;

namespace King_of_Thieves.usr.local.splash
{
    class CZFGCSplash : MenuReadyGameState
    {
        private static SpriteFont _sherwood = CMasterControl.glblContent.Load<SpriteFont>(@"Fonts/sherwood");
        private int _timer = 120;
        private Vector2 _pos = new Vector2(100, 110);
        private Graphics.CSprite _link = null;

        public CZFGCSplash()
        {
            Graphics.CSprite.initFrameRateMapping();
            _link = new Graphics.CSprite("splash:link");
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["HUD:rupees:rupeeGet"]);
        }

        public override void Update(GameTime gameTime)
        {
            _timer--;

            if (_timer <= 0)
                Master.Push(new CDedicationPage());
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Graphics.CGraphics.GPU.Clear(Color.Black);
            Graphics.CGraphics.spriteBatch.DrawString(_sherwood, "Zelda Fan Game Central", _pos, Color.White);
            _link.draw(70, 105);
        }
    }
}