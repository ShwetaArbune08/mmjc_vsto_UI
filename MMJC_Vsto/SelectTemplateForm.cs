using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Formatting;
using Sundeus.models;
using dragdrop;
using System.Net;


namespace Sundeus
{
    public partial class SelectTemplateForm : Form
    {
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString()
            {
                return Text;
            }
        }
        public class ComboboxItemtemplate
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString()
            {
                return Text;
            }
        }
        public SelectTemplateForm()
        {
            InitializeComponent();
            foreach (var item in Loginuserinset.LstComboitem)
            {
                ComboboxItem itemComboboxItem = new ComboboxItem();
                itemComboboxItem.Text =item.Description.ToString();
                itemComboboxItem.Value = item.ContractTypeId.ToString();
                comboBox1listcontracttype.Items.Add(itemComboboxItem);
            }


        }

        private void comboBox1listcontracttype_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            // Save the selected employee's name, because we will remove
            // the employee's name from the list.
            ComboboxItem selected = (ComboboxItem)comboBox1listcontracttype.SelectedItem;
            int contractypeID = 0;
            try
            {
                contractypeID = Convert.ToInt32(selected.Value);
            }
            catch (Exception )
            {

                throw ;
            }
            bool list = getcontracttypeTemplatesList(contractypeID);
            comboBoxtemplate1.Items.Clear();
            if (list)
            {
                foreach (var item in Loginuserinset.LstTemplate)
                {
                    ComboboxItemtemplate itemComboboxItem = new ComboboxItemtemplate();
                    itemComboboxItem.Text = item.TemplateName.ToString();
                    itemComboboxItem.Value = item.TemplateId.ToString();
                    comboBoxtemplate1.Items.Add(itemComboboxItem);
                }
            }

        }

        private void Next_Click(object sender, EventArgs e)
        {
            ComboboxItemtemplate selected = (ComboboxItemtemplate)comboBoxtemplate1.SelectedItem;
            try
            {
              int templateId = Convert.ToInt32(selected.Value);
                Downoadtemplate(templateId);

            }
            catch (Exception)
            {

                throw;
            }
         
        }
        public bool getcontracttypeTemplatesList(int contractypeID)
        {

            HttpClient clint = new HttpClient();
            clint.BaseAddress = new Uri(Loginuserinset.url);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


            //https://localhost:44307/api/Template/5

            HttpResponseMessage response = clint.GetAsync("Template/" + contractypeID).Result;


            var TemplateModeldata = response.Content.ReadAsAsync<IEnumerable<TemplateModel>>().Result;
            string msg = string.Empty;
            Loginuserinset.LstTemplate = new List<TemplateModel>();
            foreach (var item in TemplateModeldata)
            {
                TemplateModel c = new TemplateModel();
                c.TemplateName = item.TemplateName;
                c.TemplateId = item.TemplateId;


                Loginuserinset.LstTemplate.Add(c);
            }
            return true;


        }
   
    public void Downoadtemplate(int templateId)
    {
            string url= new Uri(Loginuserinset.url)+ "GenerateDocument/DownloadTemplate?Templateid="+ templateId;
            Globals.ThisAddIn.Application.Documents.Open(url);
            Loginuserinset.seletedtemplateid = templateId;
            try
            {
                ComboboxItem selected = (ComboboxItem)comboBox1listcontracttype.SelectedItem;
               // ComboboxItemtemplate selectedtem = templateId;
                int contractypeID = 0;

                contractypeID = Convert.ToInt32(selected.Value);
                Loginuserinset.seletedcontractId = contractypeID;
                Loginuserinset.seletedtemplateid = templateId;
            }
            catch (Exception)
            {

                throw;
            }
     
        }
    }
}
