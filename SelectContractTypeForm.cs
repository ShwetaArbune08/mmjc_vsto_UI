using dragdrop;
using Mmjc_Vsto.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Windows.Forms;
using static Mmjc_Vsto.SelectTemplateForm;

namespace Mmjc_Vsto
{
    public partial class SelectContractTypeForm : Form
    {
        public SelectContractTypeForm()
        {
            InitializeComponent();
            foreach (var item in Loginuserinset.LstComboitem)
            {
                ComboboxItem itemComboboxItem = new ComboboxItem();
                itemComboboxItem.Text = item.assignmentName;
                itemComboboxItem.Value = item.id;
                Loginuserinset.url = "http://3.108.92.117:9992";
                comboBox2.Items.Add(itemComboboxItem);
                comboBox2.DisplayMember = "type"; // Property to display
                comboBox2.ValueMember = "id";
                textBox1.Text = Loginuserinset.tempName;
                richTextBox1.Text = Loginuserinset.templatesummary;
            }
        }

        public static dynamic _customTaskPanes;
        private void Button1_Click(object sender, EventArgs e)
        {
            ContractType g = new ContractType();
            
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri( Loginuserinset.url)
            };
            //AddTableToExistingWordDocument(client);
            string token = Loginuserinset.token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            // Update endpoint with the dynamic ID
            string endpoint = $"/mmjc/secretarial/attributes/{Loginuserinset.assignmentId}/{Loginuserinset.subAssignmentId}/1,{Loginuserinset.documentId}";
            HttpResponseMessage response = client.GetAsync(endpoint).Result;
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            var result = JsonSerializer.Deserialize<ApiResponseBody>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Extract the body
            var result1 = result.Body;

            foreach (var item in Globals.ThisAddIn.CustomTaskPanes)
            {
                item.Visible = false;
            }


            if (response != null)
            {
                this.Hide();
                Globals.ThisAddIn.CustomTaskPanes.Add(new OrdersListUserControl(
                              result1), "MetaData").Visible = true;

            }
            Loginuserinset.Attributes = new List<AssignmentDetails>();
            Loginuserinset.tempName = textBox1.Text;
            Loginuserinset.templatesummary = richTextBox1.Text;
            foreach (var item in result1)
            {
                Loginuserinset.Attributes.Add(item);
            }

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                var selectedItem = (ComboboxItem)comboBox2.SelectedItem;

                var selectedId = selectedItem.Value;
                LoadSubAssignments((int)selectedId);
                Loginuserinset.assignmentId = (int)selectedId;

            }

        }
        private void ComboBox1_Click(object sender, EventArgs e)
        {
            // Manually call the SelectedIndexChanged logic
            ComboBox1_SelectedIndexChanged(sender, e);
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                var selectedItem = (ComboboxItem)comboBox2.SelectedItem;
                int selectedId = (int)selectedItem.Value;

                // ✅ Load sub-assignments
                LoadSubAssignments(selectedId);

                // ✅ Load document types when an assignment is selected
                LoadDocumentTypes(selectedId);

                // ✅ Update assignment ID
                Loginuserinset.assignmentId = selectedId;
            }
        }

        private void LoadSubAssignments(int id)
        {
            try
            {
                var subAssignments = GetSubAssignments(id);

                comboBox1.DataSource = subAssignments;
                comboBox1.DisplayMember = "subAssignmentName"; // Property to display
                comboBox1.ValueMember = "Id";
                var selectedItem = (SubAssignmentList)comboBox1.SelectedItem;
                if(selectedItem != null)
                {
                    var selectedId = selectedItem.id;
                    Loginuserinset.subAssignmentId = (int)selectedId;
                }
                //Loginuserinset.assignmentId = id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }
        private List<SubAssignmentList> GetSubAssignments(int assignmentId)
        {
            // Example: Replace with logic to fetch sub-assignments from your data source
            var res = Loginuserinset.LstComboitem
                .Where(sub => sub.id == assignmentId).FirstOrDefault();

            return res.subAssignmentList;
        }
        private string GetAssignmentName(int assignmentId)
        {
            // Example: Replace with logic to fetch sub-assignments from your data source
            var res = Loginuserinset.LstComboitem
                .Where(sub => sub.id == assignmentId).FirstOrDefault();

            return res.assignmentName;
        }
        private void SelectContractTypeForm_Load(object sender, System.EventArgs e)
        {

        }

        private void SelectContractTypeForm_Load_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void ComboBox3_Click(object sender, EventArgs e)
        {
            // Manually call the SelectedIndexChanged logic
            ComboBox3_SelectedIndexChanged(sender, e);
        }
        private void LoadDocumentTypes(int id)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(Loginuserinset.url)
                };

                string token = Loginuserinset.token;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                var assignmentName = GetAssignmentName(id);

                string endpoint = $"/mmjc/secretarial/documentTypes/{assignmentName}";
                HttpResponseMessage response = client.GetAsync(endpoint).Result;

                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                var apiResponse = JsonSerializer.Deserialize<ApiResponseForDocument>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var result = apiResponse?.Body?.ToList(); // Ensure it's a fresh list

                // ✅ Set DataSource *before* setting SelectedIndex
                comboBox3.DataSource = result;
                comboBox3.DisplayMember = "Type";
                comboBox3.ValueMember = "Id";

                if (result != null && result.Count > 0)
                {
                    comboBox3.SelectedIndex = -1;  // ✅ Reset selection initially
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }


        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem is DocumentInfo selectedItem)
            {
                int selectedId = selectedItem.Id; // Ensure 'Id' is the correct property
                string documentType = selectedItem.Type; // Ensure 'Type' is the correct property
            }
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }
    }
}
