namespace RogStock2025_Utilities.Screens
{
    partial class frmMenuItems_Utilities
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
            BTNClose = new Button();
            STBStatus = new StatusStrip();
            STLStatus = new ToolStripStatusLabel();
            BTNDelete = new Button();
            panel1 = new Panel();
            CMBMNU_MenuItemName = new ComboBox();
            TXTMNU_MenuItemObject = new TextBox();
            CMBMNU_DisplayWhere = new ComboBox();
            label4 = new Label();
            CMBMNU_Type = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            BTNNew = new Button();
            label1 = new Label();
            BTNSave = new Button();
            BTNUndo = new Button();
            TXTHidden = new TextBox();
            STBStatus.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // BTNClose
            // 
            BTNClose.DialogResult = DialogResult.Cancel;
            BTNClose.Location = new Point(447, 179);
            BTNClose.Margin = new Padding(4, 3, 4, 3);
            BTNClose.Name = "BTNClose";
            BTNClose.Size = new Size(88, 27);
            BTNClose.TabIndex = 7;
            BTNClose.Text = "Close";
            BTNClose.UseVisualStyleBackColor = true;
            BTNClose.Click += BTNClose_Click;
            // 
            // STBStatus
            // 
            STBStatus.Items.AddRange(new ToolStripItem[] { STLStatus });
            STBStatus.Location = new Point(0, 216);
            STBStatus.Name = "STBStatus";
            STBStatus.Size = new Size(557, 22);
            STBStatus.TabIndex = 15;
            STBStatus.Text = "statusStrip1";
            // 
            // STLStatus
            // 
            STLStatus.Name = "STLStatus";
            STLStatus.Size = new Size(0, 17);
            // 
            // BTNDelete
            // 
            BTNDelete.Location = new Point(118, 179);
            BTNDelete.Margin = new Padding(4, 3, 4, 3);
            BTNDelete.Name = "BTNDelete";
            BTNDelete.Size = new Size(88, 27);
            BTNDelete.TabIndex = 5;
            BTNDelete.Text = "Delete";
            BTNDelete.UseVisualStyleBackColor = true;
            BTNDelete.Click += BTNDelete_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(CMBMNU_MenuItemName);
            panel1.Controls.Add(TXTMNU_MenuItemObject);
            panel1.Controls.Add(CMBMNU_DisplayWhere);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(CMBMNU_Type);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(BTNNew);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(14, 4);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(521, 156);
            panel1.TabIndex = 0;
            panel1.TabStop = true;
            // 
            // CMBMNU_MenuItemName
            // 
            CMBMNU_MenuItemName.FormattingEnabled = true;
            CMBMNU_MenuItemName.Location = new Point(149, 19);
            CMBMNU_MenuItemName.Name = "CMBMNU_MenuItemName";
            CMBMNU_MenuItemName.Size = new Size(184, 23);
            CMBMNU_MenuItemName.Sorted = true;
            CMBMNU_MenuItemName.TabIndex = 0;
            CMBMNU_MenuItemName.Tag = "1";
            CMBMNU_MenuItemName.SelectedIndexChanged += CMBMNU_MenuItemName_SelectedIndexChanged;
            CMBMNU_MenuItemName.Leave += CMBMNU_MenuItemName_Leave;
            // 
            // TXTMNU_MenuItemObject
            // 
            TXTMNU_MenuItemObject.Location = new Point(149, 48);
            TXTMNU_MenuItemObject.MaxLength = 50;
            TXTMNU_MenuItemObject.Name = "TXTMNU_MenuItemObject";
            TXTMNU_MenuItemObject.Size = new Size(157, 23);
            TXTMNU_MenuItemObject.TabIndex = 1;
            TXTMNU_MenuItemObject.Tag = "1";
            // 
            // CMBMNU_DisplayWhere
            // 
            CMBMNU_DisplayWhere.FormattingEnabled = true;
            CMBMNU_DisplayWhere.Location = new Point(148, 108);
            CMBMNU_DisplayWhere.Margin = new Padding(4, 3, 4, 3);
            CMBMNU_DisplayWhere.MaxLength = 50;
            CMBMNU_DisplayWhere.Name = "CMBMNU_DisplayWhere";
            CMBMNU_DisplayWhere.Size = new Size(144, 23);
            CMBMNU_DisplayWhere.Sorted = true;
            CMBMNU_DisplayWhere.TabIndex = 3;
            CMBMNU_DisplayWhere.Tag = "1";
            CMBMNU_DisplayWhere.KeyDown += CMBMNU_DisplayWhere_KeyDown;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 113);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(85, 15);
            label4.TabIndex = 13;
            label4.Text = "Display Where:";
            // 
            // CMBMNU_Type
            // 
            CMBMNU_Type.FormattingEnabled = true;
            CMBMNU_Type.Items.AddRange(new object[] { "Form", "Operation", "Report" });
            CMBMNU_Type.Location = new Point(148, 79);
            CMBMNU_Type.Margin = new Padding(4, 3, 4, 3);
            CMBMNU_Type.MaxLength = 10;
            CMBMNU_Type.Name = "CMBMNU_Type";
            CMBMNU_Type.Size = new Size(144, 23);
            CMBMNU_Type.Sorted = true;
            CMBMNU_Type.TabIndex = 2;
            CMBMNU_Type.Tag = "1";
            CMBMNU_Type.KeyDown += CMBMNU_Type_KeyDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 84);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(95, 15);
            label3.TabIndex = 11;
            label3.Text = "Menu Item Type:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 54);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(106, 15);
            label2.TabIndex = 9;
            label2.Text = "Menu Item Object:";
            // 
            // BTNNew
            // 
            BTNNew.Location = new Point(350, 21);
            BTNNew.Margin = new Padding(4, 3, 4, 3);
            BTNNew.Name = "BTNNew";
            BTNNew.Size = new Size(46, 23);
            BTNNew.TabIndex = 0;
            BTNNew.TabStop = false;
            BTNNew.Text = "New";
            BTNNew.UseVisualStyleBackColor = true;
            BTNNew.Click += BTNNew_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(113, 15);
            label1.TabIndex = 7;
            label1.Text = "Menu Item Caption:";
            // 
            // BTNSave
            // 
            BTNSave.Location = new Point(14, 179);
            BTNSave.Margin = new Padding(4, 3, 4, 3);
            BTNSave.Name = "BTNSave";
            BTNSave.Size = new Size(88, 27);
            BTNSave.TabIndex = 4;
            BTNSave.Text = "Save";
            BTNSave.UseVisualStyleBackColor = true;
            BTNSave.Click += BTNSave_Click;
            // 
            // BTNUndo
            // 
            BTNUndo.Location = new Point(245, 179);
            BTNUndo.Margin = new Padding(4, 3, 4, 3);
            BTNUndo.Name = "BTNUndo";
            BTNUndo.Size = new Size(88, 27);
            BTNUndo.TabIndex = 6;
            BTNUndo.Text = "Undo";
            BTNUndo.UseVisualStyleBackColor = true;
            BTNUndo.Click += BTNUndo_Click;
            // 
            // TXTHidden
            // 
            TXTHidden.Location = new Point(0, 0);
            TXTHidden.Name = "TXTHidden";
            TXTHidden.Size = new Size(0, 23);
            TXTHidden.TabIndex = 17;
            TXTHidden.TabStop = false;
            // 
            // frmMenuItems_Utilities
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(557, 238);
            Controls.Add(TXTHidden);
            Controls.Add(BTNUndo);
            Controls.Add(STBStatus);
            Controls.Add(BTNDelete);
            Controls.Add(panel1);
            Controls.Add(BTNSave);
            Controls.Add(BTNClose);
            Name = "frmMenuItems_Utilities";
            Text = "Menu Items";
            Load += frmMenuItems_Utilities_Load;
            STBStatus.ResumeLayout(false);
            STBStatus.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BTNClose;
        private StatusStrip STBStatus;
        private ToolStripStatusLabel STLStatus;
        private Button BTNDelete;
        private Panel panel1;
        private ComboBox CMBMNU_DisplayWhere;
        private Label label4;
        private ComboBox CMBMNU_Type;
        private Label label3;
        private Label label2;
        private Button BTNNew;
        private Label label1;
        private Button BTNSave;
        private TextBox TXTMNU_MenuItemObject;
        private Button BTNUndo;
        private TextBox TXTHidden;
        private ComboBox CMBMNU_MenuItemName;
    }
}