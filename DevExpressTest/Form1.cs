using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevExpressTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void imageComboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(imageComboBoxEdit.SelectedItem.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
