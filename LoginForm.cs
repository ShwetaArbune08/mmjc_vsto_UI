using Mmjc_Vsto.models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace Mmjc_Vsto
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var token = "";
            {

                Loginuserinset.username = usernametextbox.Text;

                Loginuserinset.url = comboBox1selectServie.Text;
                Loginuserinset.password = textBox1.Text;
                Loginuserinset.tenantId = Convert.ToInt32(textBox2.Text);
                using (HttpClient client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Loginuserinset.url);
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    var requestData = new
                    {
                        username = Loginuserinset.username,
                        password = Loginuserinset.password
                    };

                    string jsonData = System.Text.Json.JsonSerializer.Serialize(requestData);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Make the POST call
                    HttpResponseMessage response = client.PostAsync("login", content).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;

                        // Deserialize the JSON response to a dynamic object to focus only on the message
                        var jsonDocument = JsonDocument.Parse(jsonResponse);
                        token = jsonDocument.RootElement.GetProperty("body").GetProperty("token").GetString();

                        var message = jsonDocument.RootElement.GetProperty("message").GetString();

                        if (!string.IsNullOrEmpty(message) && message.Contains("user logged in"))
                        {
                            //MessageBox.Show($"Message: {message}", "API Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Loginuserinset.IsLoggedIn = true;
                            Globals.ThisAddIn.reloadRibbon();
                        }
                        else
                        {
                            MessageBox.Show("Error During Login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"HTTP Error: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (getcontracttype(token))
                {
                    SelectContractTypeForm formscf = new SelectContractTypeForm();
                    //   formscf.Show();
                    Loginuserinset.IsLoggedIn = true;
                    Loginuserinset.token = token;
                    Globals.ThisAddIn.reloadRibbon();
                }


            }
            this.Close();
        }
        

        public class ApiResponse
        {
            public string Message { get; set; }
            public Body Body { get; set; }
            public int Status { get; set; }
        }
        public bool getcontracttype(string token)
        {

            // Initialize HttpClient
            HttpClient client = new HttpClient();

            // Set Base Address
            client.BaseAddress = new Uri("http://3.108.92.117:9992");

            // Add Authorization Header
            string tokenBarer = token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenBarer);

            // Accept Header
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            // Endpoint
            string endpoint = "/mmjc/assignments/assignmentRepository/assignments";


            HttpResponseMessage response = client.GetAsync(endpoint).Result;


            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            var result = System.Text.Json.JsonSerializer.Deserialize<ApiResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Extract the body list
            Loginuserinset.LstComboitem = result.Body.data;

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
