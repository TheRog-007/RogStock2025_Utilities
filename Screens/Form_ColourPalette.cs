using RogStock2025_Utilities.Modules;
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
    public partial class frmColourPalette : Form
    {
        bool blnStart = true;
        Pen penColour = new Pen(Color.Black);

        //*custom subs/funcs********
        private void CustomRadioButtonClick(object sender, EventArgs e)
        {
            /*
              Created 15/07/2025 By Roger Williams

              radiobutton click event
            
            */

            int intTag = 0;

            intTag = Convert.ToInt16(((RadioButton)(sender)).Tag);
            frmThemeMaintenance.clrSelectedColour = Color.FromName(clsView_Utilities.aryColours[intTag]);
            this.Hide();
        }

        private void CustomRadioButtonPaint(object sender, PaintEventArgs e)
        {
            /*
              Created 15/07/2025 By Roger Williams

              radiobutton paint event for drawing the colour palette
            
              each radiobutton has a number and that is used to draw its colour

            */
            Rectangle rectTemp = e.ClipRectangle;
            int intTag = 0;

            intTag = Convert.ToInt16(((RadioButton)(sender)).Tag);

            e.Graphics.DrawRectangle(penColour, rectTemp);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromName(Modules.clsView_Utilities.aryColours[intTag])), rectTemp);

        }

        public void CreateColourRadioButtons()
        {
            /*
              Created 15/07/2025 By Roger Williams

              creates radiobuttons for drawing the colour palette
              with label for colour name

              NOTE: uses radiobutton tag to signifiy which Modules.clsView_Utilities.aryColours element
                    it represents
            
            */

            Label LBLTemp = null;
            RadioButton RBTemp = null;  //x=18 y=28 first control pos
            int intNum = 0;
            int intX = 18;
            int intY = 28;
            //114 colours!
            //rb height = 13 width = 14!
            for (intNum = 0; intNum != 114; intNum++)
            {
                RBTemp = new RadioButton();
                LBLTemp = new Label();

                RBTemp.Parent = this.GRPColours;
                LBLTemp.Parent = this.GRPColours;

                //bottom of groupbox?
                if (intY >= 390)
                {
                    intY = 28;
                    //move to next "column"
                    intX = intX + 150;
                }

                //set controls pos
                RBTemp.Top = intY;
                RBTemp.Left = intX;
                RBTemp.Width = 14;
                RBTemp.Height = 13;
                LBLTemp.Top = intY;
                LBLTemp.Left = intX + 20;
                //use slightly smaller font
                LBLTemp.Font = new Font("Microsoft Sans Serif", 9);

                //assign custom events
                RBTemp.Paint += CustomRadioButtonPaint;
                RBTemp.Click += CustomRadioButtonClick;
                //move to next "row" position
                intY = intY + 20;

                RBTemp.Tag = intNum;  //so paint knows what colour to use
                //set label caption to colour name
                LBLTemp.Text = Modules.clsView_Utilities.aryColours[intNum];
                LBLTemp.Width = 130;

                //stop re-running this
                blnStart = false;
            }
        }

        //*******form events***


        public frmColourPalette()
        {
            InitializeComponent();
        }

        private void frmColourPalette_Load(object sender, EventArgs e)
        {
            //stop running more than once as form is hidden when "closed"
            if (blnStart)
            {
                CreateColourRadioButtons();
            }
        }
    }
}
