using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.HUD.info
{
	public class CBaseInfo : CHUDElement
	{
		private static SpriteFont _sherwood = CMasterControl.glblContent.Load<SpriteFont>(@"Fonts/sherwood");
		protected Vector2 _textOffset = Vector2.Zero;
		private static readonly Vector2 _SHADOW = new Vector2(1, 1);
		private Color _textColor = Color.White;

		private String _info;

		public CBaseInfo () : base()
		{
			//we only support one color in alpha stage, the technology is just not there
			this._textColor = Color.WhiteSmoke;
		}

		public void setInfo(String info)
		{
			this._info = info;
		}


		public override void draw(object sender)
		{
			base.draw(sender);
			this.drawTextWithShadow(this._info, this._textColor);
		}

		private void drawTextWithShadow(String text, Color color)
		{
			//shadow
			Graphics.CGraphics.spriteBatch.DrawString(_sherwood, text, _position + _textOffset + _SHADOW, Color.Black);
			//actual text
			Graphics.CGraphics.spriteBatch.DrawString(_sherwood, text, _position + _textOffset, color);
		}
	}
}

