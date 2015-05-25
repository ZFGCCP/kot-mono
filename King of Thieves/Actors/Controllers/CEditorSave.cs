using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Actors.Controllers
{
    class CEditorSave : CActor
    {
        private bool _saveFile = false;
        private string _fileName = "";

        public CEditorSave()
        {
            _imageIndex.Add("iconSave", new Graphics.CSprite("editor:icons:save", Graphics.CTextures.textures["editor:icons:save"]));
            _position = new Vector2(45, 4);

            swapImage("iconSave", false);

            _hitBox = new Collision.CHitBox(this, 0, 0, 16, 16);
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
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = "";
            sfd.Filter = "xml files (*.xml)|*.xml";
            sfd.Title = "Save Map";

            if (sfd.ShowDialog() != DialogResult.Cancel)
            {
                _saveFile = true;
                _fileName = sfd.FileName; 
            }

           

            sfd = null;
        }

        public string fileName
        {
            get
            {
                return _fileName;
            }
        }

        public bool saveFile
        {
            get
            {
                bool temp = _saveFile;
                _saveFile = false;

                return temp;
            }
        }

        public override void drawMe(bool useOverlay = false, SpriteBatch spriteBatch = null)
        {
            base.drawMe();
        }

    }
}
