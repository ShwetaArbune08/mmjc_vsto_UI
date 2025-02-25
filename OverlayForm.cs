using Mmjc_Vsto;
using System;
using System.Windows.Forms;

namespace dragdrop
{
    public partial class OverlayForm : Form
    {
        public OverlayForm()
        {
            InitializeComponent();
        }
        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            string text = e.Data.GetData(DataFormats.Text).ToString();
            this.Hide();
            Globals.ThisAddIn.OnDropOccurred(e.X, e.Y, e.Data.GetData(DataFormats.Text).ToString(), e.Data.GetData(DataFormats.Text).ToString());
        }

        private void textBox2_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                // Get the range from the point
                Microsoft.Office.Interop.Word.Range range = (Microsoft.Office.Interop.Word.Range)Globals.ThisAddIn.Application.ActiveWindow.RangeFromPoint(e.X, e.Y);

                if (range != null)
                {
                    range.Select(); // Select the range if it's valid
                }
                else
                {
                    Console.WriteLine("Range is null. The point might be outside the document content.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during DragOver: {ex.Message}");
            }
        }
    }
}