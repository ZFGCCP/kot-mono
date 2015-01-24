using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace King_of_Thieves.Forms.Map_Edit
{
    public partial class FrmNewComponent : Form
    {
        private FrmMap _parent = null;

        public FrmNewComponent(FrmMap parent) :
            this()
        {
            _parent = parent;
        }

        public FrmNewComponent()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
