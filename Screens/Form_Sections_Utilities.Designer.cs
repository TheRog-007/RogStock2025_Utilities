namespace RogStock2025_Utilities.Screens
{
    partial class frmSections_Utilities
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
            STBStatus = new StatusStrip();
            STLStatus = new ToolStripStatusLabel();
            BTNDelete = new Button();
            panel1 = new Panel();
            BTNNew = new Button();
            CMBSEC_Area = new ComboBox();
            label1 = new Label();
            BTNSave = new Button();
            BTNClose = new Button();
            STBStatus.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // STBStatus
            // 
            STBStatus.Items.AddRange(new ToolStripItem[] { STLStatus });
            STBStatus.Location = new Point(0, 137);
            STBStatus.Name = "STBStatus";
            STBStatus.Size = new Size(384, 22);
            STBStatus.TabIndex = 16;
            STBStatus.Text = "statusStrip1";
            // 
            // STLStatus
            // 
            STLStatus.Name = "STLStatus";
            STLStatus.Size = new Size(0, 17);
            // 
            // BTNDelete
            // 
            BTNDelete.Location = new Point(117, 99);
            BTNDelete.Margin = new Padding(4, 3, 4, 3);
            BTNDelete.Name = "BTNDelete";
            BTNDelete.Size = new Size(88, 27);
            BTNDelete.TabIndex = 13;
            BTNDelete.Text = "Delete";
            BTNDelete.UseVisualStyleBackColor = true;
            BTNDelete.Click += BTNDelete_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(BTNNew);
            panel1.Controls.Add(CMBSEC_Area);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(13, 12);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(352, 63);
            panel1.TabIndex = 0;
            panel1.TabStop = true;
            // 
            // BTNNew
            // 
            BTNNew.Location = new Point(285, 21);
            BTNNew.Margin = new Padding(4, 3, 4, 3);
            BTNNew.Name = "BTNNew";
            BTNNew.Size = new Size(46, 23);
            BTNNew.TabIndex = 1;
            BTNNew.Text = "New";
            BTNNew.UseVisualStyleBackColor = true;
            BTNNew.Click += BTNNew_Click;
            // 
            // CMBSEC_Area
            // 
            CMBSEC_Area.FormattingEnabled = true;
            CMBSEC_Area.Location = new Point(106, 20);
            CMBSEC_Area.Margin = new Padding(4, 3, 4, 3);
            CMBSEC_Area.Name = "CMBSEC_Area";
            CMBSEC_Area.Size = new Size(144, 23);
            CMBSEC_Area.Sorted = true;
            CMBSEC_Area.TabIndex = 0;
            CMBSEC_Area.Tag = "1";
            CMBSEC_Area.SelectedValueChanged += CMBSections_SelectedValueChanged;
            CMBSEC_Area.KeyDown += CMBSections_KeyDown;
            CMBSEC_Area.Leave += CMBSEC_Area_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 7;
            label1.Text = "Section:";
            // 
            // BTNSave
            // 
            BTNSave.Location = new Point(13, 99);
            BTNSave.Margin = new Padding(4, 3, 4, 3);
            BTNSave.Name = "BTNSave";
            BTNSave.Size = new Size(88, 27);
            BTNSave.TabIndex = 12;
            BTNSave.Text = "Save";
            BTNSave.UseVisualStyleBackColor = true;
            BTNSave.Click += BTNSave_Click;
            // 
            // BTNClose
            // 
            BTNClose.DialogResult = DialogResult.Cancel;
            BTNClose.Location = new Point(276, 99);
            BTNClose.Margin = new Padding(4, 3, 4, 3);
            BTNClose.Name = "BTNClose";
            BTNClose.Size = new Size(88, 27);
            BTNClose.TabIndex = 14;
            BTNClose.Text = "Close";
            BTNClose.UseVisualStyleBackColor = true;
            BTNClose.Click += BTNClose_Click;
            // 
            // frmSections_Utilities
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 159);
            Controls.Add(STBStatus);
            Controls.Add(BTNDelete);
            Controls.Add(panel1);
            Controls.Add(BTNSave);
            Controls.Add(BTNClose);
            Name = "frmSections_Utilities";
            Text = "Sections Maintenance";
            Load += frmSections_Utilities_Load;
            STBStatus.ResumeLayout(false);
            STBStatus.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip STBStatus;
        private ToolStripStatusLabel STLStatus;
        private Button BTNDelete;
        private Panel panel1;
        private Button BTNNew;
        private ComboBox CMBSEC_Area;
        private Label label1;
        private Button BTNSave;
        private Button BTNClose;
    }
}