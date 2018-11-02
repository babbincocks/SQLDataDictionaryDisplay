namespace SQLDataDictionaryDisplay
{
    partial class frmMain
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
            this.cbDatabases = new System.Windows.Forms.ComboBox();
            this.lbDBItems = new System.Windows.Forms.ListBox();
            this.lbDepending = new System.Windows.Forms.ListBox();
            this.lbDependents = new System.Windows.Forms.ListBox();
            this.lblDepending = new System.Windows.Forms.Label();
            this.lblDependent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbDatabases
            // 
            this.cbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabases.FormattingEnabled = true;
            this.cbDatabases.Location = new System.Drawing.Point(29, 24);
            this.cbDatabases.Name = "cbDatabases";
            this.cbDatabases.Size = new System.Drawing.Size(243, 21);
            this.cbDatabases.TabIndex = 0;
            this.cbDatabases.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lbDBItems
            // 
            this.lbDBItems.FormattingEnabled = true;
            this.lbDBItems.Location = new System.Drawing.Point(29, 69);
            this.lbDBItems.Name = "lbDBItems";
            this.lbDBItems.Size = new System.Drawing.Size(295, 394);
            this.lbDBItems.TabIndex = 1;
            this.lbDBItems.SelectedIndexChanged += new System.EventHandler(this.lbDBItems_SelectedIndexChanged);
            // 
            // lbDepending
            // 
            this.lbDepending.FormattingEnabled = true;
            this.lbDepending.HorizontalScrollbar = true;
            this.lbDepending.Location = new System.Drawing.Point(404, 69);
            this.lbDepending.Name = "lbDepending";
            this.lbDepending.Size = new System.Drawing.Size(527, 173);
            this.lbDepending.TabIndex = 2;
            // 
            // lbDependents
            // 
            this.lbDependents.FormattingEnabled = true;
            this.lbDependents.HorizontalScrollbar = true;
            this.lbDependents.Location = new System.Drawing.Point(404, 290);
            this.lbDependents.Name = "lbDependents";
            this.lbDependents.Size = new System.Drawing.Size(527, 173);
            this.lbDependents.TabIndex = 3;
            // 
            // lblDepending
            // 
            this.lblDepending.AutoSize = true;
            this.lblDepending.Location = new System.Drawing.Point(401, 53);
            this.lblDepending.Name = "lblDepending";
            this.lblDepending.Size = new System.Drawing.Size(0, 13);
            this.lblDepending.TabIndex = 4;
            // 
            // lblDependent
            // 
            this.lblDependent.AutoSize = true;
            this.lblDependent.Location = new System.Drawing.Point(401, 274);
            this.lblDependent.Name = "lblDependent";
            this.lblDependent.Size = new System.Drawing.Size(0, 13);
            this.lblDependent.TabIndex = 5;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 508);
            this.Controls.Add(this.lblDependent);
            this.Controls.Add(this.lblDepending);
            this.Controls.Add(this.lbDependents);
            this.Controls.Add(this.lbDepending);
            this.Controls.Add(this.lbDBItems);
            this.Controls.Add(this.cbDatabases);
            this.Name = "frmMain";
            this.Text = "Database Dependencies";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDatabases;
        private System.Windows.Forms.ListBox lbDBItems;
        private System.Windows.Forms.ListBox lbDepending;
        private System.Windows.Forms.ListBox lbDependents;
        private System.Windows.Forms.Label lblDepending;
        private System.Windows.Forms.Label lblDependent;
    }
}

