using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using King_of_Thieves.Graphics;
using King_of_Thieves.Actors;
using System.Collections.Generic;

using Gears.Cloud;
using King_of_Thieves.usr.local;
using King_of_Thieves.Input;
using System.Linq;
using Gears.Cloud.Utility;
using System.Timers;
using System.Diagnostics;

namespace King_of_Thieves.Actors.HUD.info
{
	public class CBenchmarkInfo : CBaseInfo
	{
		private Stopwatch _updateTimer = new Stopwatch();
        private Stopwatch _drawTimer = new Stopwatch();
        private Timer _fpsTimer = new Timer();
        private int _updateFrames;
        private int _drawFrames;
        private int _updateFPS;
        private int _drawFPS;

        private void _fpsHandler(object sender, ElapsedEventArgs e) 
        { 
            _updateFPS = _updateFrames; 
            _updateFrames = 0; 
            _drawFPS = _drawFrames; 
            _drawFrames = 0; 
        }
        

		public CBenchmarkInfo () : base()
		{
			_fixedPosition.X = 10;
			_fixedPosition.Y = 100;

            _fpsTimer.Elapsed += new ElapsedEventHandler(_fpsHandler);
            _fpsTimer.Enabled = true;
            _fpsTimer.Interval = 1000;
            _fpsTimer.Start();

			this.setInfo ("Loaded");
		}

		public override void update(Microsoft.Xna.Framework.GameTime gameTime)
		{
            _updateFrames++;
            _updateTimer.Restart();
            base.update(gameTime);
		}

		public override void draw (object sender)
		{
            _drawFrames++;
			base.draw (sender);
           
			//TODO draw measurements
            _drawTimer.Stop();


            double updateTime = (double)_updateTimer.Elapsed.Milliseconds;
            double drawTime = (double)_drawTimer.Elapsed.Milliseconds;
            _drawTimer.Reset();
            _drawTimer.Start();
			this.setInfo("UpdateTime: " + updateTime + " ms Update FPS: " + _updateFPS + "\n" +
                         "DrawTime: " + drawTime + " ms Draw FPS: " + _drawFPS + "\n");
		}
	}
}

