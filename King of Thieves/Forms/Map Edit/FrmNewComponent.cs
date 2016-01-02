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
        private string _actorName = "";
        private List<string> _params = new List<string>();

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
            if (txtName.Text.Trim() == string.Empty)
                MessageBox.Show("You must enter an actor name.");
            else
            {
                _actorName = txtName.Text;
                this.Hide();
            }
        }

        public string actorName
        {
            get
            {
                return _actorName;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtParamValue.Text.Trim() != string.Empty)
            {
                lstParams.Items.Add(txtParamValue.Text);
                _params.Add(txtParamValue.Text);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstParams.SelectedIndex >= 0)
            {
                lstParams.Items.RemoveAt(lstParams.SelectedIndex);
                _params.RemoveAt(lstParams.SelectedIndex);
            }
        }

        public string[] parameters
        {
            get
            {
                string[] copiedList = new string[_params.Count];
                _params.CopyTo(copiedList, 0);
                return copiedList;
            }
        }

        private void FrmNewComponent_Load(object sender, EventArgs e)
        {

        }
    }
}
