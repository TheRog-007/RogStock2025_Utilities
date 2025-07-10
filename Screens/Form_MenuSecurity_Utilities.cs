using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025_Utilities.Screens
{
    public partial class frmMenuSecurity_Utilities : Form
    {
        public frmMenuSecurity_Utilities()
        {
            InitializeComponent();
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {

            Modules.clsView_Utilities.RemoveFromOpenForms(this.ParentForm, this.Text);
            this.Close();
        }

    }
}
