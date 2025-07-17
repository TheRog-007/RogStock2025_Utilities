namespace RogStock2025_Utilities.Screens
{
    partial class frmColourPalette
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
            GRPColours = new GroupBox();
            SuspendLayout();
            // 
            // GRPColours
            // 
            GRPColours.Location = new Point(11, 12);
            GRPColours.Name = "GRPColours";
            GRPColours.Size = new Size(920, 426);
            GRPColours.TabIndex = 0;
            GRPColours.TabStop = false;
            GRPColours.Text = "Click Box To Select Colour";
            // 
            // frmColourPalette
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(943, 448);
            Controls.Add(GRPColours);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmColourPalette";
            Text = "Select Colour For Control Property";
            Load += frmColourPalette_Load;
            ResumeLayout(false);
        }

        #endregion

        private GroupBox GRPColours;
    }
}