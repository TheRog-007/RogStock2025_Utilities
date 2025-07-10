using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025_Utilities.Screens
{
    public partial class frmThemeMaintenance : Form
    {
        //list used to store theme colours
        string[,,] aryThemeColours = new string[100, 100, 100];
        int intAryPos = 1;
        bool blnNew = false;

        public frmThemeMaintenance()
        {
            InitializeComponent();
        }

        //***other subs/funcs********
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
            //     Modules.clsView_Utilities.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, blnEnable);
            //     Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Edit");
            //     Modules.clsData_Utilities.PopulateComboBoxes(this.CMBMNU_MenuItemName, Modules.clsData_Utilities.CNST_STR_MENU_MENUITEMS, "", "", "", "", "");
            blnNew = false;
        }

        private void CustomKeyDown(object sender, KeyEventArgs e)
        {

            /*
             Created 09/07/2025 By Roger Williams
         
             ensures ALL comboboxes are "locked" from user key entry
            
            */

            e.SuppressKeyPress = true;

        }
        private void SaveRecord()
        {
            /*
             Created 09/07/2025 By Roger Williams
         
             saves theme to theme colours file: \Resources\rogstock2025theme.thm
             stores values from temp  theme array            

            */

            string strTemp = "";
            StreamWriter stmWrite = null;
            int intNum = 0;

            strTemp = Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\rogstock2025theme.thm";

            try
            {
                stmWrite = new StreamWriter(strTemp);

                for (intNum = 0; intNum != intAryPos; intNum++)
                {
                    stmWrite.WriteLine(aryThemeColours[intNum, 0, 0] + "," + aryThemeColours[0, intNum, 0] + "," + aryThemeColours[0, 0, intNum]);
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Accessing Theme File!", "File Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            stmWrite.Close();
        }


    




        //**************form events**********
        private void BTNClose_Click(object sender, EventArgs e)
        {
            Modules.clsView_Utilities.RemoveFromOpenForms(this.ParentForm, this.Text);
            this.Close();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void frmThemeMaintenance_Load(object sender, EventArgs e)
        {
            this.CMBControlType.KeyDown += CustomKeyDown;
        }

        private void CMBControlType_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
