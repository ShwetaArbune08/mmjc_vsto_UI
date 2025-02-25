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
    public partial class SelectContractTypeForm : Form
    {
        public SelectContractTypeForm()
        {
            InitializeComponent();

            comboBox1.DataSource = Loginuserinset.LstComboitem;
            comboBox1.DisplayMember = "Description";

        }

        class ComboItem
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }

        public static dynamic  _customTaskPanes;
        private void Button1_Click(object sender, EventArgs e)
        {

   
           ContractType g = new ContractType();
            g = (ContractType)comboBox1.SelectedValue;
            Loginuserinset.SelectedContracttype = g.ContractTypeId;
            Loginuserinset.SelectedContracttypename = g.Description;
            Loginuserinset.templatename = textBox1.Text;
            Loginuserinset.templatesummary = richTextBox1.Text;
            bool status =  getContracttypemetadata( g.ContractTypeId);
          
            foreach (var item in Globals.ThisAddIn.CustomTaskPanes)
            {
                item.Visible = false;
            }


            if (status)
            {
                this.Hide();
              Globals.ThisAddIn.CustomTaskPanes.Add(new OrdersListUserControl(
                            Loginuserinset.Attributes), "MetaData").Visible = true;

            }



        }


        public bool getContracttypemetadata(int ContractTypeId)
        {

            HttpClient clint = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            clint.BaseAddress = new Uri(Loginuserinset.url); 
            HttpResponseMessage response = clint.GetAsync("ContractType/"+ ContractTypeId+ "?tenantId="+Loginuserinset.tenantId).Result;

            var contracttyoedata = response.Content.ReadAsAsync<ContractType>().Result;
            string msg = string.Empty;
            Loginuserinset.Attributes = new List<Attributes>();
            foreach (var item in contracttyoedata.Attributes)
            {
                              Loginuserinset.Attributes.Add(item);
            }
            return true;


        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SelectContractTypeForm_Load(object sender, System.EventArgs e)
        {
        
        }
  
    }
}
