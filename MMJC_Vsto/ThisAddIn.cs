using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using dragdrop;

namespace Sundeus
{
    public partial class ThisAddIn
    {

        // pub
        public Office.IRibbonUI ribbon;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Globals.ThisAddIn.CustomTaskPanes.Add(new OrdersListUserControl(), "MetaData").Visible = false;
            Globals.ThisAddIn.CustomTaskPanes.Add(new ClausesListUserControl(), "ClauseList").Visible = false;
            Globals.ThisAddIn.Application.WindowActivate += new Word.ApplicationEvents4_WindowActivateEventHandler(Application_WindowActivate);

            Loginuserinset.IsLoggedIn = false;
            Loginuserinset.LstClauses = new List<models.Clauses>();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new SDmain();
        }

        #endregion

        //   private Microsoft.Office.Tools.Word.RichTextContentControl richTextControl1;
        public static int count;
        //public void AddRichTextControlAtSelection(string _data)
        //{

        //    Microsoft.Office.Tools.Word.RichTextContentControl richTextControl1;
        //    Word.Document currentDocument = this.Application.ActiveDocument;
        //    currentDocument.Paragraphs[1].Range.InsertParagraphBefore();
        //    currentDocument.Paragraphs[1].Range.Select();
        //    Document extendedDocument = Globals.Factory.GetVstoObject(currentDocument);

        //    richTextControl1 = extendedDocument.Controls.AddRichTextContentControl("richTextControl1");
        //    richTextControl1.PlaceholderText = _data;
        //}
        int county = 0;
        List<RichTextContentControl> richTextControls;
        public void AddRichTextControlAtSelection(string _data)
        {
            Word.Document currentDocument = Globals.ThisAddIn.Application.ActiveDocument;

            Document extendedDocument = Globals.Factory.GetVstoObject(currentDocument);

            if (currentDocument.ContentControls.Count > 0)
            {

                currentDocument.ActiveWindow.Selection.Range.HighlightColorIndex = Word.WdColorIndex.wdYellow;
                currentDocument.ActiveWindow.Selection.Range.Select();

                richTextControls = new List<RichTextContentControl>();

                foreach (Word.ContentControl nativeControl in currentDocument.ContentControls)
                {
                    if (nativeControl.Type == Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText)
                    {
                        county++;
                        RichTextContentControl tempControl = extendedDocument.Controls.AddRichTextContentControl("VSTORichTextControl" + county.ToString());
                        richTextControls.Add(tempControl);
                        tempControl.Title = _data;
                        tempControl.Tag = "metadata";
                        tempControl.PlaceholderText = _data;
                        break;
                    }
                }
            }

            else
            {
                RichTextContentControl VSTORichTextControl;
                VSTORichTextControl = extendedDocument.Controls.AddRichTextContentControl("VSTORichTextControl");
                VSTORichTextControl.PlaceholderText = _data;
                VSTORichTextControl.Title = _data;
                VSTORichTextControl.Tag = "metadata";
            }

        }

        public void AddRichTextControlAtSelectiontemplate(string _data, string name, string id, string tagtype)
        {
            Word.Document currentDocument = Globals.ThisAddIn.Application.ActiveDocument;

            Document extendedDocument = Globals.Factory.GetVstoObject(currentDocument);

            if (currentDocument.ContentControls.Count > 0)
            {

                //   currentDocument.ActiveWindow.Selection.Range.HighlightColorIndex = Word.WdColorIndex.wdYellow;
                currentDocument.ActiveWindow.Selection.Range.Select();

                richTextControls = new List<RichTextContentControl>();

                foreach (Word.ContentControl nativeControl in currentDocument.ContentControls)
                {
                    if (nativeControl.Type == Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText)
                    {
                        county++;
                        RichTextContentControl tempControl = extendedDocument.Controls.AddRichTextContentControl("VSTORichTextControl" + county.ToString());
                        richTextControls.Add(tempControl);
                        tempControl.Tag = tagtype;
                        tempControl.Title = id;
                        tempControl.PlaceholderText = _data;


                        if (tagtype.ToUpper() == "CLAUSE")
                        {
                            tempControl.Title = name;
                        }


                        break;
                    }
                }
            }

            else
            {
                RichTextContentControl VSTORichTextControl;
                VSTORichTextControl = extendedDocument.Controls.AddRichTextContentControl("VSTORichTextControl");
                VSTORichTextControl.PlaceholderText = _data;
                VSTORichTextControl.Title = id;
                if (tagtype.ToUpper() == "CLAUSE")
                {
                    VSTORichTextControl.Title = name;

                }

                VSTORichTextControl.Tag = tagtype;
                // VSTORichTextControl.at = _data;

            }

        }
        /// <summary>
        /// Called when [drop occurred].
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="data">The data.</param>
        public void OnDropOccurred(int x, int y, object data, string ide)
        {
            //Get the Word range from the form's point location 
            Microsoft.Office.Interop.Word.Range range = (Microsoft.Office.Interop.Word.Range)Globals.ThisAddIn.Application.ActiveWindow.RangeFromPoint(x, y);
            //Insert a dummy details table for the selected order
            //Word.Table table=this.Application.ActiveDocument.Tables.Add(range,4,4);

            //Word = this.Application.ActiveDocument.Tables.Add(range, 4, 4);
            //         table.Range.Font.Size = 8;
            //         table.set_Style("Table Grid 8");
            //         table.Rows[1].Cells[1].Range.Text = "Order Details";
            //         table.Rows[1].Cells[2].Range.Text = "Order Details";
            //         table.Rows[1].Cells[3].Range.Text = "Order Details";
            //         table.Rows[1].Cells[4].Range.Text = "Order Details";
            //         for (int i = 2; i < 5; i++)
            //{
            //             for (int j = 1; j < 5; j++)
            //             {
            //                 table.Rows[i].Cells[j].Range.Text = data.ToString();    
            //             }

            //}
            //    AddRichTextControlAtSelection textBox2_DragDrop
            //  string attributeName = "%" + data.ToString() + "%";
            int id = 0;
            foreach (var item in Loginuserinset.Attributes)
            {

                if (item.Label == data)
                {
                    id = item.AttributeId;

                }
            }

            AddRichTextControlAtSelectiontemplate(data.ToString(), data.ToString(), id.ToString(), "Metadata");


            //AddRichTextControlAtSelection(data.ToString());



        }
        public void OnDropOccurredclauses(int x, int y, object data)
        {
            //Get the Word range from the form's point location 
            Microsoft.Office.Interop.Word.Range range = (Microsoft.Office.Interop.Word.Range)Globals.ThisAddIn.Application.ActiveWindow.RangeFromPoint(x, y);
            //Insert a dummy details table for the selected order
            //Word.Table table=this.Application.ActiveDocument.Tables.Add(range,4,4);

            //Word = this.Application.ActiveDocument.Tables.Add(range, 4, 4);
            //         table.Range.Font.Size = 8;
            //         table.set_Style("Table Grid 8");
            //         table.Rows[1].Cells[1].Range.Text = "Order Details";
            //         table.Rows[1].Cells[2].Range.Text = "Order Details";
            //         table.Rows[1].Cells[3].Range.Text = "Order Details";
            //         table.Rows[1].Cells[4].Range.Text = "Order Details";
            //         for (int i = 2; i < 5; i++)
            //{
            //             for (int j = 1; j < 5; j++)
            //             {
            //                 table.Rows[i].Cells[j].Range.Text = data.ToString();    
            //             }

            //}
            //    AddRichTextControlAtSelection 
            string attributeName = data.ToString()
                ;
            int clauseid = 0;
            foreach (var item in Loginuserinset.clauseListdragdrop)
            {

                if (item.Name == data)
                {
                    attributeName = item.Text;
                    clauseid = item.ClauseId;

                }
            }
            //
            models.Clauses Clausesobj = new models.Clauses();

            Clausesobj.ClauseId = clauseid;
            //new clause addition is here

            Loginuserinset.LstClauses.Add(Clausesobj);



            AddRichTextControlAtSelectiontemplate(attributeName, data.ToString(), clauseid.ToString(), "Clause");



        }
        public OverlayFormClauses OverlayFormClauses { get; set; }

        public OverlayForm OverlayForm { get; set; }
        void Application_WindowActivate(Word.Document Doc, Word.Window Wn)
        {
            if (OverlayForm != null && OverlayForm.Visible)

                OverlayForm.Opacity = 100;


        }

        public void opentemplate()
        {
            this.Application.Documents.Open("https://localhost:44307/api/GenerateDocument/DownloadTemplate?Templateid=9");

        }


        public void reloadRibbon()
        {
            ribbon.Invalidate();
            // return null;
        }
    }
}
