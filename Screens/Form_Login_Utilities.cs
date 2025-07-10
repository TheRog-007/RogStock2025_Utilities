using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;


namespace RogStock2025_Utilities.Screens;

public partial class frmLogin_Utilities : Form
{
    /*
       Created 12/02/2025 By Roger Williams

       Login screen!

     */
    public frmLogin_Utilities()
    {
        InitializeComponent();
    }
    //other
    private void LoginUser()
    {
        /*
           Created 17/02/2025 By Roger Williams

           logins user if valid and creates record in login_current

         */

        if (Modules.clsData_Utilities.CheckLogin(this.TXTUser.Text, this.TXTPassword.Text))
        {
            //create record in login_current
            //    Modules.clsData_Utilities.CreateCurrentLoginRecord(this.TXTUser.Text);

            this.Close();
        }
        else
        {
            MessageBox.Show("Invalid User Name or Password", "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    //form events
    private void BTNCancel_Click(object sender, EventArgs e)
    {
        /*
           Created 15/02/2025 By Roger Williams


         */
        this.Close();
        Application.Exit();
    }

    private void BTNLogin_Click(object sender, EventArgs e)
    {
        /*
           Created 15/02/2025 By Roger Williams

           Login user

         */
        LoginUser();
    }

    private void frmLogin_Utilities_Load(object sender, EventArgs e)
    {
        //apply system theme
        Modules.clsView_Utilities.SetTheme(this);
    }
}
