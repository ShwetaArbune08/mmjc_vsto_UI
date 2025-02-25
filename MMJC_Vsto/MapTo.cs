using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sundeus
{
    public partial class MapTo : Form
    {
        public MapTo()
        {
            InitializeComponent();
            //   comboBox1list       
            List<String> listBox1 = new List<String>();
            foreach (var item in Loginuserinset.Attributes)
            {
                listBox1.Add(item.Label);
            }

            comboBox1list.DataSource = listBox1;
     //       comboBox1list.DisplayMember = "Description";

        }

        private void ComboBox1list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var attributeName = comboBox1list.SelectedValue.ToString();
           // string attributeName = "%" + comboBox1list.SelectedValue.ToString() + "%";


            Globals.ThisAddIn.AddRichTextControlAtSelectiontemplate(attributeName, attributeName, attributeName,"Metadata");
            this.Hide();

        }
    }
}
