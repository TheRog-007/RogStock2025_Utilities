using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using RogStock2025_Utilities.Modules;

namespace RogStock2025_Utilities.Screens
{
    public partial class frmLoginMaintenance : Form
    {
       //used to signify new record
        bool blnNew = false;

        public frmLoginMaintenance()
        {
            InitializeComponent();
        }
        //************custom code*************

        private void ResetForm(string strKeep, bool blnEnable)
        {
            /*
             Created 18/06/2025 By Roger Williams

             Resets form 
             Enables/Disables form

            VARS

            strKeep     - control to leave
            blnEnable   - enable or disable form

            */

            //reset form

            Modules.clsView_Utilities.ResetForm(this, strKeep);
            //load users comnbo with current users
            Modules.clsData_Utilities.PopulateComboBoxes(this.CMBUser, Modules.clsData_Utilities.CNST_STR_LOGIN, "", "", "", "", "", false);
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Edit");
            blnNew = false;
        }

        private bool CheckForPassword()
        {
            /*
              Created 03/07/2025 By Roger Williams

              simple check if password is null

            */
            if (this.TXTPassword.Text.Length == 0)
            {
                MessageBox.Show("Password Required!", "Missing Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.TXTPassword.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void GetUserPassword()
        /*
          Created 03/07/2025 By Roger Williams

          Used by CMBUser events to get user password when user name typed or selected

        */
        {
           
            if (!blnNew)
            {
                //check if exiats
                if (Modules.clsData_Utilities.CheckLoginExists(this.CMBUser.Text))
                {
                    //get password
                    this.TXTPassword.Text = clsData_Utilities.GetPassword(this.CMBUser.Text);
                }
                else
                {
                    //not found?
                    this.CMBUser.Focus();
                }
            }
        }


        private void SaveRecord()
        {
            /*
              Created 18/06/2025 By Roger Williams

              if new records:
                 - checks user does not already exist
                 - encrypts password
              else
              - encrypts password IF changed

            */

            //MemoryStream memStream = new MemoryStream();
            //StreamWriter sw = new StreamWriter(memStm);

            //sw.Write(yourMysteryObject);

            //SqlCommand sqlCmd = new SqlCommand("INSERT INTO TableName(VarBinaryColumn) VALUES (@VarBinary)", sqlConnection);

            //sqlCmd.Parameters.Add("@VarBinary", SqlDbType.VarBinary, Int32.MaxValue);

            //sqlCmd.Parameters["@VarBinary"].Value = memStream.GetBuffer();

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlTransaction SQLTransaction = null;

            //no user name don't process
            if (this.CMBUser.Text.Length == 0) return;

            if (CheckForPassword())
            {
                if (blnNew)
                {
                    if (Modules.clsData_Utilities.CheckLoginExists(this.CMBUser.Text))
                    {
                        MessageBox.Show("User Already Exists!", "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //reset form and update users combobox
                        ResetForm("", true);
                        return;
                    }
                }

                //open database
                try
                {
                    using (SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                    {
                        SQLConn.Open();
                        SQLCmd = SQLConn.CreateCommand();

                        try
                        { 
                        if (blnNew)
                            {
                                //create user
                                SQLCmd.CommandText = "INSERT INTO " + Modules.clsData_Utilities.CNST_STR_LOGIN +
                                    " (LOG_User, LOG_Password) VALUES ('" + this.CMBUser.Text + "','" + this.TXTPassword.Text + "');";
                            }
                            else
                            {
                                //update user
                                SQLCmd.CommandText = "UPDATE " + Modules.clsData_Utilities.CNST_STR_LOGIN + " SET LOG_Password='" + this.TXTPassword.Text + "' WHERE LOG_User ='" + this.CMBUser.Text + "';";
                            }

                            SQLTransaction = SQLConn.BeginTransaction();
                            SQLCmd.Transaction = SQLTransaction;
                            SQLCmd.ExecuteNonQuery();
                            SQLTransaction.Commit();
                            //reset form and update users combobox
                            ResetForm("", true);
                            MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            SQLTransaction.Rollback();
                            MessageBox.Show("Error Saving Data:\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                     }

                    SQLConn.Close();
      
                }
                catch (Exception ex)
                {
                    if (SQLTransaction.Connection != null)
                    {
                        SQLTransaction.Rollback();
                    }

                    MessageBox.Show("Error Saving Data:\n\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - deletes login record using a transaction

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmdLogin;
            SqlTransaction SQLTransaction;
            int intWritten = 0;

            if (blnNew)
            {
                //if new just clear form
                ResetForm("", false);
                return;
            }

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                {
                    SQLConn.Open();

                    //create command objects for each table
                    SQLCmdLogin = new SqlCommand("DELETE FROM " + Modules.clsData_Utilities.CNST_STR_LOGIN + " WHERE LOG_User = '" + this.CMBUser.Text + "';", SQLConn);
                    //start transction
                    SQLTransaction = SQLConn.BeginTransaction();
                    //assign commands to the transaction
                    SQLCmdLogin.Transaction = SQLTransaction;

                    try
                    {
                        //delete existing
                        SQLCmdLogin.ExecuteNonQuery();

                        //write changes
                        SQLTransaction.Commit();
                        ResetForm("", false);
                        MessageBox.Show("Record Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        if (SQLTransaction.Connection != null)
                        {
                            SQLTransaction.Rollback();
                        }
                        MessageBox.Show("Error Deleting Data:\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    SQLConn.Close();

                    //reset form and update users combobox
                    ResetForm("", true);
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Accessing Database:\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //*********** form events *************
        private void frmLoginMaintenance_Load(object sender, EventArgs e)
        {
            //load users comnbo with current users
            Modules.clsData_Utilities.PopulateComboBoxes(this.CMBUser, Modules.clsData_Utilities.CNST_STR_LOGIN, "", "", "", "", "", false);
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Edit");
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            //remove from open forms list
            Modules.clsView_Utilities.RemoveFromOpenForms(this.ParentForm, this.Text);
            this.Close();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            //save data
            SaveRecord();
        }

        private void BTNNew_Click(object sender, EventArgs e)
        {
            //reset form
            ResetForm("", true);
            blnNew = true;
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: New");
        }

        private void TXTPassword_Leave(object sender, EventArgs e)
        {
            //make sure password entered
            CheckForPassword();
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete Record?", "Erase Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;
            }

            //delete user
            DeleteRecord();
        }

        private void BTNShowPassword_Click(object sender, EventArgs e)
        {
            /*
              Created 02/07/2025 By Roger Williams

              shows/hides password char to show password to user

            */
            if (this.TXTPassword.PasswordChar == '*')
            {
                this.TXTPassword.PasswordChar = Convert.ToChar(0x0);
                this.BTNShowPassword.Text = "Hide";
            }
            else
            {
                this.TXTPassword.PasswordChar = '*';
                this.BTNShowPassword.Text = "Show";
            }
        }

        private void CMBUser_Leave(object sender, EventArgs e)
        {
            /*
              Created 02/07/2025 By Roger Williams

              checks manually type user name exists if NOT new record

            */
            //get user password
            GetUserPassword();
        }


        private void CMBUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get user password
            GetUserPassword();
        }
    }
}
