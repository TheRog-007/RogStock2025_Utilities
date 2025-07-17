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
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            BTNSave = new Button();
            BTNClose = new Button();
            PANMain = new Panel();
            BTNPaste = new Button();
            BTNCopy = new Button();
            LBLSelectedColour = new Label();
            RBSelectedColour = new RadioButton();
            BTNColour = new Button();
            BTNUndoSingle = new Button();
            label4 = new Label();
            CMBProperty = new ComboBox();
            label3 = new Label();
            CMBControl = new ComboBox();
            label1 = new Label();
            DLGColour = new ColorDialog();
            label2 = new Label();
            STBStatus.SuspendLayout();
            PANMain.SuspendLayout();
            SuspendLayout();
            // 
            // BTNUndo
            // 
            BTNUndo.Location = new Point(218, 447);
            BTNUndo.Margin = new Padding(4, 3, 4, 3);
            BTNUndo.Name = "BTNUndo";
            BTNUndo.Size = new Size(88, 27);
            BTNUndo.TabIndex = 17;
            BTNUndo.Text = "Undo";
            BTNUndo.UseVisualStyleBackColor = true;
            BTNUndo.Click += BTNUndo_Click;
            // 
            // STBStatus
            // 
            STBStatus.Items.AddRange(new ToolStripItem[] { STLStatus, toolStripStatusLabel1 });
            STBStatus.Location = new Point(0, 482);
            STBStatus.Name = "STBStatus";
            STBStatus.Size = new Size(889, 22);
            STBStatus.TabIndex = 16;
            STBStatus.Text = "statusStrip1";
            // 
            // STLStatus
            // 
            STLStatus.Name = "STLStatus";
            STLStatus.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // BTNSave
            // 
            BTNSave.Location = new Point(13, 447);
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
            BTNClose.Location = new Point(785, 447);
            BTNClose.Margin = new Padding(4, 3, 4, 3);
            BTNClose.Name = "BTNClose";
            BTNClose.Size = new Size(88, 27);
            BTNClose.TabIndex = 15;
            BTNClose.Text = "Close";
            BTNClose.UseVisualStyleBackColor = true;
            BTNClose.Click += BTNClose_Click;
            // 
            // PANMain
            // 
            PANMain.BorderStyle = BorderStyle.FixedSingle;
            PANMain.Controls.Add(BTNPaste);
            PANMain.Controls.Add(BTNCopy);
            PANMain.Controls.Add(LBLSelectedColour);
            PANMain.Controls.Add(RBSelectedColour);
            PANMain.Controls.Add(BTNColour);
            PANMain.Controls.Add(BTNUndoSingle);
            PANMain.Controls.Add(label4);
            PANMain.Controls.Add(CMBProperty);
            PANMain.Controls.Add(label3);
            PANMain.Controls.Add(CMBControl);
            PANMain.Controls.Add(label1);
            PANMain.Location = new Point(13, 16);
            PANMain.Name = "PANMain";
            PANMain.Size = new Size(319, 206);
            PANMain.TabIndex = 40;
            PANMain.Paint += PANMain_Paint;
            // 
            // BTNPaste
            // 
            BTNPaste.Location = new Point(163, 133);
            BTNPaste.Name = "BTNPaste";
            BTNPaste.Size = new Size(59, 23);
            BTNPaste.TabIndex = 54;
            BTNPaste.Text = "Paste";
            BTNPaste.UseVisualStyleBackColor = true;
            BTNPaste.Click += BTNPaste_Click;
            // 
            // BTNCopy
            // 
            BTNCopy.Location = new Point(96, 133);
            BTNCopy.Name = "BTNCopy";
            BTNCopy.Size = new Size(59, 23);
            BTNCopy.TabIndex = 53;
            BTNCopy.Text = "Copy";
            BTNCopy.UseVisualStyleBackColor = true;
            BTNCopy.Click += BTNCopy_Click;
            // 
            // LBLSelectedColour
            // 
            LBLSelectedColour.AutoSize = true;
            LBLSelectedColour.Font = new Font("Segoe UI", 10F);
            LBLSelectedColour.Location = new Point(110, 105);
            LBLSelectedColour.Name = "LBLSelectedColour";
            LBLSelectedColour.Size = new Size(13, 19);
            LBLSelectedColour.TabIndex = 52;
            LBLSelectedColour.Text = " ";
            // 
            // RBSelectedColour
            // 
            RBSelectedColour.AutoSize = true;
            RBSelectedColour.Location = new Point(87, 108);
            RBSelectedColour.Name = "RBSelectedColour";
            RBSelectedColour.Size = new Size(14, 13);
            RBSelectedColour.TabIndex = 51;
            RBSelectedColour.TabStop = true;
            RBSelectedColour.UseVisualStyleBackColor = true;
            RBSelectedColour.Visible = false;
            RBSelectedColour.Paint += RBSelectedColour_Paint;
            // 
            // BTNColour
            // 
            BTNColour.Location = new Point(19, 133);
            BTNColour.Name = "BTNColour";
            BTNColour.Size = new Size(59, 23);
            BTNColour.TabIndex = 48;
            BTNColour.Text = "Select";
            BTNColour.UseVisualStyleBackColor = true;
            BTNColour.Click += BTNColour_Click;
            // 
            // BTNUndoSingle
            // 
            BTNUndoSingle.Location = new Point(241, 133);
            BTNUndoSingle.Name = "BTNUndoSingle";
            BTNUndoSingle.Size = new Size(57, 22);
            BTNUndoSingle.TabIndex = 47;
            BTNUndoSingle.Text = "Undo";
            BTNUndoSingle.UseVisualStyleBackColor = true;
            BTNUndoSingle.Click += BTNUndoSingle_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(19, 105);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(53, 19);
            label4.TabIndex = 46;
            label4.Text = "Colour:";
            // 
            // CMBProperty
            // 
            CMBProperty.BackColor = SystemColors.Window;
            CMBProperty.FormattingEnabled = true;
            CMBProperty.Location = new Point(84, 54);
            CMBProperty.Margin = new Padding(4, 3, 4, 3);
            CMBProperty.Name = "CMBProperty";
            CMBProperty.Size = new Size(144, 23);
            CMBProperty.Sorted = true;
            CMBProperty.TabIndex = 43;
            CMBProperty.Tag = "1";
            CMBProperty.SelectedIndexChanged += CMBProperty_SelectedIndexChanged;
            CMBProperty.KeyDown += CMBProperty_KeyDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(19, 58);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(65, 19);
            label3.TabIndex = 44;
            label3.Text = "Property:";
            // 
            // CMBControl
            // 
            CMBControl.BackColor = SystemColors.Window;
            CMBControl.FormattingEnabled = true;
            CMBControl.Location = new Point(84, 20);
            CMBControl.Margin = new Padding(4, 3, 4, 3);
            CMBControl.Name = "CMBControl";
            CMBControl.Size = new Size(179, 23);
            CMBControl.TabIndex = 40;
            CMBControl.Tag = "1";
            CMBControl.SelectedIndexChanged += CMBControl_SelectedIndexChanged;
            CMBControl.KeyDown += CMBControlType_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(18, 23);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(58, 19);
            label1.TabIndex = 41;
            label1.Text = "Control:";
            // 
            // DLGColour
            // 
            DLGColour.FullOpen = true;
            DLGColour.SolidColorOnly = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(339, 9);
            label2.Name = "label2";
            label2.Size = new Size(101, 19);
            label2.TabIndex = 41;
            label2.Text = "Theme Preview";
            // 
            // frmThemeMaintenance
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(889, 504);
            Controls.Add(label2);
            Controls.Add(PANMain);
            Controls.Add(BTNUndo);
            Controls.Add(STBStatus);
            Controls.Add(BTNSave);
            Controls.Add(BTNClose);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmThemeMaintenance";
            Text = "Theme Maintenance";
            Load += frmThemeMaintenance_Load;
            Paint += frmThemeMaintenance_Paint;
            STBStatus.ResumeLayout(false);
            STBStatus.PerformLayout();
            PANMain.ResumeLayout(false);
            PANMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BTNUndo;
        private StatusStrip STBStatus;
        private ToolStripStatusLabel STLStatus;
        private Button BTNSave;
        private Button BTNClose;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel PANMain;
        private Button BTNUndoSingle;
        private ComboBox CMBColour;
        private Label label4;
        private ComboBox CMBProperty;
        private Label label3;
        private ComboBox CMBControl;
        private Label label1;
        private ColorDialog DLGColour;
        private Button BTNColour;
        private RadioButton RBSelectedColour;
        private Label LBLSelectedColour;
        private Button BTNPaste;
        private Button BTNCopy;
        private Label label2;
    }
}