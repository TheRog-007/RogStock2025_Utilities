using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


//using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Drawing2D;


namespace RogStock2025_Utilities.Screens
{
    public partial class frmMain_Utilities : Form
    {
        /*
             
                   Created 24/06//2025 By Roger Williams

                   Copied the main menu screen to be used here for the external utilities program
               
             
                   Created 13/02/2025 By Roger Williams

                   Main menu screen!

         */

        //used by "mainmenu" button
        Brush bruShowHide1 = new SolidBrush(Color.White);
        Brush bruShowHide2 = new SolidBrush(Color.GreenYellow);
        Brush bruShowHide3 = new SolidBrush(Color.Yellow);
        Brush bruShowHide4 = new SolidBrush(Color.Cyan);
        Point pntShowHide = new Point(10, 10);
        Font fntShowHide = new Font("Segoe UI", 11, FontStyle.Bold);
        int intColourSwap = 0;

        ////used by "mainmenu"
        bool blnShowMenu = false;
        Brush bruMenu = new SolidBrush(Color.White);
        Point pntMenu = new Point(10, 4);
        Font fntMenu = new Font("Segoe UI", 10, FontStyle.Bold);


        public frmMain_Utilities()
        {
            InitializeComponent();
            //apply colour "theme" to menu
            this.MNUMainMenu.Renderer = new Modules.clsView_Utilities.clsMenuColourScheme();
        }


        //*custom sub/funcs**
        private void Custom_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rctTemp = e.ClipRectangle;

            if (sender is ToolStripMenuItem)
            {
                e.Graphics.FillRectangle(Brushes.Olive, rctTemp);
                e.Graphics.DrawString(((ToolStripMenuItem)sender).Text, fntMenu, bruMenu, pntMenu);
            }
            if (sender is MenuStrip)
            {
                e.Graphics.FillRectangle(Brushes.Olive, rctTemp);
            }
        }


        private void MenuItemClicked(object sender, EventArgs e)
        {
            /*
                   Created 01/07/2025 By Roger Williams

                   Global handler for menu item click event
                   
            */

            if (((ToolStripMenuItem)sender).Name != "MNUMenu" && ((ToolStripMenuItem)sender).Name != "MNULogins" && ((ToolStripMenuItem)sender).Name != "MNUExit")
            {
                ShowMenuItem(((ToolStripMenuItem)sender).Text);
            }
        }

        //****form events*******
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams


                   
            */
            // Modules.clsData_Utilities.DeleteCurrentLoginRecord();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            /*
                   Created 13/02/2025 By Roger Williams

                   Show login screen

            */
            frmLogin_Utilities frmTemp;

            //load theme
            Modules.clsView_Utilities.ReadThemeData();

            //open login screen
            frmTemp = new frmLogin_Utilities();
            frmTemp.ShowDialog();

            //init custom sql error data if not found exit
            if (!Modules.clsData_Utilities.InitCustomErrorhandler(Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\Errorlist.res"))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
                return;
            }

            //set main menu controls
            this.PANMenu.Height = 0;
            this.PANMenu.Top = 0;
            //            this.PANSections.Height = 0;
            this.PANOptions.Height = 0;
            this.BTNShowHide.Top = 0;
            this.MNUMenu.Paint += this.Custom_Paint;
            this.MNULogins.Paint += this.Custom_Paint;
            //  this.MNUOperations.Paint += this.Custom_Paint;
            this.MNUMainMenu.Paint += this.Custom_Paint;
            this.TMRMenu.Enabled = true;
            //menu item custom click handler
            this.MNUGroups.Click += MenuItemClicked;
            this.MNULoginMaintenance.Click += MenuItemClicked;
            this.MNUMenu.Click += MenuItemClicked;
            this.MNUMenuItems.Click += MenuItemClicked;
            this.MNUMenuSecurity.Click += MenuItemClicked;
            this.MNUSections.Click += MenuItemClicked;
            this.MNUThemeMaintenance.Click += MenuItemClicked;
            //apply system theme
            Modules.clsView_Utilities.SetTheme(this);
        }

        private void ShowMenuItem(string strWhat)
        {
            /*
             Created 13/03/2025 By Roger Williams

             Opens menu item!
             Partially hide the menu strip

            */

            Modules.clsView_Utilities.OpenForm(strWhat);
            blnShowMenu = false;
        }

        //menu click events
        private void MNUSections_Click(object sender, EventArgs e)
        {
            /*
              Created 24/06/2025 By Roger Williams


            */

            ShowMenuItem(this.MNUSections.Text);
        }
        private void MNUGroups_Click(object sender, EventArgs e)
        {
            /*
              Created 24/06/2025 By Roger Williams


            */

            ShowMenuItem(this.MNUGroups.Text);
        }

        private void MNUMenuItems_Click(object sender, EventArgs e)
        {
            /*
              Created 24/06/2025 By Roger Williams


            */

            ShowMenuItem(this.MNUMenuItems.Text);
        }





        private void BTNShowHide_Paint(object sender, PaintEventArgs e)
        {
            Graphics graTemp = e.Graphics;
            GraphicsState state = graTemp.Save();  //save state
            Brush bruTemp = null;

            graTemp.ResetTransform();

            // Rotate.
            graTemp.RotateTransform(90);

            // Translate to desired position. Be sure to append
            // the rotation so it occurs after the rotation.
            graTemp.TranslateTransform(27, 8, MatrixOrder.Append);

            switch (intColourSwap)
            {
                case 0:
                    bruTemp = bruShowHide1;
                    break;
                case 1:
                    bruTemp = bruShowHide3;
                    break;
                case 2:
                    bruTemp = bruShowHide2;
                    break;
                case 3:
                    bruTemp = bruShowHide3;
                    break;
                case 4:
                    intColourSwap = 0;
                    bruTemp = bruShowHide1;
                    break;
            }

            if (blnShowMenu)
            {
                graTemp.DrawString("Hide", fntShowHide, bruTemp, 0, 0);
            }
            else
            {
                graTemp.DrawString("Menu", fntShowHide, bruTemp, 0, 0);
            }

            // Restore the graphics state.
            graTemp.Restore(state);
        }

        private void BTNShowHide_Click(object sender, EventArgs e)
        {
            /*
               Created 26/06/2025 By Roger Williams

               shows hides the main menu controls


            */


            int intNum = 0;

            blnShowMenu = !blnShowMenu;
            /*
      
            menu panel defaults

            x: 0
            y: 0
            h: 50
            
            sectiion panel defaults

            x: 1
            y: 60
            h: 230

            */


            //process
            if (blnShowMenu)
            {
                for (intNum = 0; intNum != 58; intNum++)
                {
                    this.PANMenu.Height++;
                    this.PANMenu.Update();
                    this.PANOptions.Height++;
                    this.PANOptions.Update();
                }
                //   for (intNum = 0; intNum != 490; intNum++)
                //   {
                //this.PANSections.Height++;
                //this.PANSections.Update();

                //if (intNum % 5 !=0)
                //{ 
                ////refresh menu buttons
                //RefreshMenuButtons();
                //}
                //  }

                //remove button border
                this.BTNShowHide.FlatAppearance.BorderSize = 0;
            }
            else
            {
                for (intNum = 0; intNum != 58; intNum++)
                {
                    this.PANMenu.Height--;
                    this.PANMenu.Update();
                    this.PANOptions.Height--;
                    this.PANOptions.Update();
                }
                //   for (intNum = 0; intNum != 490; intNum++)
                //   {
                //this.PANSections.Height--;
                //this.PANSections.Update();
                //   }

                //reset button border
                this.BTNShowHide.FlatAppearance.BorderSize = 1;
            }
        }

        private void CMBOpenScreens_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
                    Created 17/02/2025 By Roger Williams



             */
            //show form
            Modules.clsView_Utilities.OpenSelectedForm(this.CMBOpenScreens.Text);
        }

        private void MNUMainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MNUExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void TMRMenu_Tick(object sender, EventArgs e)
        {
            //swap menu show button text colour
            intColourSwap++;
            this.BTNShowHide.Refresh();
        }

        private void CMBOpenScreens_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }


        //class end
    }
}
