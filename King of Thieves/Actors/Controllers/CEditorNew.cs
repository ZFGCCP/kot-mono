using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace King_of_Thieves.Actors.Controllers
{
    class CEditorNew : CActor
    {
        private bool _createNew = false;

        public CEditorNew()
        {
            _imageIndex.Add("iconNew", new Graphics.CSprite("editor:icons"));
            _position = new Vector2(5, 5);

            swapImage("iconNew", false);

            _hitBox = new Collision.CHitBox(this, 0,0,16, 16);
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }

        public override void click(object sender)
        {
            if (MessageBox.Show("Are you sure you want to create a new map?", "Create New", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            _createNew = true;
        }

        public bool createNew
        {
            get
            {
                bool temp = _createNew;
                _createNew = false;
                return temp;
            }

        }

        public override void drawMe(bool useOverlay = false, SpriteBatch spriteBatch = null)
        {
            base.drawMe();
        }
    }
}
