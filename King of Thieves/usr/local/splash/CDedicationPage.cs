using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gears.Navigation;
using King_of_Thieves.Actors;
using Gears.Cloud;
using King_of_Thieves.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.usr.local.splash
{
    class CDedicationPage : MenuReadyGameState
    {
        private static SpriteFont _sherwood = CMasterControl.glblContent.Load<SpriteFont>(@"Fonts/sherwood");
        private int _timer = 800;
        private Vector2 _pos = new Vector2(50, 40);

        public CDedicationPage()
        {
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["bgm:metallica"]);
        }

        public override void Update(GameTime gameTime)
        {
            _timer--;

            if (_timer <= 0)
                Master.Push(new CTitleState());
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Graphics.CGraphics.GPU.Clear(Color.Black);
            Graphics.CGraphics.spriteBatch.DrawString(_sherwood, "In memory of Justin O'Leary\n\n Forever the king of ZFGC\n\n   2-18-86 to 5-22-15", _pos, Color.White);
        }
    }
}
