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

namespace XSheet.v2.Form
{
    public partial class XDashBoard : DevExpress.XtraEditors.XtraForm
    {
        public XDashBoard()
        {
            InitializeComponent();
            dash_viewMain.Dashboard = new DevExpress.DashboardCommon.Dashboard();
            dash_viewMain.Dashboard.LoadFromXml("\\\\ichart3d\\XSheetModel\\Dashboard/Inventory.xml");
            this.WindowState = FormWindowState.Maximized;
            this.Show();
        }
    }
}