namespace RogStock2025_Utilities.Screens
{
    partial class frmThemeMaintenance
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
            BTNUndo = new Button();
            STBStatus = new StatusStrip();
            STLStatus = new ToolStripStatusLabel();
            BTNDelete = new Button();
            BTNSave = new Button();
            BTNClose = new Button();
            BTNNew = new Button();
            CMBControlType = new ComboBox();
            label1 = new Label();
            STBStatus.SuspendLayout();
            SuspendLayout();
            // 
            // BTNUndo
            // 
            BTNUndo.Location = new Point(218, 378);
            BTNUndo.Margin = new Padding(4, 3, 4, 3);
            BTNUndo.Name = "BTNUndo";
            BTNUndo.Size = new Size(88, 27);
            BTNUndo.TabIndex = 17;
            BTNUndo.Text = "Undo";
            BTNUndo.UseVisualStyleBackColor = true;
            // 
            // STBStatus
            // 
            STBStatus.Items.AddRange(new ToolStripItem[] { STLStatus });
            STBStatus.Location = new Point(0, 428);
            STBStatus.Name = "STBStatus";
            STBStatus.Size = new Size(800, 22);
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
            BTNDelete.Location = new Point(117, 378);
            BTNDelete.Margin = new Padding(4, 3, 4, 3);
            BTNDelete.Name = "BTNDelete";
            BTNDelete.Size = new Size(88, 27);
            BTNDelete.TabIndex = 14;
            BTNDelete.Text = "Delete";
            BTNDelete.UseVisualStyleBackColor = true;
            // 
            // BTNSave
            // 
            BTNSave.Location = new Point(13, 378);
            BTNSave.Margin = new Padding(4, 3, 4, 3);
            BTNSave.Name = "BTNSave";
            BTNSave.Size = new Size(88, 27);
            BTNSave.TabIndex = 13;
            BTNSave.Text = "Save";
            BTNSave.UseVisualStyleBackColor = true;
            BTNSave.Click += BTNSave_Click;
            // 
            // BTNClose
            // 
            BTNClose.DialogResult = DialogResult.Cancel;
            BTNClose.Location = new Point(336, 378);
            BTNClose.Margin = new Padding(4, 3, 4, 3);
            BTNClose.Name = "BTNClose";
            BTNClose.Size = new Size(88, 27);
            BTNClose.TabIndex = 15;
            BTNClose.Text = "Close";
            BTNClose.UseVisualStyleBackColor = true;
            BTNClose.Click += BTNClose_Click;
            // 
            // BTNNew
            // 
            BTNNew.Location = new Point(275, 12);
            BTNNew.Margin = new Padding(4, 3, 4, 3);
            BTNNew.Name = "BTNNew";
            BTNNew.Size = new Size(46, 23);
            BTNNew.TabIndex = 18;
            BTNNew.Text = "New";
            BTNNew.UseVisualStyleBackColor = true;
            // 
            // CMBControlType
            // 
            CMBControlType.BackColor = SystemColors.Window;
            CMBControlType.FormattingEnabled = true;
            CMBControlType.Location = new Point(100, 11);
            CMBControlType.Margin = new Padding(4, 3, 4, 3);
            CMBControlType.Name = "CMBControlType";
            CMBControlType.Size = new Size(144, 23);
            CMBControlType.Sorted = true;
            CMBControlType.TabIndex = 19;
            CMBControlType.Tag = "1";
            CMBControlType.KeyDown += CMBControlType_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 16);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 20;
            label1.Text = "Control/Type:";
            // 
            // frmThemeMaintenance
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BTNNew);
            Controls.Add(CMBControlType);
            Controls.Add(label1);
            Controls.Add(BTNUndo);
            Controls.Add(STBStatus);
            Controls.Add(BTNDelete);
            Controls.Add(BTNSave);
            Controls.Add(BTNClose);
            Name = "frmThemeMaintenance";
            Text = "Theme Maintenance";
            Load += frmThemeMaintenance_Load;
            STBStatus.ResumeLayout(false);
            STBStatus.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BTNUndo;
        private StatusStrip STBStatus;
        private ToolStripStatusLabel STLStatus;
        private Button BTNDelete;
        private Button BTNSave;
        private Button BTNClose;
        private Button BTNNew;
        private ComboBox CMBControlType;
        private Label label1;
    }
}