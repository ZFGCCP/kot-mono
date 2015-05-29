using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace King_of_Thieves.Actors.Controllers
{
    class CEditorOpen : CActor
    {

        private bool _openFile = false;
        private string _fileName = "";

        public CEditorOpen()
        {
            _imageIndex.Add("iconOpen", new Graphics.CSprite("editor:icons:open"));
            _position = new Vector2(25, 4);

            swapImage("iconOpen", false);

            _hitBox = new Collision.CHitBox(this, 0, 0, 16, 16);
        }

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }

        public override void click(object sender)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.Filter = ".xml files (*.xml) | *.xml";
            ofd.Title = "Open Map File";
            
            ofd.ShowDialog();

            if (ofd.ShowDialog() != DialogResult.Cancel)
            {
                _openFile = true;
                _fileName = ofd.FileName;
            }

            ofd = null;
        }

        public bool openFile
        {
            get
            {
                bool temp = _openFile;
                _openFile = false;

                return temp;
            }
        }

        public string fileName
        {
            get
            {
                return _fileName;
            }
        }

        public override void drawMe(bool useOverlay = false, SpriteBatch spriteBatch = null)
        {
            base.drawMe();
        }
        
    }
}
