namespace Mmjc_Vsto
{
    partial class SelectTemplateForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1listcontracttype = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxtemplate1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Contract type";
            // 
            // comboBox1listcontracttype
            // 
            this.comboBox1listcontracttype.FormattingEnabled = true;
            this.comboBox1listcontracttype.Location = new System.Drawing.Point(175, 47);
            this.comboBox1listcontracttype.Name = "comboBox1listcontracttype";
            this.comboBox1listcontracttype.Size = new System.Drawing.Size(249, 21);
            this.comboBox1listcontracttype.TabIndex = 1;
            this.comboBox1listcontracttype.SelectedIndexChanged += new System.EventHandler(this.comboBox1listcontracttype_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Template";
            // 
            // comboBoxtemplate1
            // 
            this.comboBoxtemplate1.FormattingEnabled = true;
            this.comboBoxtemplate1.Location = new System.Drawing.Point(175, 101);
            this.comboBoxtemplate1.Name = "comboBoxtemplate1";
            this.comboBoxtemplate1.Size = new System.Drawing.Size(249, 21);
            this.comboBoxtemplate1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(230, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Next";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Next_Click);
            // 
            // SelectTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 223);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxtemplate1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1listcontracttype);
            this.Controls.Add(this.label1);
            this.Name = "SelectTemplateForm";
            this.Text = "SelectTemplateForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1listcontracttype;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxtemplate1;
        private System.Windows.Forms.Button button1;
    }
}