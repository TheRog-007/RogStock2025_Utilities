namespace RogStock2025_Utilities.Screens
{
    partial class frmLoginMaintenance
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
            panel1 = new Panel();
            BTNShowPassword = new Button();
            BTNNew = new Button();
            CMBUser = new ComboBox();
            TXTPassword = new TextBox();
            label2 = new Label();
            label1 = new Label();
            BTNSave = new Button();
            BTNClose = new Button();
            BTNDelete = new Button();
            STBStatus = new StatusStrip();
            STLStatus = new ToolStripStatusLabel();
            panel1.SuspendLayout();
            STBStatus.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(BTNShowPassword);
            panel1.Controls.Add(BTNNew);
            panel1.Controls.Add(CMBUser);
            panel1.Controls.Add(TXTPassword);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(14, 14);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(352, 95);
            panel1.TabIndex = 5;
            panel1.TabStop = true;
            // 
            // BTNShowPassword
            // 
            BTNShowPassword.Location = new Point(285, 53);
            BTNShowPassword.Name = "BTNShowPassword";
            BTNShowPassword.Size = new Size(46, 24);
            BTNShowPassword.TabIndex = 9;
            BTNShowPassword.Text = "Show";
            BTNShowPassword.UseVisualStyleBackColor = true;
            BTNShowPassword.Click += BTNShowPassword_Click;
            // 
            // BTNNew
            // 
            BTNNew.Location = new Point(285, 17);
            BTNNew.Margin = new Padding(4, 3, 4, 3);
            BTNNew.Name = "BTNNew";
            BTNNew.Size = new Size(46, 23);
            BTNNew.TabIndex = 0;
            BTNNew.Text = "New";
            BTNNew.UseVisualStyleBackColor = true;
            BTNNew.Click += BTNNew_Click;
            // 
            // CMBUser
            // 
            CMBUser.FormattingEnabled = true;
            CMBUser.Location = new Point(117, 16);
            CMBUser.Margin = new Padding(4, 3, 4, 3);
            CMBUser.Name = "CMBUser";
            CMBUser.Size = new Size(144, 23);
            CMBUser.Sorted = true;
            CMBUser.TabIndex = 1;
            CMBUser.Tag = "1";
            CMBUser.SelectedIndexChanged += CMBUser_SelectedIndexChanged;
            CMBUser.Leave += CMBUser_Leave;
            // 
            // TXTPassword
            // 
            TXTPassword.Location = new Point(117, 53);
            TXTPassword.Margin = new Padding(4, 3, 4, 3);
            TXTPassword.MaxLength = 10;
            TXTPassword.Name = "TXTPassword";
            TXTPassword.PasswordChar = '*';
            TXTPassword.Size = new Size(116, 23);
            TXTPassword.TabIndex = 2;
            TXTPassword.Tag = "1";
            TXTPassword.Leave += TXTPassword_Leave;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 53);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 8;
            label2.Text = "Password:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 7;
            label1.Text = "User Name:";
            // 
            // BTNSave
            // 
            BTNSave.Location = new Point(14, 134);
            BTNSave.Margin = new Padding(4, 3, 4, 3);
            BTNSave.Name = "BTNSave";
            BTNSave.Size = new Size(88, 27);
            BTNSave.TabIndex = 3;
            BTNSave.Text = "Save";
            BTNSave.UseVisualStyleBackColor = true;
            BTNSave.Click += BTNSave_Click;
            // 
            // BTNClose
            // 
            BTNClose.DialogResult = DialogResult.Cancel;
            BTNClose.Location = new Point(277, 134);
            BTNClose.Margin = new Padding(4, 3, 4, 3);
            BTNClose.Name = "BTNClose";
            BTNClose.Size = new Size(88, 27);
            BTNClose.TabIndex = 5;
            BTNClose.Text = "Close";
            BTNClose.UseVisualStyleBackColor = true;
            BTNClose.Click += BTNClose_Click;
            // 
            // BTNDelete
            // 
            BTNDelete.Location = new Point(118, 134);
            BTNDelete.Margin = new Padding(4, 3, 4, 3);
            BTNDelete.Name = "BTNDelete";
            BTNDelete.Size = new Size(88, 27);
            BTNDelete.TabIndex = 4;
            BTNDelete.Text = "Delete";
            BTNDelete.UseVisualStyleBackColor = true;
            BTNDelete.Click += BTNDelete_Click;
            // 
            // STBStatus
            // 
            STBStatus.Items.AddRange(new ToolStripItem[] { STLStatus });
            STBStatus.Location = new Point(0, 173);
            STBStatus.Name = "STBStatus";
            STBStatus.Size = new Size(385, 22);
            STBStatus.TabIndex = 6;
            STBStatus.Text = "statusStrip1";
            // 
            // STLStatus
            // 
            STLStatus.Name = "STLStatus";
            STLStatus.Size = new Size(0, 17);
            // 
            // frmLoginMaintenance
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(385, 195);
            Controls.Add(STBStatus);
            Controls.Add(BTNDelete);
            Controls.Add(panel1);
            Controls.Add(BTNSave);
            Controls.Add(BTNClose);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmLoginMaintenance";
            Text = "Login Maintenance";
            Load += frmLoginMaintenance_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            STBStatus.ResumeLayout(false);
            STBStatus.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TXTPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.ComboBox CMBUser;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Button BTNNew;
        private Button BTNShowPassword;
        private StatusStrip STBStatus;
        private ToolStripStatusLabel STLStatus;
    }
}