using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace XSheet
{
    public partial class XSheetPopUp : DevExpress.XtraEditors.XtraForm
    {
        public XSheetPopUp()
        {
            ///1
            InitializeComponent();
            spreadsheetControl1.LoadDocument("tmp/tmp");
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {

        }
    }
}