using Newtonsoft.Json;
using Sundeus.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sundeus
{
    public partial class ClausesName : Form
    {
        public ClausesName()
        {
            InitializeComponent();
        }
        public static string clausetext;
        public ClausesName(string _clausetext)
        {
            clausetext = _clausetext;
            InitializeComponent();
        }

        private void ClausesName_Load(object sender, EventArgs e)
        {
            clausebodytextBox7.Text = clausetext;

            contracttypetextBox1.Text = Loginuserinset.SelectedContracttypename;

            TemplateNametextbox.Text = Loginuserinset.templatename;
            contracttypetextBox1.Enabled = false;
            TemplateNametextbox.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private async Task SaveClauses()
        {

            Clauses Clausesobj = new Clauses();

            Clausesobj.Name = clausenametextbox.Text;
            Clausesobj.Text = clausebodytextBox7.Text;
            Clausesobj.Language_Id = 1;
            Clausesobj.IsMandatory = Mandatory.Checked;
            Clausesobj.IsTracked = trackcheckBox1.Checked;
            Clausesobj.ContractTypeId = Loginuserinset.SelectedContracttype;
            Clausesobj.Description = descriptiontextbox.Text;
            Clausesobj.IsDeleted = false;


            Clausesobj.CreatedBy = "ADDIN";
            Clausesobj.UpdatedBy = "ADDIN";
            Clausesobj.CreatedDate = DateTime.Now;
            Clausesobj.UpdatedDate = DateTime.Now;
            //new clause addition is here

            Loginuserinset.LstClauses.Add(Clausesobj);



            this.Close();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Globals.ThisAddIn.Application.Selection.Text = clausebodytextBox7.Text;
        //    AddRichTextControlAtSelectiontemplate(attributeName, data.ToString(), clauseid, "clause");


            Globals.ThisAddIn.AddRichTextControlAtSelectiontemplate(clausebodytextBox7.Text, clausenametextbox.Text,"1", "Clause");
            _ = SaveClauses();
            this.Close();
        }

        private void descriptiontextbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
