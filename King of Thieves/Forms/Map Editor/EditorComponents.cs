using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace King_of_Thieves.Forms.Map_Editor
{
    public partial class EditorComponents : Form
    {
        private List<string> nameSpaceList = new List<string>();
        private const string TOP_LEVEL = "King_of_Thieves.Actors.";

        public EditorComponents()
        {
            InitializeComponent();
        }

        private void btnPlayTest_Click(object sender, EventArgs e)
        {
            Gears.Cloud.Master.Push(new usr.local.PlayableState());
        }

        private void EditorComponents_Load(object sender, EventArgs e)
        {
            cmbActorCategory.SelectedIndex = 0;
            populateNameSpaceList();
            populateActorList("all");
        }

        private void populateNameSpaceList()
        {
            var all = Assembly.GetExecutingAssembly().GetTypes().Select(t => t.Namespace).Distinct();
            foreach (var x in all)
            {
                string stringVal = x.ToString();
                if (stringVal.IndexOf(TOP_LEVEL + "Items") == 0 ||
                    stringVal.IndexOf(TOP_LEVEL + "NPC") == 0 ||
                    stringVal.IndexOf(TOP_LEVEL + "Player") == 0 ||
                    stringVal.IndexOf(TOP_LEVEL + "World") == 0 
                    )nameSpaceList.Add(stringVal);
            }
        }

        private List<String> getNameSpacesByFilter(string filter)
        {
            List<String> results = new List<string>();

            foreach (string nameSpace in nameSpaceList)
                if (nameSpace.IndexOf(TOP_LEVEL + filter) == 0)
                    results.Add(nameSpace);

            return results;
        }

        private void populateActorList(string filter)
        {
            lstActorList.Items.Clear();
            List<System.Type> actorList = new List<System.Type>();
            List<String> nameSpaceReference = filter == "all" ? nameSpaceList : getNameSpacesByFilter(filter);

            foreach (string nameSpace in nameSpaceReference)
                actorList.AddRange(Assembly.GetExecutingAssembly().GetTypes().ToList().Where(t => t.Namespace == nameSpace).ToList());

            foreach(System.Type type in actorList)
                lstActorList.Items.Add(type.ToString().Substring(type.ToString().LastIndexOf('.') + 1));

            lstActorList.Sorted = true;
        }

        private void cmbActorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateActorList(cmbActorCategory.Text);
        }
    }
}
