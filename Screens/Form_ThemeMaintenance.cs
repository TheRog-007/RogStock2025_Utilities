using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025_Utilities.Screens
{
    /*
       Created 10/07/2025 By Roger Williams
             
       Allows custom theme

       uses:

       \resources\rogstock2025theme.thm

       CSV file with structure:

       <control>,<property>,<colour name>


       Also uses form and code from projects: ColourPaletteTest, TestVariablePassBack

       Uses custom paint event to show radiobutton as a square of the selected colour, this was taken directly from ColourPaletteTest

       

    */
    public partial class frmThemeMaintenance : Form
    {
        //list used to store theme colours
        string[,,] aryThemeColoursOld = new string[51, 51, 51];
        int intAryThemeColoursOldPos = 1;
        string[,,] aryThemeColoursNew = new string[51, 51, 51];
        //Note: arrays are 1 to 51 as want to store 50 possible values

        //for embedded preview form
        Form frmPreview = new Form_ThemePreview();
        //colour palette form
        Form frmColour = new frmColourPalette();
        //for preview form border and other drawing
        Brush bruTemp = new SolidBrush(Color.Black);
        Point pntTemp1 = new Point();
        Point pntTemp2 = new Point();
        Pen penTemp = new Pen(Color.Black);
        Pen penSelectedColour = new Pen(Color.Black);
        //static method is shared between this form and the colour palette form
        public static Color clrSelectedColour = Color.Empty;
        //used by copy/paste colour buttons
        public static Color clrCopyColour = Color.Empty;

        public frmThemeMaintenance()
        {
            InitializeComponent();
        }

        //***other subs/funcs********
        private void ResetForm()
        {
            /*
                 Created 04/07/2025 By Roger Williams

                 Resets form 
      
            */

            //reset form
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Edit");
            this.CMBControl.Text = String.Empty;
            this.CMBProperty.Text = String.Empty;
            this.LBLSelectedColour.Text = String.Empty;
            clrSelectedColour = Color.Empty;
            this.RBSelectedColour.Visible = false;
        }

        private void EmbedPreviewForm()
        {
            /*
              Created 14/07/2025 By Roger Williams

              embeds preview form for preview!

             */


            frmPreview = new Form_ThemePreview();
            frmPreview.TopLevel = false;
            frmPreview.Parent = this;
            frmPreview.Left = 345;
            frmPreview.Top = 20;
            frmPreview.Show();
            frmColour.Hide();

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
             stores values from new theme array    
            
             creates backup copy before saving

            */

            string strTemp1 = "";
            string strTemp2 = "";
            string strDateTime = "";
            StreamWriter stmWrite = null;
            int intNum = 1;

            string AddZero(int intWhat)
            {
                if (intWhat < 10)
                {
                    return "0" + intWhat.ToString();
                }
                else
                {
                    return intWhat.ToString();
                }
            }

            strTemp1 = Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\rogstock2025theme.thm";

            //if exists create backup copy
            if (File.Exists(strTemp1))
            {
                //get datetime for unique copy name
                strTemp2 = Path.GetDirectoryName(strTemp1) + @"\rogstock2025theme";
                strDateTime = "_" + AddZero(DateTime.Now.Day) + AddZero(DateTime.Now.Month) + DateTime.Now.Year.ToString() + ".thm";
                //create backup copy
                File.Copy(strTemp1, strTemp2 + strDateTime, true);
            }

            try
            {
                stmWrite = new StreamWriter(strTemp1);

                while (aryThemeColoursNew[intNum, 0, 0] != null)
                {
                    stmWrite.WriteLine(aryThemeColoursNew[intNum, 0, 0] + "," + aryThemeColoursNew[0, intNum, 0] + "," + aryThemeColoursNew[0, 0, intNum]);
                    intNum++;
                }

                MessageBox.Show("Saved!\n\nBackup Copy Of Theme File Is\n\nResources\\" + strTemp2 + strDateTime, "DataSaved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Accessing Theme File!", "File Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            stmWrite.Close();
        }

        private void FillPropertyComboBox()
        {
            /*
              Created 11/07/2025 By Roger Williams
             
              Fills the property combobox from the new theme array based on the control combobox text
              
            */

            int intNum = 1;

            if (this.CMBControl.Text.Length > 0)
            {
                this.CMBProperty.Items.Clear();
                this.CMBProperty.Text = String.Empty;
                this.LBLSelectedColour.Text = String.Empty;
                this.RBSelectedColour.Visible = false;

                while (aryThemeColoursNew[intNum, 0, 0] != null)
                {
                    if (aryThemeColoursNew[intNum, 0, 0] == this.CMBControl.Text)
                    {
                        //add property to combobox
                        this.CMBProperty.Items.Add(aryThemeColoursNew[0, intNum, 0]);
                    }

                    intNum++;
                }
            }
            else
            {
                //if user has not selected a control do nothing
                return;
            }
        }

        public void GetPropertyColour()
        {
            /*
              
              Modified 15/07/2025 By Roger Williams
              
              Now uses pulic proerty clrselectedcolour

              
              Created 11/07/2025 By Roger Williams
             
              Fills the property combobox from the new theme array based on the control combobox text
              
            */

            int intNum = 1;

            if ((this.CMBControl.Text.Length > 0) && (this.CMBProperty.Text.Length > 0))
            {
                while (aryThemeColoursNew[intNum, 0, 0] != null)
                {
                    if (aryThemeColoursNew[intNum, 0, 0] == this.CMBControl.Text)
                    {
                        if (aryThemeColoursNew[0, intNum, 0] == this.CMBProperty.Text)
                        {
                            //get colour
                            clrSelectedColour = Color.FromName(aryThemeColoursNew[0, 0, intNum]);
                            //show name to user
                            this.LBLSelectedColour.Text = clrSelectedColour.Name;
                            this.RBSelectedColour.Visible = true;
                            this.RBSelectedColour.Refresh();
                            break;
                        }

                    }
                    intNum++;
                }
            }

        }

        public void UpdateNewArray()
        {
            /*
             Created 11/07/2025 By Roger Williams

             put combobox values and color into new array

            */

            int intNum = 1;

            if ((this.CMBControl.Text.Length > 0) && (this.CMBProperty.Text.Length > 0))
            {
                while (aryThemeColoursNew[intNum, 0, 0] != null)
                {
                    if (aryThemeColoursNew[intNum, 0, 0] == this.CMBControl.Text)
                    {
                        if (aryThemeColoursNew[0, intNum, 0] == this.CMBProperty.Text)
                        {
                            //update colour
                            aryThemeColoursNew[0, 0, intNum] = clrSelectedColour.Name;
                            break;
                        }

                    }
                    intNum++;
                }
            }
        }

        public void ResetSingleNewArrayValue()
        {
            /*
             Created 11/07/2025 By Roger Williams

             uses control/property comobox to reset new array single entry from New array

            */

            int intNum = 1;

            if ((this.CMBControl.Text.Length > 0) && (this.CMBProperty.Text.Length > 0))
            {
                while (aryThemeColoursNew[intNum, 0, 0] != null)
                {
                    if (aryThemeColoursNew[intNum, 0, 0] == this.CMBControl.Text)
                    {
                        if (aryThemeColoursNew[0, intNum, 0] == this.CMBProperty.Text)
                        {
                            //update colour
                            aryThemeColoursNew[0, 0, intNum] = aryThemeColoursOld[0, 0, intNum];
                            //get colour
                            clrSelectedColour = Color.FromName(aryThemeColoursNew[0, 0, intNum]);
                            //show name to user
                            this.LBLSelectedColour.Text = clrSelectedColour.Name;
                            this.RBSelectedColour.Visible = true;
                            this.RBSelectedColour.Refresh();
                            break;
                        }
                    }
                    intNum++;
                }

                PreviewChanges(false);
            }
        }


        public void PreviewChanges(bool blnLoad)
        {
            /*
             Modified 17/07/2025 By Roger Williams
             
             added blnLoad so when form_load occurs can "load" the settings into the preview form 

             Created 14/07/2025 By Roger Williams

             updates colours in embedded preview form for preview from aryThemeColoursNew

            */



            int intNum = 1;

            void ApplyToControl(string strWhat, string strProperty)
            {
                /*
                  Created 11/07/2025 By Roger Williams

                  uses control/property comobox to set test control colour property for preview!

                 */

                DataGridView DGVTemp = null;

                switch (strWhat)
                {
                    case "Button":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestButton"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestButton"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "CheckBox":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestCheckBox"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestCheckBox"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "ComboBox":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestComboBox"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestComboBox"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "DataGridView":
                        DGVTemp = (DataGridView)frmPreview.Controls["TestDataGridView"];

                        if (strProperty == "BackgroundColor")
                        {
                            DGVTemp.BackColor = clrSelectedColour;
                        }
                        //if (strProperty == "ForeColor")
                        //{
                        //    DGVTemp.ForeColor = clrSelectedColour;
                        //}
                        if (strProperty == "GridColor")
                        {
                            DGVTemp.GridColor = clrSelectedColour;
                        }
                        if (strProperty == "DefaultCellStyle.BackColor")
                        {
                            DGVTemp.DefaultCellStyle.BackColor = clrSelectedColour;
                        }
                        if (strProperty == "DefaultCellStyle.ForeColor")
                        {
                            DGVTemp.DefaultCellStyle.ForeColor = clrSelectedColour;
                        }
                        if (strProperty == "ColumnHeadersDefaultCellStyle.BackColor")
                        {
                            DGVTemp.ColumnHeadersDefaultCellStyle.BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ColumnHeadersDefaultCellStyle.ForeColor")
                        {
                            DGVTemp.ColumnHeadersDefaultCellStyle.ForeColor = clrSelectedColour;
                        }
                        break;
                    //case "TestDataGridViewColumn":

                    //    break;
                    case "Form":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.ForeColor = clrSelectedColour;
                        }
                        break;
                    case "GroupBox":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestGroupBox"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestGroupBox"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "Label":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestLabel"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestLabel"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "ListBox":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestListBox"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestListBox"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "ListView":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestListView"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestListView"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "NumericUpDown":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestNumericUpDown"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestNumericUpDown"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "Panel":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestPanel"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestPanel"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "RadioButton":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestRadioButton"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestRadioButton"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "StatusStrip":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestStatusStrip"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestStatusStrip"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "TabControl":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestComboBox"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestComboBox"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "TabPage":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestTabControl"].Controls[0].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestTabControl"].Controls[0].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "TextBox":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestTextBox"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestTextBox"].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "ToolStripStatusLabel":
                        if (strProperty == "BackColor")
                        {
                            ((StatusStrip)frmPreview.Controls["TestStatusStrip"]).Items[0].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            ((StatusStrip)frmPreview.Controls["TestStatusStrip"]).Items[0].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "TreeView":
                        if (strProperty == "BackColor")
                        {
                            frmPreview.Controls["TestTreeView"].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmPreview.Controls["TestTreeView"].ForeColor = clrSelectedColour;
                        }
                        break;
                }
            }


            if (!blnLoad)
            {
                //update new array BEFORE preview
                UpdateNewArray();

                if ((this.CMBControl.Text.Length > 0) && (this.CMBProperty.Text.Length > 0))
                {
                    while (aryThemeColoursNew[intNum, 0, 0] != null)
                    {
                        clrSelectedColour = Color.FromName(aryThemeColoursNew[0, 0, intNum]);
                        ApplyToControl(aryThemeColoursNew[intNum, 0, 0], aryThemeColoursNew[0, intNum, 0]);
                        intNum++;
                    }
                }
            }
            else
            {
                while (aryThemeColoursNew[intNum, 0, 0] != null)
                {
                    clrSelectedColour = Color.FromName(aryThemeColoursNew[0, 0, intNum]);
                    ApplyToControl(aryThemeColoursNew[intNum, 0, 0], aryThemeColoursNew[0, intNum, 0]);
                    intNum++;
                }
            }
        }


        public int CopyToOldNewArray()
        {
            /*
             Modified 15/07/2025 By Roger Williams
            
             Now uses function in view module!

             Created 12/07/2025 By Roger Williams

             put theme array in viewmodule data into new/old array 

            */

            return Modules.clsView_Utilities.PopulateThemeArraysForForm(ref aryThemeColoursNew, ref aryThemeColoursOld);

        }

        public void CopyOldToNewArray()
        {
            /*
             Created 11/07/2025 By Roger Williams

             put Old array data into new array for editing used by undo

            */

            int intNum = 1;

            while (aryThemeColoursOld[intNum, 0, 0] != null)
            {
                aryThemeColoursNew[intNum, 0, 0] = aryThemeColoursOld[intNum, 0, 0];
                aryThemeColoursNew[0, intNum, 0] = aryThemeColoursOld[0, intNum, 0];
                aryThemeColoursNew[0, 0, intNum] = aryThemeColoursOld[0, 0, intNum];
                intNum++;
            }
        }

        public void ReadThemeColourData()
        {
            /*
             Modified 15/07/2025 By Roger Williams 
            
             Realised theme file ALREADY loaded on start so skipped that bit!


             Created 09/07/2025 By Roger Williams
         
             reads theme colours file: \Resources\rogstock2025theme.thm
             and stores values into theme array            

             Theme File Format:

             <control>,<property>,<colour>

             Returns

             true if loaded ok

             Note: was using streamreader until yet another major .Net bug hit whereby using it to read a CSV file in the above
                   format resulted in each ReadLine() command returning the line minus the FIRST character
                   thank you Microsoft for over 40 years of perpetual substandard development!

            */

            int intNum = 0;

            //copy theme array into new/old arrays
            intAryThemeColoursOldPos = CopyToOldNewArray();

            //populate CMBControls!
            for (intNum = 1; intNum != intAryThemeColoursOldPos; intNum++)
            {
                if (this.CMBControl.Items.Count > 0)
                {
                    if (this.CMBControl.Items.IndexOf(aryThemeColoursOld[intNum, 0, 0]) == -1)
                    {
                        this.CMBControl.Items.Add(aryThemeColoursOld[intNum, 0, 0]);
                    }
                }
                else
                {
                    this.CMBControl.Items.Add(aryThemeColoursOld[intNum, 0, 0]);
                }
            }
        }

        //**************form events**********
        private void BTNClose_Click(object sender, EventArgs e)
        {
            Modules.clsView_Utilities.RemoveFromOpenForms(this.Text);
            frmColour.Close();
            this.Close();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void frmThemeMaintenance_Load(object sender, EventArgs e)
        {
            this.CMBControl.KeyDown += CustomKeyDown;
            Modules.clsView_Utilities.ReadThemeData();
            ReadThemeColourData();
            ResetForm();
            EmbedPreviewForm();
            PreviewChanges(true);
        }

        private void CMBControlType_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void CMBControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPropertyComboBox();
        }

        private void CMBProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPropertyColour();
        }

        private void CMBProperty_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*
              Created 11/07/2025 By Roger Williams

              resets new array to be a copy of the original theme array 

            */
            {
                if (MessageBox.Show("Changes Made Undo?", "Lose Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return;
                }

                CopyOldToNewArray();
                PreviewChanges(false);
                ResetForm();
            }
        }

        private void BTNColour_Click(object sender, EventArgs e)
        {
            /*
              Created 16/07/2025 By Roger Williams

              shows colour palette form so user can select a colour
              has shared property intSelectedColor that the colour palette form
              uses to store the selcted colour

              if no colour selected will be color.empty

              Also set color to empty after processing selection!

            */

            //reset colour before showing form
            clrSelectedColour = Color.Empty;

            frmColour.StartPosition = FormStartPosition.CenterScreen;
            frmColour.ShowDialog();

            if (clrSelectedColour != Color.Empty)
            {
                this.LBLSelectedColour.Text = clrSelectedColour.Name;
                //reset colour property
                this.RBSelectedColour.Visible = true;
                this.RBSelectedColour.Refresh();
                PreviewChanges(false);
            }
            else
            {
                this.RBSelectedColour.Visible = false;
            }
        }

        private void BTNUndoSingle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Changes Made Undo?", "Lose Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;
            }

            ResetSingleNewArrayValue();
        }
        
        private void frmThemeMaintenance_Paint(object sender, PaintEventArgs e)
        {
            //320 x 590 top 15 draw border around embedded form
            e.Graphics.DrawRectangle(penTemp, 338, 15, 550, 400);

            pntTemp1.Y = 430;
            pntTemp1.X = 0;
            pntTemp2.Y = 430;
            pntTemp2.X = this.Width;
            //draw line above main controls
            e.Graphics.DrawLine(penTemp, pntTemp1, pntTemp2);
        }

        private void RBSelectedColour_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 16/07/2025 By Roger Williams

              radiobutton paint event for drawing the selected colour 
            */

            Rectangle rectTemp = e.ClipRectangle;

            if (clrSelectedColour == Color.Empty)
            {
                return;
            }

            e.Graphics.DrawRectangle(penSelectedColour, rectTemp);
            e.Graphics.FillRectangle(new SolidBrush(clrSelectedColour), rectTemp);
        }

        private void BTNCopy_Click(object sender, EventArgs e)
        {
            clrCopyColour = clrSelectedColour;

            MessageBox.Show("Colour Copied!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BTNPaste_Click(object sender, EventArgs e)
        {

            if (clrCopyColour != Color.Empty)
            {
                clrSelectedColour = clrCopyColour;
                this.RBSelectedColour.Visible = true;
                this.RBSelectedColour.Refresh();
                this.LBLSelectedColour.Text = clrSelectedColour.Name;
                PreviewChanges(false);
            }

        }

        private void PANMain_Paint(object sender, PaintEventArgs e)
        {
            //320 x 590 top 15 draw border around embedded form
            e.Graphics.DrawRectangle(penTemp, 12, 100, 295, 80);
   }
    }
}