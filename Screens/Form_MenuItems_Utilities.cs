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

/*
  Created 04/07/2025 By Roger Williams

  menu item maintenance

  requires Menu_Areas to have records!

  uses data binding so can use undo

*/

namespace RogStock2025_Utilities.Screens
{
    public partial class frmMenuItems_Utilities : Form
    {
        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBMNU_MenuItemName";
        //datasets used to checking if data changed
        DataSet DSTMenuItems = new DataSet();
        bool blnNew = false;
     //   bool blnOk = false;
        bool blnLoading = false;
        bool blnShow = true;
        Pen penTemp;
        //binding sources to link tables to controls
        BindingSource BNSMenuItems = new BindingSource();

        public frmMenuItems_Utilities()
        {
            InitializeComponent();
        }

        //************custom subs/funcs***********
        private void BindForm()
        {
            /*
              Created 04/07/2025 By Roger Williams

              binds form to table: 
            
              Menu_MenuItems


            */

            if (this.BNSMenuItems.Count > 0)
            {
                this.BNSMenuItems = new BindingSource();
            }

            this.BNSMenuItems.DataSource = this.DSTMenuItems.Tables[Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS];

            //clear bindings
            this.TXTHidden.DataBindings.Clear();
            this.CMBMNU_MenuItemName.DataBindings.Clear();
            this.TXTMNU_MenuItemObject.DataBindings.Clear();
            this.CMBMNU_DisplayWhere.DataBindings.Clear();
            this.CMBMNU_Type.DataBindings.Clear();
            ;

            //bind form controls to stock_LOT
            this.TXTHidden.DataBindings.Add("text", this.BNSMenuItems, "MNU_MenuItemName", true, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBMNU_MenuItemName.DataBindings.Add("text", this.BNSMenuItems, "MNU_MenuItemName");
            this.TXTMNU_MenuItemObject.DataBindings.Add("text", this.BNSMenuItems, "MNU_MenuItemObject");
            this.CMBMNU_Type.DataBindings.Add("text", this.BNSMenuItems, "MNU_Type");
            this.CMBMNU_DisplayWhere.DataBindings.Add("text", this.BNSMenuItems, "MNU_DisplayWhere");
        }

        private void ResetForm(string strKeep, bool blnEnable)
        {
            /*
                 Created 04/07/2025 By Roger Williams

                 Resets form 
                 Enables/Disables form

                VARS

                strKeep     - control to leave
                blnEnable   - enable or disable form

            */

            //reset form
            Modules.clsView_Utilities.ResetForm(this, strKeep);
            Modules.clsView_Utilities.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, blnEnable);
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Edit");
            Modules.clsData_Utilities.PopulateComboBoxes(this.CMBMNU_MenuItemName, Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS, "", "", "", "", "",false);
            blnNew = false;
        }



        private void LoadRecord()
        {
            /*
              Created 04/07/2025 By Roger Williams
   
              Populates the form
              Populates dataset with table for binding
              Enables the form

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataAdapter DADTemp = null;

            string strSQL1 = "SELECT * FROM " + Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS + " WHERE MNU_MenuItemName ='" + this.CMBMNU_MenuItemName.Text + "';";

            blnLoading = true;
            //reset form except item id combobox
            ResetForm(CNST_STR_FIRSTCONTROL, false);

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = strSQL1;
                    SQLCmd.CommandType = CommandType.Text;
                   
                    if (SQLCmd.ExecuteScalar() != null)
                    {
                        //clear existing records
                        if (DSTMenuItems.Tables.Count > 0)
                        {
                            this.DSTMenuItems.Tables[Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS].Clear();
                        }

                        //////get record into into dataset
                        DADTemp = new SqlDataAdapter(strSQL1, SQLConn);
                        DADTemp.SelectCommand.CommandText = strSQL1;
                        DADTemp.Fill(DSTMenuItems, Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS);

                        SQLConn.Close();
                        //enable form
                        Modules.clsView_Utilities.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                        //update hidden text control with item id
                        this.TXTHidden.Text = this.CMBMNU_MenuItemName.Text;

                        //bindform
                        BindForm();
                    }
                    else
                    {
                        MessageBox.Show("No Records Found", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            blnLoading = false;
        }
        private void DeleteRecord()
        {
            /*
              Created 04/07/2025 By Roger Williams

              Deletes current record using a transaction
              clears form 
              Clears datasets

            Deletes from:

            Menu_MenuItems

            IF record in:  Menu_Groups  deletes from there 

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmdMenuItems;
            SqlCommand SQLCmdGroups;
            SqlTransaction SQLTransaction;

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                {
                    SQLConn.Open();

                    //create command objects for each table
                    SQLCmdMenuItems = new SqlCommand("DELETE FROM " + Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS + " WHERE MNU_MenuItemName = '" + this.CMBMNU_MenuItemName.Text + "';", SQLConn);
                    SQLCmdGroups = new SqlCommand("DELETE FROM " + Modules.clsData_Utilities.CNST_STR_MENU_GROUPS + " WHERE GRP_MenuItem = '" + this.CMBMNU_MenuItemName.Text + "';", SQLConn);
                    //start transction
                    SQLTransaction = SQLConn.BeginTransaction();
                    //assign commands to the transaction
                    SQLCmdMenuItems.Transaction = SQLTransaction;
                    SQLCmdGroups.Transaction = SQLTransaction;

                    try
                    {
                        //delete existing
                        SQLCmdMenuItems.CommandType = CommandType.Text;
                        SQLCmdMenuItems.ExecuteNonQuery();

                        SQLCmdGroups.CommandType = CommandType.Text;
                        SQLCmdGroups.ExecuteNonQuery();
                        SQLTransaction.Commit();

                        ResetForm("", true);
                        MessageBox.Show("Record Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        SQLTransaction.Rollback();
                        MessageBox.Show("Error Deleting Data:\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Accessing Database:\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveRecord()
        {
            /*         
              Created 04/07/2025 By Roger Williams

              Saves record

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmdMenuItems;
            SqlTransaction SQLTransaction;

            //commit changes to dataset
            this.BNSMenuItems.EndEdit();


            //check required fields completed
            if (Modules.clsView_Utilities.ValidateRequiredFields(this))
            {

                //check if already exists
                if (blnNew)
                {
                    if (Modules.clsData_Utilities.CheckMenuItemExists(this.CMBMNU_MenuItemName.Text))
                    {
                        MessageBox.Show("Cannot Add " + this.CMBMNU_MenuItemName.Text + " As It Already Exists!", "Duplicate Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (!blnNew)
                {
                    //if existing record check something to save!
                    if (this.DSTMenuItems.Tables[Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS].GetChanges() == null)
                    {

                        MessageBox.Show("Error Nothing Changed", "Nothing To Save!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                try
                {
                    using (SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                    {
                        SQLConn.Open();
                        //create command objects for each table
                        SQLCmdMenuItems = new SqlCommand("SELECT;", SQLConn);

                        //start transction
                        SQLTransaction = SQLConn.BeginTransaction();
                        //assign commands to the transaction
                        SQLCmdMenuItems.Transaction = SQLTransaction;

                        try
                        {
                            if (!blnNew)
                            {
                                if (DSTMenuItems.Tables[Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS].Rows[0].RowState == DataRowState.Modified)
                                {
                                    //Update records!
                                    SQLCmdMenuItems.CommandText = "UPDATE " + Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS + " " +
                                                              "SET MNU_MenuItemName = '" + this.CMBMNU_MenuItemName.Text + "', MNU_MenuItemObject ='" + this.TXTMNU_MenuItemObject.Text + "', MNU_Type ='" + this.CMBMNU_Type.Text +
                                                              "', MNU_DisplayWhere = '" + this.CMBMNU_DisplayWhere.Text + "' WHERE  MNU_MenuItemName ='" + this.CMBMNU_MenuItemName.Text + "';";
                                    SQLCmdMenuItems.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                //add new records 
                                SQLCmdMenuItems.CommandText = "INSERT INTO " + Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS + " (MNU_MenuItemName, MNU_MenuItemObject, MNU_Type, MNU_DisplayWhere) " +
                                                          "VALUES('" + this.CMBMNU_MenuItemName.Text + "','" + this.TXTMNU_MenuItemObject.Text + "','" + this.CMBMNU_Type.Text + "','" + this.CMBMNU_DisplayWhere.Text + "');";

                                SQLCmdMenuItems.ExecuteNonQuery();
                            }


                            //write changes
                            SQLTransaction.Commit();
                            //clear form dataset

                            if (this.BNSMenuItems.Count > 0)
                            {
                                this.BNSMenuItems.RemoveCurrent();
                            }

                            //reset form
                            ResetForm("", true);
                            MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            SQLTransaction.Rollback();
                            MessageBox.Show("Error Saving Data:\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        SQLConn.Close();
                    }
                }
                catch (Exception ex)
                {
                    //Whoops!                
                    MessageBox.Show("Error Accessing Database:\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //*****form events**********
        private void BTNClose_Click(object sender, EventArgs e)
        {
            Modules.clsView_Utilities.RemoveFromOpenForms(this.ParentForm, this.Text);
            this.Close();
        }

        private void frmMenuItems_Utilities_Load(object sender, EventArgs e)
        {
            if (Modules.clsData_Utilities.CheckForAreas() == false)
            {
                //no lots for location!
                MessageBox.Show("No Menu Sections Data Available - Cannot Continue!\n\nPlease Check Stock UOM Maintenance and Stock Product Family Maintenance Screens For Data ", "No Records Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                blnShow = false;
            }

            Modules.clsData_Utilities.PopulateComboBoxes(this.CMBMNU_MenuItemName, Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS, "", "", "", "", "", false);
            Modules.clsData_Utilities.PopulateComboBoxes(this.CMBMNU_DisplayWhere, Modules.clsData_Utilities.CNST_STR_MENU_AREAS, "", "", "", "", "", false);
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Edit");
        }

        private void CMBMNU_Type_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void CMBMNU_DisplayWhere_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void BTNNew_Click(object sender, EventArgs e)
        {
            //reset form
            ResetForm("", true);
            blnNew = true;
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: New");
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            //save data
            SaveRecord();
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete Record?", "Erase Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;
            }

            //delete group
            DeleteRecord();
        }

        private void CMBMNU_MenuItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams

                   Load selected record

            */


            if (blnNew)
            {
                //if new record do nothing
                return;
            }

            if (this.CMBMNU_MenuItemName.SelectedIndex != -1)
            {

                if (this.TXTHidden.Text == this.CMBMNU_MenuItemName.Text)
                {
                    return;
                }

                if (!blnLoading)
                {
                    //if no records load!
                    LoadRecord();
                }
            }
        }

        private void CMBMNU_MenuItemName_Leave(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
              If not new item check if type value exists in list

            */
                if (blnNew)
                {
                    if (Modules.clsData_Utilities.CheckMenuItemExists(this.CMBMNU_MenuItemName.Text))
                    {
                        MessageBox.Show("Cannot Add " + this.CMBMNU_MenuItemName.Text + " As It Already Exists!", "Duplicate Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.CMBMNU_MenuItemName.Focus();
                        return;
                    }
                    //if new record do nothing
                    return;
                }

                if (this.CMBMNU_MenuItemName.SelectedIndex != -1 || this.CMBMNU_MenuItemName.Text.Length !=0)
                {
                    if (Modules.clsView_Utilities.ValueInCombobox(this.CMBMNU_MenuItemName, this.CMBMNU_MenuItemName.Text))
                    {
                        if (this.TXTHidden.Text == this.CMBMNU_MenuItemName.Text)
                        {
                            return;
                        }


                        if (!blnLoading)
                        {
                            //if no records load!
                            LoadRecord();
                        }
                    }
                }
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        /*
          Created 07/07/2025 By Roger Williams

          undoes changes if new just clear form 

        */
        {


            if (MessageBox.Show("Changes Made Undo?", "Lose Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;              
            }

            if (!blnNew)
            {
                this.BNSMenuItems.CancelEdit();
            }
            else
            {
                ResetForm("", true);
            }
        }
    }
}
