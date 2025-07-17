using RogStock2025_Utilities.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
/*
   Created 17/02/2025 By Roger Williams

  handles GUI manipulation

*/
namespace RogStock2025_Utilities.Modules
{
    internal static class clsView_Utilities
    {
        //used for theme custom line colour
        static int clrLine = 0;
        //list used to store theme colours
        static string[,,] aryThemeColours = new string[51, 51, 51];
        static int intAryPos = 1;
        static Form frmMain = null;

        //used for readonly datagrid columns
        public static Color CNST_INT_READONLYCOLUMNSCOLOUR = Color.LightGray; // .DarkGray;
        public static string CNST_STR_OPENFORMSCONTROL = "CMBOpenScreens";

        //list of colours used by theme maintenance and colour palette forms

        //Note: there are actually 137 solidcolours but some where deleted
        //      as they looked the same as others!
        public static string[] aryColours = {
                "Aqua",
                "Aquamarine",
                "Black",
                "Blue",
                "BlueViolet",
                "Brown",
                "BurlyWood",
                "CadetBlue",
                "Chartreuse",
                "Chocolate",
                "Coral",
                "CornflowerBlue",
                "Crimson",
                "Cyan",
                "DarkBlue",
                "DarkCyan",
                "DarkGoldenrod",
                "DarkGray",
                "DarkGreen",
                "DarkKhaki",
                "DarkMagenta",
                "DarkOliveGreen",
                "DarkOrange",
                "DarkOrchid",
                "DarkRed",
                "DarkSalmon",
                "DarkSeaGreen",
                "DarkSlateBlue",
                "DarkSlateGray",
                "DarkTurquoise",
                "DarkViolet",
                "DeepPink",
                "DeepSkyBlue",
                "DodgerBlue",
                "Firebrick",
                "FloralWhite",
                "ForestGreen",
                "Fuchsia",
                "Gold",
                "Goldenrod",
                "Gray",
                "Green",
                "GreenYellow",
                "HotPink",
                "IndianRed",
                "Indigo",
                "Ivory",
                "Khaki",
                "LawnGreen",
                "LemonChiffon",
                "LightBlue",
                "LightCoral",
                "LightGray",
                "LightGreen",
                "LightPink",
                "LightSalmon",
                "LightSeaGreen",
                "LightSkyBlue",
                "LightSteelBlue",
                "Lime",
                "LimeGreen",
                "Magenta",
                "Maroon",
                "MediumAquamarine",
                "MediumBlue",
                "MediumOrchid",
                "MediumPurple",
                "MediumSeaGreen",
                "MediumSlateBlue",
                "MediumSpringGreen",
                "MediumTurquoise",
                "MediumVioletRed",
                "MidnightBlue",
                "Moccasin",
                "NavajoWhite",
                "Navy",
                "Olive",
                "OliveDrab",
                "Orange",
                "OrangeRed",
                "Orchid",
                "PaleGoldenrod",
                "PaleGreen",
                "PaleTurquoise",
                "PaleVioletRed",
                "PeachPuff",
                "Peru",
                "Pink",
                "Plum",
                "PowderBlue",
                "Purple",
                "Red",
                "RosyBrown",
                "RoyalBlue",
                "SaddleBrown",
                "Salmon",
                "SandyBrown",
                "SeaGreen",
                "Sienna",
                "Silver",
                "SkyBlue",
                "SlateGray",
                "SpringGreen",
                "SteelBlue",
                "Tan",
                "Teal",
                "Thistle",
                "Tomato",
                "Turquoise",
                "Violet",
                "Wheat",
                "White",
                "Yellow",
                "YellowGreen"
        };



        //************* internal class for colour "theme"
        internal class clsMenuColourScheme : ToolStripProfessionalRenderer
        {
            public clsMenuColourScheme() : base(new clsMenuColours()) { }
        }

        public class clsMenuColours : ProfessionalColorTable
        {

            //  Color menu
            /// <summary>
            /// Gets the starting color of the gradient used when 
            /// a top-level System.Windows.Forms.ToolStripMenuItem is pressed.
            /// </summary>

            public override Color MenuItemPressedGradientBegin => Color.DimGray;

            /// <summary>
            /// Gets the end color of the gradient used when a top-level 
            /// System.Windows.Forms.ToolStripMenuItem is pressed.
            /// </summary>
            public override Color MenuItemPressedGradientEnd => Color.DimGray;

            /// <summary>
            /// Gets the border color to use with a 
            /// System.Windows.Forms.ToolStripMenuItem.
            /// </summary>
            public override Color MenuItemBorder => Color.DarkGreen;

            /// <summary>
            /// Gets the starting color of the gradient used when the 
            /// System.Windows.Forms.ToolStripMenuItem is selected.
            /// </summary>
            public override Color MenuItemSelectedGradientBegin => Color.DarkGreen; // Silver;

            /// <summary>
            /// Gets the end color of the gradient used when the 
            /// System.Windows.Forms.ToolStripMenuItem is selected.
            /// </summary>
            public override Color MenuItemSelectedGradientEnd => Color.DarkGreen;

            /// <summary>
            /// Gets the solid background color of the 
            /// System.Windows.Forms.ToolStripDropDown.
            /// </summary>
            public override Color ToolStripDropDownBackground => Color.Olive;

            /// <summary>
            /// Gets the starting color of the gradient used in the image 
            /// margin of a System.Windows.Forms.ToolStripDropDownMenu.
            /// </summary>
            public override Color ImageMarginGradientBegin => Color.DimGray;

            /// <summary>
            /// Gets the middle color of the gradient used in the image 
            /// margin of a System.Windows.Forms.ToolStripDropDownMenu.
            /// </summary>
            public override Color ImageMarginGradientMiddle => Color.DimGray;

            /// <summary>
            /// Gets the end color of the gradient used in the image 
            /// margin of a System.Windows.Forms.ToolStripDropDownMenu.
            /// </summary>
            public override Color ImageMarginGradientEnd => Color.DimGray;

            /// <summary>
            /// Gets the color to use to for shadow effects on 
            /// the System.Windows.Forms.ToolStripSeparator.
            /// </summary>
            public override Color SeparatorDark => Color.White;

        }

        public static int PopulateThemeArraysForForm(ref string[,,] aryThemeColoursNew, ref string[,,] aryThemeColoursOld)
        {
            int intNum = 1;

            while (aryThemeColours[intNum, 0, 0] != null)
            {
                aryThemeColoursNew[intNum, 0, 0] = aryThemeColours[intNum, 0, 0];
                aryThemeColoursNew[0, intNum, 0] = aryThemeColours[0, intNum, 0];
                aryThemeColoursNew[0, 0, intNum] = aryThemeColours[0, 0, intNum];

                aryThemeColoursOld[intNum, 0, 0] = aryThemeColours[intNum, 0, 0];
                aryThemeColoursOld[0, intNum, 0] = aryThemeColours[0, intNum, 0];
                aryThemeColoursOld[0, 0, intNum] = aryThemeColours[0, 0, intNum];
                intNum++;
            }

            return intNum;
        }


        public static void ReadThemeData()
        {
            /*
             Created 09/07/2025 By Roger Williams
         
             reads theme colours file: \Resources\rogstock2025theme.thm
             and stores values into theme array            

            */

            string[] aryTemp = null;
            string strTemp = "";
            StreamReader stmRead = null;
            int intNum = 1;

            strTemp = Path.GetDirectoryName(Application.ExecutablePath)+ @"\Resources\rogstock2025theme.thm";

            //if theme file not found just tell user as does not affect application operation
            if (!File.Exists(strTemp))
            {
                MessageBox.Show("Theme File Not Found In Resources Folder!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                intAryPos = 1;
                //fill array with null as this procedure is called when application loads
                //AND when theme maintenance is loaded
                for(intNum =1; intNum != 51;intNum++)
                {
                    aryThemeColours[intNum, 0, 0] = null;
                    aryThemeColours[0, intNum, 0] = null;
                    aryThemeColours[0, 0, intNum] = null;
                }

                stmRead = new StreamReader(strTemp);

                //read file and store in array
                while (stmRead.Peek() != -1)
                {
                    strTemp = stmRead.ReadLine();
                    //split by: ,
                    aryTemp = strTemp.Split(",");

                    aryThemeColours[intAryPos, 0, 0] = aryTemp[0];
                    aryThemeColours[0, intAryPos, 0] = aryTemp[1];
                    aryThemeColours[0, 0, intAryPos] = aryTemp[2];
                    intAryPos++;
                }

                stmRead.Close();
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Accessing Theme File!", "File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }


        public static void SetTheme(Form frmTemp)
        {
            /*
             Created 09/07/2025 By Roger Williams
         
             Sets the passed forms controls to the defined theme in the theme array            

             VARS

             frmTemp    - form to process 
            */

            //if not theme for strange reason just exit
            if (aryThemeColours.Length == 0)
            {
                return;
            }


        }

        public static void RemoveFromOpenForms(string strText)
        {
            /*
             Created 10/03/2025 By Roger Williams
         
             remove from open forms combobox on the main form
             hides sidebar
            
             VARS

             strText        - text to remove

             Note: also makes sure not duplicating entires

            */

            Control[] aryTemp;
            ComboBox CMBTemp = null;
           
            aryTemp = frmMain.Controls.Find(CNST_STR_OPENFORMSCONTROL, true);
            //TSCMBOpenForms
            foreach (Control ctlTemp in aryTemp)
            {
                if (ctlTemp != null)
                {
                    CMBTemp = (ComboBox)ctlTemp;
                    //remove if already exists
                    CMBTemp.Items.Remove(strText);
                    CMBTemp.Sorted = true;
                    CMBTemp.Update();
                }
            }
        }
        public static void AddToOpenForms(string strText)
        {
            /*
             Created 10/03/2025 By Roger Williams
         
             stores form name in open forms combobox on the main form
             hides sidebar
            
             VARS

             strText        - text to add

             Note: also makes sure not duplicating entires

            */

            Control[] aryTemp;
            ComboBox CMBTemp = null;
       
            aryTemp = frmMain.Controls.Find(CNST_STR_OPENFORMSCONTROL, true);
            //TSCMBOpenForms
            foreach (Control ctlTemp in aryTemp)
            {
                if (ctlTemp != null)
                {
                    CMBTemp = (ComboBox)ctlTemp;
                    //remove if already exists
                    CMBTemp.Items.Remove(strText);
                    CMBTemp.Items.Add(strText);
                    CMBTemp.Sorted = true;
                }
            }

         
        }

        public static void OpenForm(string strWhat)
        {
            /*
               Created 17/02/2025 By Roger Williams

               opens form adds to open forms combobox sets as MDI child

            */
       
            Form frmTemp = null;
            Button BTNTemp = null;

            //first make sure form not already open
            foreach (Form frmFind in Application.OpenForms)
            {
                if (frmFind.Text == strWhat)
                {
                    return;
                }
            }

            switch (strWhat)
            {
                case "Sections":
                    frmTemp = new frmSections_Utilities();
                    break;

                case "Groups":
                    frmTemp = new frmGroups_Utilities();
                    break;

                case "Menu Items":
                    frmTemp= new frmMenuItems_Utilities();
                    break;
                case "Menu Security":
                    frmTemp= new frmMenuSecurity_Utilities();
                    break;
                case "Login Maintenance":
                    frmTemp = new frmLoginMaintenance();
                    break;
                case "Theme Maintenance":
                    frmTemp = new frmThemeMaintenance();
                    break;
            }

            if (frmTemp != null)
            {
                if (frmMain == null)
                {
                    frmMain = Application.OpenForms["frmMain_Utilities"];
                }

                if (frmMain != null)
                {
                    frmTemp.MdiParent = frmMain;
                    //add to open form combobox
                    AddToOpenForms(frmTemp.Text);
                    BTNTemp = (Button)frmMain.Controls["BTNShowHide"];
                    BTNTemp.PerformClick();
                }
                //foreach (Form frmFind in Application.OpenForms)
                //{
                //    if (frmFind.Name  == "frmMain_Utilities")
                //    {
                //        frmTemp.MdiParent = frmFind;
                //        //add to open form combobox
                //        AddToOpenForms(frmFind, frmTemp.Text);
                //        //hide menu
                //        foreach (Form frmMainMenu in Application.OpenForms)
                //        {
                //            if (frmMainMenu.Name == "frmMain_Utilities")
                //            {
                //                BTNTemp = (Button)frmMainMenu.Controls["BTNShowHide"];
                //                BTNTemp.PerformClick();
                //            }
                //        }
                //        break;
                //    }
                // }

                //position form
                frmTemp.StartPosition = FormStartPosition.Manual;
                frmTemp.Left = 50;
                frmTemp.Top = 5;
                frmTemp.Visible = true;
            }
        }

        public static List<string> GetAnyTreeNodesSelected(TreeNode ndeParent)
        {
            /*
               Created 08/07/2025 By Roger Williams

               Returns a list for any nodes with tag of 1 AND checked


               VARS

               ndeParent    - where to search

            */

            List<string> lstNodes = new List<string>();

            void CheckAllChildren(TreeNode ndeParent)
            {
                foreach (TreeNode ndeTemp in ndeParent.Nodes)
                {

                    if (ndeTemp.Tag != null)
                    {
                        if (ndeTemp.Checked)
                        {
                            lstNodes.Add(ndeTemp.Text);
                        }
                    }

                    CheckAllChildren(ndeTemp);
                }
            }

            CheckAllChildren(ndeParent);
            return lstNodes;
        }

        public static List<string> GetAnyTreeItemSelected(TreeNode ndeParent)
        {
            /*
               Created 08/07/2025 By Roger Williams

               Returns a list for any item in nodes with tag of 1 AND checked


               VARS

               ndeParent    - where to search

            */

            List<string> lstTemp= new List<string>();

            void CheckAllChildren(TreeNode ndeParent)
            {
                foreach (TreeNode ndeTemp in ndeParent.Nodes)
                {

                    if (ndeTemp.Tag != null)
                    {
                        if (ndeTemp.Checked)
                        {
                            lstTemp.Add(ndeTemp.Text);
                        }
                    }

                    CheckAllChildren(ndeTemp);
                }
            }

            CheckAllChildren(ndeParent);
            return lstTemp;
        }

        public static void FindValueInTreeAndCheck(TreeNode ndeParent, string strValue)
        {
            /*
               Created 08/07/2025 By Roger Williams

               Checks for any item in nodes with tag of 1 that matches value and sets checked to true

               VARS

               ndeParent    - where to search
               strValue     - value to look for


            */

            bool blnFound = false;

            void CheckAllChildren(TreeNode ndeParent)
            {
                foreach (TreeNode ndeTemp in ndeParent.Nodes)
                {

                    if (ndeTemp.Tag != null)
                    {
                        if (ndeTemp.Text == strValue)
                        {
                            ndeTemp.Checked = true;
                            //check parent i.e. type e.g. Form
                            ndeTemp.Parent.Checked = true;
                            //check parents parent e.g. Inventory
                            ndeTemp.Parent.Parent.Checked = true;
                        }
                    }

                    CheckAllChildren(ndeTemp);
                }
            }

            CheckAllChildren(ndeParent);
        }

        public static bool CheckAnyTreeItemSelected(TreeNode ndeParent)
        {
            /*
               Created 08/07/2025 By Roger Williams

               Checks if checked = true for any item in nodes with tag of 1 AND checked

               VARS

               ndeParent    - where to search

            */

            bool blnFound = false;

            void CheckAllChildren(TreeNode ndeParent)
            {
                foreach (TreeNode ndeTemp in ndeParent.Nodes)
                {

                    if (ndeTemp.Tag != null)
                    {
                        if (ndeTemp.Checked)
                        {
                            blnFound = true;
                        }
                    }

                    CheckAllChildren(ndeTemp);
                }
            }

            CheckAllChildren(ndeParent);
            return blnFound;
        }

        public static void ResetTree(TreeNode ndeParent)
        {
            /*
               Created 08/07/2025 By Roger Williams

               Resets checked to false for all items in nodes with tag of 1

               VARS

               ndeParent    - where to search

            */
            void ResetAllChildren(TreeNode ndeParent)
            { 
                foreach (TreeNode ndeTemp in ndeParent.Nodes)
                {

                    ndeTemp.Checked = false;
                    ResetAllChildren(ndeTemp);
                }
            }

            ResetAllChildren(ndeParent);
        }

        public static void FormLocationChanged(object sender, EventArgs e)
        {
            /*
               Created 13/03/2025 By Roger Williams

               Stops form being moved over show menu button!

            */
            Form frmTemp = (Form)sender;
            
            if (frmTemp.Left < 20)
            {
                frmTemp.Left = 20;
            }
        }

        public static void OpenSelectedForm(string strForm)
        {
            /*
               Created 10/03/2025 By Roger Williams

               shows the form in strForm

            */


            foreach (Form frmTemp in Application.OpenForms)
            {
                if (frmTemp.Text == strForm)
                {
                    frmTemp.BringToFront();
                }
            }

        }
    
        public static void EnableDisableForm(Form frmTemp, string strFirstControl, bool blnEnable)
        {
            /*
               Created 18/02/2025 By Roger Williams

                enables/disables form controls except:
            
                btnNew, btnClose and control name passed in strFirstControl

            */

            foreach (Control ctlTemp in frmTemp.Controls)
            {
                try
                {
                    if (ctlTemp.Name != strFirstControl && ctlTemp.Name != "BTNNew" && ctlTemp.Name != "BTNClose")
                    {
                        ctlTemp.Enabled = blnEnable;
                    }
                }
                catch (Exception ex)
                {
                    //Whoops! - ignore only here if looking for a property a control does not have
                }
            }


        }

        public static void UpdateStatusBar(ToolStripStatusLabel STLSTatus, string strWhat)
        /*
            Created 03/07/2025 By Roger Williams

            updates form status bar with passed text

             VARS
        
            STLStatus   - status bar label
            strWhat     - text

         */


        {
            STLSTatus.Text = strWhat;
        }

        public static void ResetForm(Form frmTemp, string strIgnore)
        {
            /*
             Created 18/02/2025 By Roger Williams

             resets the passed form vars controls except strIgnore

              resets:
                - textbox
                - checkbox
                - listview
                - treeview
                - combobox
                - radiobutton

            */


            int intNum = 0;
 
            foreach (Control ctlTemp in frmTemp.Controls)
            {
                if (ctlTemp is TabControl)
                {
                    //iterate through tab page controls
                    for (intNum = 0; intNum < ((TabControl)ctlTemp).TabPages.Count; intNum++)
                    {
                        foreach (Control ctlTemp2 in ((TabControl)ctlTemp).TabPages[intNum].Controls)
                        {
                            try
                            {
                                if (ctlTemp2.Name != strIgnore)
                                {
                                    if (ctlTemp2 is ComboBox || ctlTemp2 is TextBox)
                                    {
                                        ctlTemp2.Text = "";
                                    }
                                    if (ctlTemp2 is CheckBox)
                                    {
                                        ((CheckBox)ctlTemp2).Checked = false;
                                    }
                                    if (ctlTemp2 is RadioButton)
                                    {
                                        ((RadioButton)ctlTemp2).Checked = false;
                                    }
                                    if (ctlTemp2 is ListView)
                                    {
                                        ((ListView)ctlTemp2).Items.Clear();
                                    }
                                    if (ctlTemp2 is TreeView)
                                    {
                                        ((TreeView)ctlTemp2).Nodes.Clear();
                                    }
                                    if (ctlTemp2 is NumericUpDown)
                                    {
                                        ((NumericUpDown)ctlTemp2).Value = ((NumericUpDown)ctlTemp2).Minimum;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                //whoops! - ignore only here if looking for a property a control does not have
                            }
                        }
                    }
                }
                //is it a panel?
                if (ctlTemp is Panel)
                {
                    //iterate through panel controls
                    foreach (Control ctlTemp2 in ((Panel)ctlTemp).Controls)
                    {
                        try
                        {
                            if (ctlTemp2.Name != strIgnore)
                            {
                                if (ctlTemp2 is ComboBox || ctlTemp2 is TextBox)
                                {
                                    ctlTemp2.Text = "";
                                }
                                if (ctlTemp2 is CheckBox)
                                {
                                    ((CheckBox)ctlTemp2).Checked = false;
                                }
                                if (ctlTemp2 is RadioButton)
                                {
                                    ((RadioButton)ctlTemp2).Checked = false;
                                }
                                if (ctlTemp2 is ListView)
                                {
                                    ((ListView)ctlTemp2).Items.Clear();
                                }
                                if (ctlTemp2 is TreeView)
                                {
                                    ((TreeView)ctlTemp2).Nodes.Clear();
                                }
                                if (ctlTemp2 is NumericUpDown)
                                {
                                    ((NumericUpDown)ctlTemp2).Value = ((NumericUpDown)ctlTemp2).Minimum;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //whoops! - ignore only here if looking for a property a control does not have
                        }
                    }
                }

                try
                {
                    if (ctlTemp.Name != strIgnore)
                    { 
                        if (ctlTemp is ComboBox || ctlTemp is TextBox)
                        {
                            ctlTemp.Text = "";
                        }
                        if (ctlTemp is CheckBox)
                        {
                            ((CheckBox)ctlTemp).Checked = false;
                        }
                        if (ctlTemp is RadioButton)
                        {
                            ((RadioButton)ctlTemp).Checked = false;
                        }
                        if (ctlTemp is ListView)
                        {
                            ((ListView)ctlTemp).Items.Clear();
                        }
                        if (ctlTemp is TreeView)
                        {
                            ((TreeView)ctlTemp).Nodes.Clear();
                        }
                        if (ctlTemp is NumericUpDown)
                        {
                            ((NumericUpDown)ctlTemp).Value = ((NumericUpDown)ctlTemp).Minimum;
                        }
                    }

                }
                catch (Exception ex)
                {
                    //whoops! - ignore only here if looking for a property a control does not have
                }
            }
        }

        public static bool ValueInCombobox(ComboBox CMBTemp, string strValue)
        {
            /*
             Created 05/03/2025 By Roger Williams

             Validates contens of passed combobox to see if passed value is in the list

             VARS

             cbmtemp        - combobox to validate
             strvalue       - what to find  

             returns true if ok

            */

            int intNum = 0;

            if (strValue == "")
            {
                return false;
            }

            intNum = CMBTemp.Items.IndexOf(strValue);

            if (intNum == -1)
            {
                CMBTemp.Text = "";
                CMBTemp.Focus();
                MessageBox.Show("Data Not In List", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
        public static bool ValidateRequiredFields(Form frmTemp)
        {
        /*
         Created 17/02/2025 By Roger Williams

         Validates form required fields populated - uses tag if 1 required field

         VARS

         frmTemp        - form to validate

         returns true if ok

        */
            List<string> lstErrors = new List<string>();
            string strTemp = "";
            int intNum = 0;

            foreach (Control ctlTemp in frmTemp.Controls)
            {
                try
                {
                    if (ctlTemp is TabControl)
                    {
                        //iterate through tab page controls
                        for (intNum = 0; intNum < ((TabControl)ctlTemp).TabPages.Count; intNum++)
                        {
                            ((TabControl)ctlTemp).SelectedIndex = intNum;

                            //check each page for required fields
                            foreach (Control ctlTab in ((TabControl)ctlTemp).SelectedTab.Controls)
                            {
                                try
                                {
                                    if (ctlTab.Tag.ToString() == "1")
                                    {
                                        strTemp = ctlTab.Name.Substring(3, ctlTab.Name.Length - 3);

                                        if (ctlTab.Text.Length == 0)
                                        {
                                            strTemp = ((TabControl)ctlTemp).SelectedTab.Controls["LBL" + strTemp].Text;
                                            lstErrors.Add(strTemp);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //whoops! - ignore only here if looking for a property a control does not have
                                }
                            }
                        }
                        //reset tab selected
                        ((TabControl)ctlTemp).SelectedIndex = 0;
                    }

                    //check if panel
                    if (ctlTemp is Panel)
                    {
                        //iterate panel controls
                        //check each page for required fields
                        foreach (Control ctlTab in ((Panel)ctlTemp).Controls)
                        {
                            try
                            {
                                if (ctlTab.Tag.ToString() == "1")
                                {
                                    strTemp = ctlTab.Name.Substring(3, ctlTab.Name.Length - 3);

                                    if (ctlTab.Text.Length == 0)
                                    {
                                        strTemp = ((Panel)ctlTemp).Controls["LBL" + strTemp].Text;
                                        lstErrors.Add(strTemp);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                //whoops! - ignore only here if looking for a property a control does not have
                            }
                        }
                    }

                    if (ctlTemp.Tag.ToString() == "1")
                    {
                        strTemp = ctlTemp.Name.Substring(3, ctlTemp.Name.Length - 3);

                        if (ctlTemp.Text.Length == 0)
                        {
                            strTemp = frmTemp.Controls["LBL" + strTemp].Text;
                            lstErrors.Add(strTemp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //whoops! - ignore only here if looking for a property a control does not have
                }
            }

            if (lstErrors.Count != 0)
            {
              strTemp = "";
              //create messagebox to user showing missing required fields
              for (intNum = 0; intNum != lstErrors.Count; intNum++)
              {
                  strTemp = strTemp + lstErrors[intNum].ToString() + "\n";
              }

              MessageBox.Show("These Fields Are Missing Required Data:\n\n" + strTemp, "Required Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                return true;
            }
        }

        //class end
    }
    }
