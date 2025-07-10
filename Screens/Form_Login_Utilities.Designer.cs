namespace RogStock2025_Utilities.Screens;

partial class frmLogin_Utilities
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
        BTNCancel = new Button();
        BTNLogin = new Button();
        panel1 = new Panel();
        TXTPassword = new TextBox();
        label2 = new Label();
        TXTUser = new TextBox();
        label1 = new Label();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // BTNCancel
        // 
        BTNCancel.DialogResult = DialogResult.Cancel;
        BTNCancel.Location = new Point(196, 140);
        BTNCancel.Margin = new Padding(4, 3, 4, 3);
        BTNCancel.Name = "BTNCancel";
        BTNCancel.Size = new Size(88, 27);
        BTNCancel.TabIndex = 4;
        BTNCancel.Text = "Cancel";
        BTNCancel.UseVisualStyleBackColor = true;
        BTNCancel.Click += BTNCancel_Click;
        // 
        // BTNLogin
        // 
        BTNLogin.Location = new Point(28, 140);
        BTNLogin.Margin = new Padding(4, 3, 4, 3);
        BTNLogin.Name = "BTNLogin";
        BTNLogin.Size = new Size(88, 27);
        BTNLogin.TabIndex = 3;
        BTNLogin.Text = "Login";
        BTNLogin.UseVisualStyleBackColor = true;
        BTNLogin.Click += BTNLogin_Click;
        // 
        // panel1
        // 
        panel1.BorderStyle = BorderStyle.FixedSingle;
        panel1.Controls.Add(TXTPassword);
        panel1.Controls.Add(label2);
        panel1.Controls.Add(TXTUser);
        panel1.Controls.Add(label1);
        panel1.Location = new Point(28, 15);
        panel1.Margin = new Padding(4, 3, 4, 3);
        panel1.Name = "panel1";
        panel1.Size = new Size(255, 115);
        panel1.TabIndex = 0;
        panel1.TabStop = true;
        // 
        // TXTPassword
        // 
        TXTPassword.Location = new Point(117, 68);
        TXTPassword.Margin = new Padding(4, 3, 4, 3);
        TXTPassword.MaxLength = 10;
        TXTPassword.Name = "TXTPassword";
        TXTPassword.PasswordChar = '*';
        TXTPassword.Size = new Size(116, 23);
        TXTPassword.TabIndex = 2;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(22, 68);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(60, 15);
        label2.TabIndex = 8;
        label2.Text = "Password:";
        // 
        // TXTUser
        // 
        TXTUser.Location = new Point(117, 25);
        TXTUser.Margin = new Padding(4, 3, 4, 3);
        TXTUser.MaxLength = 30;
        TXTUser.Name = "TXTUser";
        TXTUser.Size = new Size(116, 23);
        TXTUser.TabIndex = 1;
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
        // frmLogin_Utilities
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = BTNCancel;
        ClientSize = new Size(309, 174);
        ControlBox = false;
        Controls.Add(panel1);
        Controls.Add(BTNLogin);
        Controls.Add(BTNCancel);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        Margin = new Padding(4, 3, 4, 3);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "frmLogin_Utilities";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "RogStock 20205 Utilities - Login";
        Load += frmLogin_Utilities_Load;
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button BTNCancel;
    private System.Windows.Forms.Button BTNLogin;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox TXTPassword;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox TXTUser;
    private System.Windows.Forms.Label label1;
}

