using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


/*

  this form allows ONLY creation of groups

  Why?

  Groups are linked to to the usersgroups table so if a group is edited so must the usersgroups entries

  Therefore system design denotes any such activity is a "mass replace" and should be done in a seperate form

  Deletes will need to delete from groups and usergroups


*/
namespace RogStock2025_Utilities.Screens
{
    public partial class frmGroups_Utilities : Form
    {
        bool blnNew = false;
        bool blnLoading = false;
        Pen penTemp;

        public frmGroups_Utilities()
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
            this.CMBGRP_Group.Text = "";
            //load users comnbo with current users
            Modules.clsData_Utilities.PopulateComboBoxes(this.CMBGRP_Group, Modules.clsData_Utilities.CNST_STR_MENU_GROUPS, "GRP_Group", "", "", "", "", true);
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Edit");
            Modules.clsView_Utilities.ResetTree(this.TVMenuItems.Nodes[0]);
            Modules.clsView_Utilities.ResetTree(this.TVMenuItems_old.Nodes[0]);
            blnNew = false;
            this.TXTHidden.Text = "";
        }


        private void SaveRecord()
        {
            /*
              Created 03/07/2025 By Roger Williams

              Creates/updates MENU_Group record  
              Iterates through treeview writing checked menuitems

              Note: checks first if any treeview items checked if none selected tells user and exits
                    checks if not new if anything has changed if not tells user and exits

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlTransaction SQLTransaction = null;
            bool blnFound = false;
            List<string> lstMenuItems = new List<string>();

            //no group name don't process
            if (this.CMBGRP_Group.Text.Length == 0) return;

            if (blnNew)
            {
                if (Modules.clsData_Utilities.CheckGroupExists(this.CMBGRP_Group.Text))
                {
                    MessageBox.Show("Group Already Exists!", "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //reset form and update users combobox
                    ResetForm("", true);
                    return;
                }
            }

            if (!blnNew)
            {
                //check for changes
                foreach (TreeNode ndeTemp in this.TVMenuItems.Nodes[0].Nodes)
                {
                    if (this.TVMenuItems_old.Nodes.Find(ndeTemp.Text, true)[0].Checked != ndeTemp.Checked)
                    {
                        blnFound = false;
                        break;
                    }
                    else
                    {
                        blnFound = true;
                    }
                }

                if (blnFound)
                {
                    MessageBox.Show("No Changes Made!", "Nothing To Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            blnFound = true;


            //check something selected i.e. at least one treeview node checked
            blnFound = Modules.clsView_Utilities.CheckAnyTreeItemSelected(this.TVMenuItems.Nodes[0]);

            if (!blnFound)
            {
                MessageBox.Show("No Menu Items Selected!", "Nothing To Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
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

                    if (!blnNew)
                    {
                        //for quickness DELETE group and re-create from comboxo and treeview
                        SQLCmd.CommandText = "DELETE" + Modules.clsData_Utilities.CNST_STR_MENU_GROUPS + " WHERE GRP_Group = '" + this.CMBGRP_Group.Text + "';";
                        SQLCmd.ExecuteNonQuery();
                    }

                    //get list of selected menu items from treeview
                    lstMenuItems = Modules.clsView_Utilities.GetAnyTreeItemSelected(this.TVMenuItems.Nodes[0]);

                    //create group
                    foreach (string strTemp in lstMenuItems)
                    {
                        //filter for JUST menuitems as these havw a tag of 1
                        SQLCmd.CommandText = "INSERT INTO " + Modules.clsData_Utilities.CNST_STR_MENU_GROUPS + " (GRP_Group, GRP_MenuItem) VALUES ('" + this.CMBGRP_Group.Text + "','" + strTemp + "');";
                        SQLCmd.ExecuteNonQuery();
                    }

                    SQLTransaction.Commit();
                    //reset form and update menuitem combobox
                    ResetForm("", true);
                    MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void DeleteRecord()
        {
            /*
              Created 03/07/2025 By Roger Williams

              deletes group record using a transaction IF in Menu_UsersGroups then deletes that as well

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmdLogin;
            SqlTransaction SQLTransaction;

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
                    //start transction
                    SQLTransaction = SQLConn.BeginTransaction();

                    try
                    {
                        SQLCmdLogin = new SqlCommand("DELETE FROM " + Modules.clsData_Utilities.CNST_STR_MENU_GROUPS + " WHERE GRP_Group = '" + this.CMBGRP_Group.Text + "';", SQLConn);
                        //assign commands to the transaction
                        SQLCmdLogin.Transaction = SQLTransaction;
                        //delete groups
                        SQLCmdLogin.ExecuteNonQuery();
                        SQLCmdLogin = new SqlCommand("DELETE FROM " + Modules.clsData_Utilities.CNST_STR_MENU_USERSGROUPS + " WHERE USRGRP_Group = '" + this.CMBGRP_Group.Text + "';", SQLConn);
                        //delete from usersgroups
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

        private void PopulateTree()
        {
            /*
              Created 08/07/2025 By Roger Williams

              Populates tree with menu items ordereed by area/name split into forms/operations/reports  
              Also clones tree into tvmenuitems_old

              Sets TAG property to 1 for menu items this is used during save/delete to ensure not working with
              areas and menu item types e.g. Inventory/Forms

            */

            TreeNode ndeArea = null;
            TreeNode ndeType = null;
            TreeNode ndeRoot = null;
            TreeNode ndeItem = null;
            TreeNode ndeArea_old = null;
            TreeNode ndeType_old = null;
            TreeNode ndeRoot_old = null;
            TreeNode ndeItem_old = null;
            SqlConnection SQLConnMenuItems;
            SqlCommand SQLCmdMenuItems;
            SqlDataAdapter DADMenuItems;
            DataSet DSTMenuItems;

            int intRows = 0;
            string strArea = "";
            string strType = "";

            try
            {
                using (SQLConnMenuItems = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                {
                    SQLConnMenuItems.Open();
                    //load MenuItems items
                    SQLCmdMenuItems = new SqlCommand("SELECT * FROM " + Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS + " ORDER BY MNU_DisplayWhere, MNU_Type, MNU_MenuItemName;", SQLConnMenuItems);
                    DADMenuItems = new SqlDataAdapter(SQLCmdMenuItems);
                    DSTMenuItems = new DataSet();
                    intRows = DADMenuItems.Fill(DSTMenuItems, Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS);

                    //any records?
                    if (intRows == 0)
                    {
                        MessageBox.Show("Error Reading Data - No Records!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //clear tree
                        this.TVMenuItems.Nodes.Clear();
                        this.TVMenuItems_old.Nodes.Clear();

                        ndeRoot = this.TVMenuItems.Nodes.Add("Menu Items");
                        ndeRoot_old = this.TVMenuItems_old.Nodes.Add("Menu Items");

                        foreach (DataRow DARTemp in DSTMenuItems.Tables[Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS].Rows)
                        {
                            if (DARTemp["MNU_DisplayWhere"].ToString() != strArea)
                            {
                                strArea = DARTemp["MNU_DisplayWhere"].ToString();
                                strType = DARTemp["MNU_Type"].ToString();

                                //create tree nodes
                                ndeArea = ndeRoot.Nodes.Add(strArea);
                                ndeType = ndeArea.Nodes.Add(strType);
                                ndeArea_old = ndeRoot_old.Nodes.Add(strArea);
                                ndeType_old = ndeArea_old.Nodes.Add(strType);

                            }
                            else
                            {
                                if (DARTemp["MNU_Type"].ToString() != strType)
                                {
                                    ndeType = ndeArea.Nodes.Add(DARTemp["MNU_Type"].ToString());
                                    ndeType_old = ndeArea_old.Nodes.Add(DARTemp["MNU_Type"].ToString());
                                    strType = DARTemp["MNU_Type"].ToString();
                                }
                            }
                            ndeItem = ndeType.Nodes.Add(DARTemp["MNU_MenuItemName"].ToString());
                            ndeItem.Tag = 1;
                            ndeItem_old = ndeType_old.Nodes.Add(DARTemp["MNU_MenuItemName"].ToString());
                            ndeItem_old.Tag = 1;
                        }

                        this.TVMenuItems.Nodes[0].Expand();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadRecord()
        {
            /*
              Created 08/07/2025 By Roger Williams

              Loads record -populates treeview with menuitems selected i.e. sets checkbox to true

            */
            SqlConnection SQLConnMenuItems;
            SqlCommand SQLCmdMenuItems;
            SqlDataAdapter DADLOTMenuItems;
            DataSet DSTLOTMenuItems;
            int intRows = 0;

            blnLoading = true;

            try
            {
                using (SQLConnMenuItems = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                {
                    SQLConnMenuItems.Open();
                    //load MenuItems items
                    SQLCmdMenuItems = new SqlCommand("SELECT * FROM " + Modules.clsData_Utilities.CNST_STR_MENU_GROUPS + " WHERE GRP_Group = '" + this.CMBGRP_Group.Text + "';", SQLConnMenuItems);
                    DADLOTMenuItems = new SqlDataAdapter(SQLCmdMenuItems);
                    DSTLOTMenuItems = new DataSet();
                    intRows = DADLOTMenuItems.Fill(DSTLOTMenuItems, Modules.clsData_Utilities.CNST_STR_MENU_GROUPS);

                    //any records?
                    if (intRows == 0)
                    {
                        MessageBox.Show("No Records Found!", "No Matching Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        foreach (DataRow DARTemp in DSTLOTMenuItems.Tables[Modules.clsData_Utilities.CNST_STR_MENU_GROUPS].Rows)
                        {
                            //select in both treeviews
                            Modules.clsView_Utilities.FindValueInTreeAndCheck(this.TVMenuItems.Nodes[0],DARTemp["GRP_MenuItem"].ToString());
                            Modules.clsView_Utilities.FindValueInTreeAndCheck(this.TVMenuItems_old.Nodes[0], DARTemp["GRP_MenuItem"].ToString());
                        }

                    }
                    //set hidden textbox to stop double loading on combobox leave event
                    this.TXTHidden.Text = this.CMBGRP_Group.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            blnLoading = false;
        }

        //*********** form events *************




        private void BTNClose_Click(object sender, EventArgs e)
        {
            Modules.clsView_Utilities.RemoveFromOpenForms(this.ParentForm, this.Text);
            this.Close();
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
            if (blnNew)
            {
                if (MessageBox.Show("Delete Record?", "Erase Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                if (MessageBox.Show("This Could Also Result In UserGroups Records Being Deleted As Well\n\nDelete Record Anyway?", "Erase Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return;
                }
            }
            //delete user
            DeleteRecord();
        }

        private void frmGroups_Utilities_Load(object sender, EventArgs e)
        {
            //load users comnbo with current users
            Modules.clsData_Utilities.PopulateComboBoxes(this.CMBGRP_Group, Modules.clsData_Utilities.CNST_STR_MENU_GROUPS, "GRP_Group", "", "", "", "", true);
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Edit");
            //create pen for line drawing
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;
            PopulateTree();
        }

        private void CMBGroups_KeyDown(object sender, KeyEventArgs e)
        {
            /*
               Created 04/07/2025 By Roger Williams


               Stop user entering text
            */
            //     e.SuppressKeyPress = true;
        }

        private void CMBGroups_Leave(object sender, EventArgs e)
        {
            if (this.TXTHidden.Text != this.CMBGRP_Group.Text)
            {
                if (this.CMBGRP_Group.Text.Length != 0)
                {
                    if (blnNew)
                    {
                        if (Modules.clsData_Utilities.CheckGroupExists(this.CMBGRP_Group.Text))
                        {
                            MessageBox.Show("Group Already Exists!", "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //reset form and update menuitem combobox
                            this.CMBGRP_Group.Focus();
                            return;
                        }
                    }

                    if (!blnNew)
                    {
                        LoadRecord();
                    }
                }
            }
        }

        private void CMBGRP_MenuItem_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 08/07/2025 By Roger Williams
              
              Draws line across screen
            */
            //draw line

            e.Graphics.DrawLine(penTemp, 0, 60, this.Width, 60);
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*
              Created 08/07/2025 By Roger Williams

              undoes changes if new just clear form and reset treeview else set treeview to copies checkbox status 

            */

            List<string> lstNodes = new List<string>();

            {
                if (MessageBox.Show("Changes Made Undo?", "Lose Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return;
                }

                if (!blnNew)
                {
                    //reset treeview
                    Modules.clsView_Utilities.ResetTree(this.TVMenuItems.Nodes[0]);
                    //get checked nodes from old treeview
                    lstNodes = Modules.clsView_Utilities.GetAnyTreeNodesSelected(this.TVMenuItems_old.Nodes[0]);

                    foreach (string strTemp in lstNodes)
                    {
                        Modules.clsView_Utilities.FindValueInTreeAndCheck(this.TVMenuItems.Nodes[0], strTemp);
                    }
                }
                else
                {
                    ResetForm("", true);
                }
            }
        }

        private void CMBGRP_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CMBGRP_Group.Text.Length != 0)
            {
                if (blnNew)
                {
                    if (Modules.clsData_Utilities.CheckGroupExists(this.CMBGRP_Group.Text))
                    {
                        MessageBox.Show("Group Already Exists!", "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //reset form and update menuitem combobox
                        this.CMBGRP_Group.Focus();
                        return;
                    }
                }

                if (!blnNew)
                {
                    LoadRecord();
                }
            }
        }
    }
}
