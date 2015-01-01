using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Controllers
{
    class CEditorInputController : CActor
    {
        public bool _shutDown = false;
        private bool _dropTile = false;
        private Texture2D _panel;

        public CEditorInputController()
        {
            _panel = CMasterControl.glblContent.Load<Texture2D>("whiteBox");
        }

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }

        public override void keyDown(object sender)
        {
            if ((Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.Back))
            {
                Graphics.CGraphics.changeResolution(320, 240);
                _shutDown = true;
            }

        }

        public override void tap(object sender)
        {
            if (System.Windows.Forms.Form.ActiveForm ==
                (System.Windows.Forms.Control.FromHandle(Gears.Cloud.Master.GetGame().Window.Handle) as System.Windows.Forms.Form))
                _dropTile = true;
                
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }

        public bool dropTile
        {
            get
            {
                bool temp = _dropTile;
                _dropTile = false;
                return temp;
            }
        }

        public override void drawMe(bool useOverlay = false)
        {
            base.drawMe();

            Graphics.CGraphics.spriteBatch.Draw(_panel, new Rectangle(0, 0, 800, 32), Color.Wheat);
            Graphics.CGraphics.spriteBatch.Draw(Graphics.CTextures.rawTextures["pointer"], new Rectangle((Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).mouseX,
                                                                        (Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).mouseY, 32,32), Color.White);
        }
    }
}
