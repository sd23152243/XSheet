using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.Util;

namespace tools
{
    public partial class Form_Tools : Form
    {
        public Form_Tools()
        {
            InitializeComponent();
        }

        private void txt_Input_EditValueChanged(object sender, EventArgs e)
        {
            String input = txt_Input.Text;
            txt_Des.Text = DESUtil.EncryptString(input, DESUtil.GenerateKey());
        }

        private void txt_Des_EditValueChanged(object sender, EventArgs e)
        {
            String input = txt_Des.Text;
            txt_Input.Text = DESUtil.DecryptString(input, DESUtil.GenerateKey());
        }
    }
}
