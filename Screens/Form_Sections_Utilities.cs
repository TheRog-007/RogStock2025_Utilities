using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using RogStock2025_Utilities.Modules;


/*
 Created 03/07/2025 By Roger Williams

 Sections maintenance

 Called Area internally, but to user is called "sections" this is to avoid any confusion should sales areas be added alter!

 No editing allowed this is done via the "replace" utility

*/



namespace RogStock2025_Utilities.Screens
{
    public partial class frmSections_Utilities : Form
    {
        bool blnNew = false;

        public frmSections_Utilities()
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
            Modules.clsData_Utilities.PopulateComboBoxes(this.CMBSEC_Area, Modules.clsData_Utilities.CNST_STR_MENU_AREAS, "", "", "", "", "", false);
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Read");
            blnNew = false;
        }


        private void SaveRecord()
        {
            /*
              Created 03/07/2025 By Roger Williams

              Creates/updates MENU_Group record  

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlTransaction SQLTransaction = null;

            //no group name don't process
            if (this.CMBSEC_Area.Text.Length == 0) return;

            if (blnNew)
            {
                if (Modules.clsData_Utilities.CheckSectionExists(this.CMBSEC_Area.Text))
                {
                    MessageBox.Show("Menu Area Already Exists!", "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    SQLTransaction = SQLConn.BeginTransaction();
                    SQLCmd.Transaction = SQLTransaction;

                    if (blnNew)
                    {
                        //create area
                        SQLCmd.CommandText = "INSERT INTO " + Modules.clsData_Utilities.CNST_STR_MENU_AREAS +
                            " (SEC_Area) VALUES ('" + this.CMBSEC_Area.Text + "');";
                        SQLCmd.ExecuteNonQuery();
                    }

                    SQLTransaction.Commit();
                    //reset form and update users combobox
                    ResetForm("", true);
                    MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SQLConn.Close();
                Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Read");
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

        private void DeleteRecord()
        {
            /*
              Created 03/07/2025 By Roger Williams

              deletes group record using a transaction

              IF area in Menu_MenuItems OR Menu_Groups ask user before proceeding as those records will also need to be deletdd

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmdLogin;
            SqlTransaction SQLTransaction;
            int intNum1 = 0;
            int intNum2 = 0;
            List<string> lstMenuItemas = new List<string>();

            if (blnNew)
            {
                //if new just clear form
                ResetForm("", false);
                return;
            }

            //check if in menuitems/groups
            intNum1 = Modules.clsData_Utilities.GetMenuItemsCountForArea(this.CMBSEC_Area.Text);
            //     intNum2 = Modules.clsData_Utilities.GetMenuItemsCountForGroup(this.CMBSections.Text);

            //only need to check intnum1 as impossible to create groups WITHOUT and existing menuitem!
            if (intNum1 > 0)
            {
                if (MessageBox.Show("Record Delete Will Also Required Deleting " + intNum2.ToString() + "  Records From Groups and " + intNum1.ToString() + "  Records From Menu Items - Proceed?", "Erase Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                {
                    SQLConn.Open();

                    //create command objects for each table
                    SQLCmdLogin = new SqlCommand("", SQLConn);
                    //start transction
                    SQLTransaction = SQLConn.BeginTransaction();
                    //assign commands to the transaction
                    SQLCmdLogin.Transaction = SQLTransaction;

                    try
                    {
                        if (intNum1 != 0)
                        {
                            lstMenuItemas = Modules.clsData_Utilities.GetMenuItemsForArea(this.CMBSEC_Area.Text);

                            foreach (string strTemp in lstMenuItemas)
                            {
                                SQLCmdLogin.CommandText = "DELETE FROM " + Modules.clsData_Utilities.CNST_STR_MENU_GROUPS + " WHERE GRP_MenuItem = '" + strTemp + "';";
                                SQLCmdLogin.ExecuteNonQuery();
                            }

                            //delete menuitems
                            SQLCmdLogin.CommandText = "DELETE FROM " + Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS + " WHERE MNU_DisplayWhere = '" + this.CMBSEC_Area.Text + "';";
                            SQLCmdLogin.ExecuteNonQuery();
                        }

                        //delete area
                        SQLCmdLogin.CommandText = "DELETE FROM " + Modules.clsData_Utilities.CNST_STR_MENU_AREAS + " WHERE SEC_Area = '" + this.CMBSEC_Area.Text + "';";
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





        private void BTNClose_Click(object sender, EventArgs e)
        {
            Modules.clsView_Utilities.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void BTNNew_Click(object sender, EventArgs e)
        {
            //reset form
            ResetForm("", true);
            blnNew = true;
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: New");
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

        private void BTNSave_Click(object sender, EventArgs e)
        {
            //save data
            SaveRecord();
        }

        private void frmSections_Utilities_Load(object sender, EventArgs e)
        {
            //load users comnbo with current users
            Modules.clsData_Utilities.PopulateComboBoxes(this.CMBSEC_Area, Modules.clsData_Utilities.CNST_STR_MENU_AREAS, "", "", "", "", "", false);
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Read");
        }

        private void CMBSections_KeyDown(object sender, KeyEventArgs e)
        {
            /*
               Created 04/07/2025 By Roger Williams


               Stop user entering text
            */
            //   e.SuppressKeyPress = true;
        }

        private void CMBSections_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (blnNew)
            //{
            //    if (Modules.clsData_Utilities.CheckAreaExists(this.CMBSEC_Area.Text))
            //    {
            //        MessageBox.Show("Cannot Add " + this.CMBSEC_Area.Text + " As It Already Exists!", "Duplicate Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        this.CMBSEC_Area.Focus();
            //        return;
            //    }
            //    //if new record do nothing
            //    return;
            //}

            //if (this.CMBSEC_Area.SelectedIndex != -1 || this.CMBSEC_Area.Text.Length != 0)
            //{
            //    if (Modules.clsView_Utilities.ValueInCombobox(this.CMBSEC_Area, this.CMBSEC_Area.Text))
            //    {
            //        if (this.TXTHidden.Text == this.CMBSEC_Area.Text)
            //        {
            //            return;
            //        }


            //        if (!blnLoading)
            //        {
            //            //if no records load!
            //            LoadRecord();
            //        }
            //    }
            //}
        }

        private void CMBSEC_Area_Leave(object sender, EventArgs e)
        {
            if (blnNew)
            {
                if (Modules.clsData_Utilities.CheckAreaExists(this.CMBSEC_Area.Text))
                {
                    MessageBox.Show("Cannot Add " + this.CMBSEC_Area.Text + " As It Already Exists!", "Duplicate Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CMBSEC_Area.Focus();
                    return;
                }
                //if new record do nothing
                return;
            }
        }
    }
}
