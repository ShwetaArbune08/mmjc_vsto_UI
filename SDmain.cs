using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using System.Web.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using dragdrop;
using Microsoft.Office.Core;
using Mmjc_Vsto.models;

namespace Mmjc_Vsto
{



    [ComVisible(true)]
    public class SDmain : Office.IRibbonExtensibility
    {
        public Office.IRibbonUI ribbon;

        public SDmain()
        {
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("Sundeus.SDmain.xml");
        }

        #endregion


        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
            Globals.ThisAddIn.ribbon = ribbonUI;
        }

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion

        public void btAbout_Click(Office.IRibbonControl control)
        {
            Globals.ThisAddIn.Application.Documents.Open("https://localhost:44307/api/GenerateDocument/DownloadTemplate?Templateid=9");
            //  About _abtus = new About();
            //    _abtus.Show();
        }
        public Image GetImages(Office.IRibbonControl control)
        {
            Bitmap bmp;
            if (control.Id == "btLoginLogout")
            {
                bmp = new Bitmap(Resourceimage.login);

            }
            else if (control.Id == "Template")
            {
                bmp = new Bitmap(Resourceimage.template);

            }
            else if (control.Id == "btclauses")
            {
                bmp = new Bitmap(Resourceimage.clauses);

            }

            else if (control.Id == "btnSelectContractType")
            {
                bmp = new Bitmap(Resourceimage.contract);

            }
            else if (control.Id == "btuploaddoc")
            {
                bmp = new Bitmap(Resourceimage.upload);

            }
            else
            {
                bmp = new Bitmap(Resourceimage.sun);

            }
            return bmp;

        }

        public void btLoginLogout_Click(Office.IRibbonControl control)
        {
            if (!Loginuserinset.IsLoggedIn)
            {
                Loginuserinset.IsLoggedIn = true;
                LoginForm lf = new LoginForm();
                //  lf.FormClosed += MainPage_FormClosed();
                lf.Show();
                //     ribbon.Invalidate();
            }
            else
            {
                Loginuserinset.IsLoggedIn = false;

                ribbon.Invalidate();
            }

        }



        private void MainPage_FormClosing(object sender, FormClosedEventArgs e)
        {
            // your code here to do something before closing the form
        }
        public bool getenabled(Office.IRibbonControl control)
        {

            return Loginuserinset.IsLoggedIn;
        }
        public void btnSelectContractType_Click(Office.IRibbonControl control)
        {
            SelectContractTypeForm formscf = new SelectContractTypeForm();
            formscf.Show();
        }
        public void btnSelectTemplate_Click(Office.IRibbonControl control)
        {
            SelectTemplateForm formContractTypeForm = new SelectTemplateForm();
            formContractTypeForm.Show();
        }
        //btnSelectTemplate_Click
        public bool getEnabled(Office.IRibbonControl control)
        {
            return false;
        }
        public string getLabel(Office.IRibbonControl control)
        {
            if (Loginuserinset.IsLoggedIn)
            {
                return "Logout";
            }
            return "Login";
        }
        //  private Microsoft.Office.Tools.Word.RichTextContentControl richTextControl1;  btaddsignhere


        public void btMapTo_Click(Office.IRibbonControl control)
        {

            MapTo _maptoObj = new MapTo();
            _maptoObj.Show();
            Globals.ThisAddIn.AddRichTextControlAtSelection("TEST using contest menu");
        }
        //btListOfAllClauses_Click
        public void btListOfAllClauses_Click(Office.IRibbonControl control)
        {
            if (Loginuserinset.templatename == null)
            {
                if (Loginuserinset.seletedtemplateid != null)
                {
                    getClausesmetadata();
                    Globals.ThisAddIn.CustomTaskPanes.Add(new ClausesListUserControl(
                        Loginuserinset.clauseListdragdrop), "ClausesListforcontracttype").Visible = true;


                }
                else
                {
                    MessageBox.Show("Please Select Template First");

                }
                return;
            }
            getClausesmetadata();
            Globals.ThisAddIn.CustomTaskPanes.Add(new ClausesListUserControl(
                          Loginuserinset.clauseListdragdrop), "ClausesListforcontracttype").Visible = true;

        }
        //btNMetadataList_Click
        public bool getContracttypemetadata(int ContractTypeId)
        {

            HttpClient clint = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            clint.BaseAddress = new Uri(Loginuserinset.url);
            HttpResponseMessage response = clint.GetAsync("ContractType/" + ContractTypeId).Result;

            var contracttyoedata = response.Content.ReadAsAsync<ContractType>().Result;
            string msg = string.Empty;
            //Loginuserinset.Attributes = new List<Attributes>();
            //foreach (var item in contracttyoedata.Attributes)
            //{
            //    Loginuserinset.Attributes.Add(item);
            //}
            return true;


        }

        public void btEditClauses_Click(Office.IRibbonControl control)
        {
            if (Loginuserinset.templatename == null)
            {
                MessageBox.Show("Please Select Template First");
                return;
            }

            ClausesName _clausesname = new ClausesName(Globals.ThisAddIn.Application.Selection.Text);


            _clausesname.Show();
        }

        public void btClauses_Click(Office.IRibbonControl control)
        {
            if (Loginuserinset.templatename == null)
            {
                MessageBox.Show("Please Select Template First");
                return;
            }

            ClausesName _clausesname = new ClausesName(Globals.ThisAddIn.Application.Selection.Text);


            _clausesname.Show();

        }

        public void btaddsignhere(Office.IRibbonControl control)
        {

            Word.Range currentRange = Globals.ThisAddIn.Application.Selection.Range;
            currentRange.Text = "Signhere";
            int i = (int)currentRange.get_Information(WdInformation.wdActiveEndPageNumber);
            int x = (int)currentRange.get_Information(WdInformation.wdHorizontalPositionRelativeToPage);
            int y = (int)currentRange.get_Information(WdInformation.wdVerticalPositionRelativeToPage);


            Loginuserinset.signnumber = 1;
            Loginuserinset.coordinates = new List<coordinates>();
            List<coordinates> _co = new List<coordinates>();
            coordinates _C = new coordinates();
            _C.X = x;
            _C.Y = y;
            _C.PageNumber = i;
            _C.Number = 1;
            _co.Add(_C);
            Loginuserinset.coordinates = _co;


        }
        public void btUploadDoc_Click(Office.IRibbonControl control)
        {
            var filePath = Globals.ThisAddIn.Application.ActiveDocument.Path;
            if (String.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please Save the Document First and then upload");
                Globals.ThisAddIn.Application.ActiveDocument.Save();

                return;
            }
            var name = Globals.ThisAddIn.Application.ActiveDocument.Name;
            if (String.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please Save the Document First and then upload");
                return;
            }
            var finalpath = Path.Combine(filePath, name);
            try
            {
                TemplateDto _up = new TemplateDto();
                _up.Name = Loginuserinset.tempName;
                _up.AssignmentMasterId = Loginuserinset.assignmentId;
                _up.Status = true;
                _up.Description = Loginuserinset.templatesummary;
                _up.Version = 1;
                _up.DocumentId = Loginuserinset.documentId.ToString();
                _up.DocTypeId = 2;
                _up.CreatedBy = 1;
                _up.CreatedDate = DateTime.Now;
                _up.UpdatedDate = DateTime.Now;
                _up.UpdatedBy = 1;
                var tepm = PostingtemplateAsync(_up, finalpath, name);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        static async Task<bool> PostingtemplateAsync(TemplateDto uploadmodeslobj, string filepath, string name)
        {
            try
            {
                if (!File.Exists(filepath))
                {
                    Console.WriteLine("File does not exist.");
                    return false;
                }
                FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                using (fs)
                {
                    IFormFile filesa = new FormFile(fs, 0, fs.Length, null, name)
                    {
                        Headers = new HeaderDictionary()
                    };
                    var status = Posttemp(filesa, uploadmodeslobj).Result;
                    if (status)
                    {
                        MessageBox.Show("Uploaded Succesfully");

                    }
                    else
                    {
                        MessageBox.Show("not Uploaded ");

                    }
                }
            }
            catch (IOException X)
            {
                throw X;
                //it failed...handle accordingly..error details kept in X
            }
            catch (Exception e)
            {

                throw e;
            }




            return true;


        }

        public static async Task<bool> Posttemp(IFormFile file, TemplateDto uploadModelObj)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    Console.WriteLine("Invalid file.");
                    return false;
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7233/");

                    // Read file into byte array
                    byte[] data;
                    using (var br = new BinaryReader(file.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)file.Length);
                    }

                    // Prepare multipart content
                    MultipartFormDataContent multiContent = new MultipartFormDataContent();

                    // Add file content (name must match `CreateTemplateViewModel.File`)
                    ByteArrayContent fileContent = new ByteArrayContent(data);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    multiContent.Add(fileContent, "File", file.FileName);

                    // Add TemplateDto object (name must match `CreateTemplateViewModel.template`)
                    var templateContent = new StringContent(JsonConvert.SerializeObject(uploadModelObj));
                    templateContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    multiContent.Add(templateContent, "template"); // The key must match the server-side property name


                    // Call the API
                    var response = await client.PostAsync("api/template/create", multiContent);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Template created successfully.");
                        return true;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error: {response.StatusCode}, Details: {errorContent}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }



        public bool Ribbonreload(string id, bool all)
        {

            try
            {
                if (!all)
                {
                    ribbon.InvalidateControl(id);

                }
                else
                {

                }

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public bool getClausesmetadata()
        {

            HttpClient clint = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            clint.BaseAddress = new Uri(Loginuserinset.url);
            HttpResponseMessage response = clint.GetAsync("Clauses/GetAllClausesWithoutPaging?contracttype=" + Loginuserinset.seletedcontractId).Result;

            var clausesdata = response.Content.ReadAsAsync<dynamic>().Result;
            Loginuserinset.clauseListdragdrop = new List<Clauses>();
            string msg = string.Empty;
            foreach (var item in clausesdata)
            {

                Clauses clauses = new Clauses();
                clauses.ClauseId = item.clauseId;
                clauses.Name = item.name;

                clauses.Text = item.text;

                clauses.Description = item.description;




                Loginuserinset.clauseListdragdrop.Add(clauses);
            }
            return true;


        }
    }

}
