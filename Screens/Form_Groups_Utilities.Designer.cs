namespace RogStock2025_Utilities.Screens
{
    partial class frmGroups_Utilities
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
            TXTHidden = new TextBox();
            TVMenuItems_old = new TreeView();
            TVMenuItems = new TreeView();
            label2 = new Label();
            BTNNew = new Button();
            CMBGRP_Group = new ComboBox();
            label1 = new Label();
            BTNSave = new Button();
            BTNClose = new Button();
            BTNUndo = new Button();
            STBStatus.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // STBStatus
            // 
            STBStatus.Items.AddRange(new ToolStripItem[] { STLStatus });
            STBStatus.Location = new Point(0, 513);
            STBStatus.Name = "STBStatus";
            STBStatus.Size = new Size(436, 22);
            STBStatus.TabIndex = 11;
            STBStatus.Text = "statusStrip1";
            // 
            // STLStatus
            // 
            STLStatus.Name = "STLStatus";
            STLStatus.Size = new Size(0, 17);
            // 
            // BTNDelete
            // 
            BTNDelete.Location = new Point(117, 480);
            BTNDelete.Margin = new Padding(4, 3, 4, 3);
            BTNDelete.Name = "BTNDelete";
            BTNDelete.Size = new Size(88, 27);
            BTNDelete.TabIndex = 8;
            BTNDelete.Text = "Delete";
            BTNDelete.UseVisualStyleBackColor = true;
            BTNDelete.Click += BTNDelete_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(TXTHidden);
            panel1.Controls.Add(TVMenuItems_old);
            panel1.Controls.Add(TVMenuItems);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(BTNNew);
            panel1.Controls.Add(CMBGRP_Group);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(18, 7);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(398, 458);
            panel1.TabIndex = 9;
            panel1.TabStop = true;
            panel1.Paint += panel1_Paint;
            // 
            // TXTHidden
            // 
            TXTHidden.Location = new Point(470, 21);
            TXTHidden.Name = "TXTHidden";
            TXTHidden.Size = new Size(0, 23);
            TXTHidden.TabIndex = 12;
            TXTHidden.TabStop = false;
            // 
            // TVMenuItems_old
            // 
            TVMenuItems_old.BackColor = SystemColors.InactiveCaption;
            TVMenuItems_old.CheckBoxes = true;
            TVMenuItems_old.Location = new Point(199, 54);
            TVMenuItems_old.Name = "TVMenuItems_old";
            TVMenuItems_old.Size = new Size(98, 93);
            TVMenuItems_old.TabIndex = 11;
            TVMenuItems_old.Visible = false;
            // 
            // TVMenuItems
            // 
            TVMenuItems.CheckBoxes = true;
            TVMenuItems.Location = new Point(22, 72);
            TVMenuItems.Name = "TVMenuItems";
            TVMenuItems.Size = new Size(348, 350);
            TVMenuItems.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 54);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 9;
            label2.Text = "Menu Items:";
            // 
            // BTNNew
            // 
            BTNNew.Location = new Point(285, 21);
            BTNNew.Margin = new Padding(4, 3, 4, 3);
            BTNNew.Name = "BTNNew";
            BTNNew.Size = new Size(46, 23);
            BTNNew.TabIndex = 0;
            BTNNew.Text = "New";
            BTNNew.UseVisualStyleBackColor = true;
            BTNNew.Click += BTNNew_Click;
            // 
            // CMBGRP_Group
            // 
            CMBGRP_Group.BackColor = SystemColors.Window;
            CMBGRP_Group.FormattingEnabled = true;
            CMBGRP_Group.Location = new Point(106, 20);
            CMBGRP_Group.Margin = new Padding(4, 3, 4, 3);
            CMBGRP_Group.Name = "CMBGRP_Group";
            CMBGRP_Group.Size = new Size(144, 23);
            CMBGRP_Group.Sorted = true;
            CMBGRP_Group.TabIndex = 1;
            CMBGRP_Group.Tag = "1";
            CMBGRP_Group.SelectedIndexChanged += CMBGRP_Group_SelectedIndexChanged;
            CMBGRP_Group.KeyDown += CMBGroups_KeyDown;
            CMBGRP_Group.Leave += CMBGroups_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 7;
            label1.Text = "Group:";
            // 
            // BTNSave
            // 
            BTNSave.Location = new Point(13, 480);
            BTNSave.Margin = new Padding(4, 3, 4, 3);
            BTNSave.Name = "BTNSave";
            BTNSave.Size = new Size(88, 27);
            BTNSave.TabIndex = 7;
            BTNSave.Text = "Save";
            BTNSave.UseVisualStyleBackColor = true;
            BTNSave.Click += BTNSave_Click;
            // 
            // BTNClose
            // 
            BTNClose.DialogResult = DialogResult.Cancel;
            BTNClose.Location = new Point(336, 480);
            BTNClose.Margin = new Padding(4, 3, 4, 3);
            BTNClose.Name = "BTNClose";
            BTNClose.Size = new Size(88, 27);
            BTNClose.TabIndex = 10;
            BTNClose.Text = "Close";
            BTNClose.UseVisualStyleBackColor = true;
            BTNClose.Click += BTNClose_Click;
            // 
            // BTNUndo
            // 
            BTNUndo.Location = new Point(218, 480);
            BTNUndo.Margin = new Padding(4, 3, 4, 3);
            BTNUndo.Name = "BTNUndo";
            BTNUndo.Size = new Size(88, 27);
            BTNUndo.TabIndex = 12;
            BTNUndo.Text = "Undo";
            BTNUndo.UseVisualStyleBackColor = true;
            BTNUndo.Click += BTNUndo_Click;
            // 
            // frmGroups_Utilities
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 535);
            Controls.Add(BTNUndo);
            Controls.Add(STBStatus);
            Controls.Add(BTNDelete);
            Controls.Add(panel1);
            Controls.Add(BTNSave);
            Controls.Add(BTNClose);
            Name = "frmGroups_Utilities";
            Text = "Groups Maintenance";
            Load += frmGroups_Utilities_Load;
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
        private ComboBox CMBGRP_Group;
        private Label label1;
        private Button BTNSave;
        private Button BTNClose;
        private Label label2;
        private TreeView TVMenuItems;
        private Button BTNUndo;
        private TreeView TVMenuItems_old;
        private TextBox TXTHidden;
    }
}