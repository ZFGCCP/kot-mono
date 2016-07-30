using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors.NPC.Other
{
    public class CBaseNpc : CActor
    {
        protected event actorEventHandler onDialogBegin;
        protected event actorEventHandler onDialogEnd;
        

        protected string[] _currentDialog = null;
        protected bool _speaking = false;
        private bool _textRequest = false;

        public CBaseNpc() :
            base()
        {
            onDialogBegin += new actorEventHandler(dialogBegin);
            onDialogEnd += new actorEventHandler(dialogEnd);
        }

        public override void update(GameTime gameTime)
        {
            if (!CMasterControl.buttonController.textBoxActive)
                base.update(gameTime);

            if (_textRequest)
            {
                onDialogBegin(this);
                _textRequest = false;
            }

            if (_speaking && Actors.HUD.Text.CTextBox.messageFinished)
                onDialogEnd(null);

        }

        public override void keyRelease(object sender)
        {
            base.keyRelease(sender);

            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;

            if (input.keysReleased.Contains(input.getKey(CInput.KEY_ACTION)))
                if (MathExt.MathExt.checkPointInCircle(_position, new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY), _hearingRadius))
                    onDialogBegin(sender);
        }

        protected virtual void dialogBegin(object sender)
        {
            if (_currentDialog != null)
            {
                CMasterControl.buttonController.createTextBox(_currentDialog);
                _speaking = true;
            }
        }

        protected virtual void dialogEnd(object sender)
        {
            _speaking = false;
        }

        protected void _triggerTextEvent()
        {
            _textRequest = true;
        }

        protected float _searchForWalls(int distance, int width)
        {
            float angle = 999;
            //Collision.CHitBox hitbox = new Collision.CHitBox(null,)


            return angle;
        }
    }
}
