namespace RogStock2025_Utilities.Screens
{
    partial class frmMain_Utilities
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
            components = new System.ComponentModel.Container();
            TMRMenu = new System.Windows.Forms.Timer(components);
            BTNShowHide = new Button();
            PANOptions = new Panel();
            CMBOpenScreens = new ComboBox();
            label1 = new Label();
            PANMenu = new Panel();
            MNUMainMenu = new MenuStrip();
            MNUMenu = new ToolStripMenuItem();
            MNUSections = new ToolStripMenuItem();
            MNUGroups = new ToolStripMenuItem();
            MNUMenuItems = new ToolStripMenuItem();
            MNUMenuSecurity = new ToolStripMenuItem();
            MNUThemeMaintenance = new ToolStripMenuItem();
            MNULogins = new ToolStripMenuItem();
            MNULoginMaintenance = new ToolStripMenuItem();
            MNUExit = new ToolStripMenuItem();
            PANOptions.SuspendLayout();
            PANMenu.SuspendLayout();
            MNUMainMenu.SuspendLayout();
            SuspendLayout();
            // 
            // TMRMenu
            // 
            TMRMenu.Interval = 4000;
            TMRMenu.Tick += TMRMenu_Tick;
            // 
            // BTNShowHide
            // 
            BTNShowHide.BackColor = Color.Green;
            BTNShowHide.FlatAppearance.MouseDownBackColor = Color.LightGreen;
            BTNShowHide.FlatAppearance.MouseOverBackColor = Color.OliveDrab;
            BTNShowHide.FlatStyle = FlatStyle.Flat;
            BTNShowHide.Location = new Point(0, 0);
            BTNShowHide.Name = "BTNShowHide";
            BTNShowHide.Size = new Size(33, 58);
            BTNShowHide.TabIndex = 8;
            BTNShowHide.UseVisualStyleBackColor = false;
            BTNShowHide.Click += BTNShowHide_Click;
            BTNShowHide.Paint += BTNShowHide_Paint;
            // 
            // PANOptions
            // 
            PANOptions.BackColor = Color.Olive;
            PANOptions.Controls.Add(CMBOpenScreens);
            PANOptions.Controls.Add(label1);
            PANOptions.Location = new Point(31, 0);
            PANOptions.Name = "PANOptions";
            PANOptions.Size = new Size(178, 59);
            PANOptions.TabIndex = 9;
            // 
            // CMBOpenScreens
            // 
            CMBOpenScreens.BackColor = Color.Olive;
            CMBOpenScreens.FlatStyle = FlatStyle.Flat;
            CMBOpenScreens.Font = new Font("Microsoft Sans Serif", 10F);
            CMBOpenScreens.ForeColor = Color.White;
            CMBOpenScreens.FormattingEnabled = true;
            CMBOpenScreens.Location = new Point(13, 29);
            CMBOpenScreens.Name = "CMBOpenScreens";
            CMBOpenScreens.Size = new Size(153, 24);
            CMBOpenScreens.TabIndex = 1;
            CMBOpenScreens.SelectedIndexChanged += CMBOpenScreens_SelectedIndexChanged;
            CMBOpenScreens.KeyDown += CMBOpenScreens_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 10F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(15, 8);
            label1.Name = "label1";
            label1.Size = new Size(99, 17);
            label1.TabIndex = 0;
            label1.Text = "Open Screens";
            // 
            // PANMenu
            // 
            PANMenu.BackColor = Color.Olive;
            PANMenu.Controls.Add(MNUMainMenu);
            PANMenu.Location = new Point(208, 0);
            PANMenu.Name = "PANMenu";
            PANMenu.Size = new Size(250, 59);
            PANMenu.TabIndex = 10;
            // 
            // MNUMainMenu
            // 
            MNUMainMenu.BackgroundImageLayout = ImageLayout.None;
            MNUMainMenu.Items.AddRange(new ToolStripItem[] { MNUMenu, MNULogins, MNUExit });
            MNUMainMenu.Location = new Point(0, 0);
            MNUMainMenu.Name = "MNUMainMenu";
            MNUMainMenu.Size = new Size(250, 27);
            MNUMainMenu.TabIndex = 0;
            MNUMainMenu.Text = "menuStrip1";
            MNUMainMenu.ItemClicked += MNUMainMenu_ItemClicked;
            // 
            // MNUMenu
            // 
            MNUMenu.DropDownItems.AddRange(new ToolStripItem[] { MNUSections, MNUGroups, MNUMenuItems, MNUMenuSecurity, MNUThemeMaintenance });
            MNUMenu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MNUMenu.ForeColor = Color.White;
            MNUMenu.Name = "MNUMenu";
            MNUMenu.Size = new Size(58, 23);
            MNUMenu.Text = "Menu";
            // 
            // MNUSections
            // 
            MNUSections.BackgroundImageLayout = ImageLayout.None;
            MNUSections.DisplayStyle = ToolStripItemDisplayStyle.Text;
            MNUSections.ForeColor = Color.White;
            MNUSections.Name = "MNUSections";
            MNUSections.Size = new Size(212, 24);
            MNUSections.Text = "Sections";
            // 
            // MNUGroups
            // 
            MNUGroups.ForeColor = Color.White;
            MNUGroups.Name = "MNUGroups";
            MNUGroups.Size = new Size(212, 24);
            MNUGroups.Text = "Groups";
            // 
            // MNUMenuItems
            // 
            MNUMenuItems.ForeColor = Color.White;
            MNUMenuItems.Name = "MNUMenuItems";
            MNUMenuItems.Size = new Size(212, 24);
            MNUMenuItems.Text = "Menu Items";
            // 
            // MNUMenuSecurity
            // 
            MNUMenuSecurity.ForeColor = Color.White;
            MNUMenuSecurity.Name = "MNUMenuSecurity";
            MNUMenuSecurity.Size = new Size(212, 24);
            MNUMenuSecurity.Text = "Menu Security";
            // 
            // MNUThemeMaintenance
            // 
            MNUThemeMaintenance.Name = "MNUThemeMaintenance";
            MNUThemeMaintenance.Size = new Size(212, 24);
            MNUThemeMaintenance.Text = "Theme Maintenance";
            // 
            // MNULogins
            // 
            MNULogins.DropDownItems.AddRange(new ToolStripItem[] { MNULoginMaintenance });
            MNULogins.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MNULogins.ForeColor = Color.White;
            MNULogins.Name = "MNULogins";
            MNULogins.Size = new Size(64, 23);
            MNULogins.Text = "Logins";
            // 
            // MNULoginMaintenance
            // 
            MNULoginMaintenance.ForeColor = Color.White;
            MNULoginMaintenance.Name = "MNULoginMaintenance";
            MNULoginMaintenance.Size = new Size(204, 24);
            MNULoginMaintenance.Text = "Login Maintenance";
            // 
            // MNUExit
            // 
            MNUExit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MNUExit.ForeColor = Color.White;
            MNUExit.Name = "MNUExit";
            MNUExit.Size = new Size(45, 23);
            MNUExit.Text = "Exit";
            MNUExit.Click += MNUExit_Click;
            // 
            // frmMain_Utilities
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1379, 865);
            Controls.Add(PANMenu);
            Controls.Add(PANOptions);
            Controls.Add(BTNShowHide);
            IsMdiContainer = true;
            MainMenuStrip = MNUMainMenu;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "frmMain_Utilities";
            Text = "RogStock 2025 - Utilities";
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            PANOptions.ResumeLayout(false);
            PANOptions.PerformLayout();
            PANMenu.ResumeLayout(false);
            PANMenu.PerformLayout();
            MNUMainMenu.ResumeLayout(false);
            MNUMainMenu.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer TMRMenu;
        private Button BTNShowHide;
        private Panel PANOptions;
        private ComboBox CMBOpenScreens;
        private Label label1;
        private Panel PANMenu;
        private MenuStrip MNUMainMenu;
        private ToolStripMenuItem MNUMenu;
        private ToolStripMenuItem MNULogins;
        private ToolStripMenuItem MNUExit;
        private ToolStripMenuItem MNUSections;
        private ToolStripMenuItem MNUGroups;
        private ToolStripMenuItem MNUMenuItems;
        private ToolStripMenuItem MNUMenuSecurity;
        private ToolStripMenuItem MNULoginMaintenance;
        private ToolStripMenuItem MNUThemeMaintenance;
    }
}