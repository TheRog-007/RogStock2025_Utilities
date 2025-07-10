namespace RogStock2025_Utilities.Screens
{
    partial class frmMenuSecurity_Utilities
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
            SuspendLayout();
            // 
            // BTNClose
            // 
            BTNClose.DialogResult = DialogResult.Cancel;
            BTNClose.Location = new Point(863, 545);
            BTNClose.Margin = new Padding(4, 3, 4, 3);
            BTNClose.Name = "BTNClose";
            BTNClose.Size = new Size(88, 27);
            BTNClose.TabIndex = 8;
            BTNClose.Text = "Close";
            BTNClose.UseVisualStyleBackColor = true;
            BTNClose.Click += BTNClose_Click;
            // 
            // frmMenuSecurity_Utilities
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(964, 584);
            Controls.Add(BTNClose);
            Name = "frmMenuSecurity_Utilities";
            Text = "Menu Security";
            ResumeLayout(false);
        }

        #endregion

        private Button BTNClose;
    }
}