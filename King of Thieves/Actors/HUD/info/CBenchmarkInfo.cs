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

		public CBenchmarkInfo () : base()
		{
			_fixedPosition.X = 10;
			_fixedPosition.Y = 120;

			this.setInfo ("Loaded");
		}

		public override void update(Microsoft.Xna.Framework.GameTime gameTime)
		{
			_updateTimer.Stop();

			_updateTimer.Start();
		}

		public override void draw (object sender)
		{
			base.draw (sender);
			//TODO draw measurements

			double updateTime = System.Math.Ceiling((double)_updateTimer.Elapsed.Milliseconds);
			this.setInfo("UpdateTime: " + updateTime + " ms\n");
		}
	}
}

