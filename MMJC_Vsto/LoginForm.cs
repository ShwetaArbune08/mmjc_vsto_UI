using log4net;
using Sundeus.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sundeus
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
          //  log.Info("");
         //   if (usernametextbox.Text=="admin"&&textBox1.Text=="admin")
            {
              
                Loginuserinset.username = usernametextbox.Text;
                Loginuserinset.url =comboBox1selectServie.Text;
                Loginuserinset.tenantId =Convert.ToInt32(textBox2.Text);

                if (getcontracttype())
                {
                    SelectContractTypeForm formscf = new SelectContractTypeForm();
                 //   formscf.Show();
                    Loginuserinset.IsLoggedIn = true;
                    Globals.ThisAddIn.reloadRibbon();
                }

                
            }
            this.Close();
        }
        public bool getContracttypemetadata(int ContractTypeId)
        {

            HttpClient clint = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            clint.BaseAddress = new Uri(Loginuserinset.url);
            HttpResponseMessage response = clint.GetAsync("ContractType/" + ContractTypeId).Result;

            var contracttyoedata = response.Content.ReadAsAsync<ContractType>().Result;
            string msg = string.Empty;
            Loginuserinset.Attributes = new List<Attributes>();
            foreach (var item in contracttyoedata.Attributes)
            {
                Loginuserinset.Attributes.Add(item);
            }
            return true;


        }
        public bool getcontracttype()
        {

            HttpClient clint = new HttpClient();
            clint.BaseAddress = new Uri(Loginuserinset.url);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


            HttpResponseMessage response = clint.GetAsync("ContractType/ContractTypeApproved?tenantId=" + Loginuserinset.tenantId).Result;
        

            var contracttyoedata = response.Content.ReadAsAsync<IEnumerable<ContractType>>().Result;
            string msg = string.Empty;
            Loginuserinset.LstComboitem = new List<ContractType>();
             foreach (var item in contracttyoedata)
            {
                ContractType c = new ContractType();
                c.Description = item.Name;
                c.ContractTypeId = item.ContractTypeId;
                

                Loginuserinset.LstComboitem.Add(c);
            }
            return true;

           
        }

        private void comboBox1selectServie_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void usernametextbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
