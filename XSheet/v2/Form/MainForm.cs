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
using XSheet.v2.Privilege;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars.Navigation;

namespace XSheet.v2.Form
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        XSheetUser user { get; set; }
        private SplashScreenManager splashManager;
        private List<TileBarItem> items;
        public MainForm(XSheetUser user)
        {
            this.user = user;
            items = new List<TileBarItem>();
            InitializeComponent();
            this.splashManager = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::XSheet.v2.Form.process), true, true);
            init();
        }

        private void init()
        {
            if (!user.logAsDesigner)
            {
                btn_Designer.Visible = false;
            }
            lbl_user.Text = user.getFullUserName();
        }

        private void btn_Designer_Click(object sender, EventArgs e)
        {
            splashManager.ShowWaitForm();
            XSheetDesigner designer = new XSheetDesigner(user);
            this.Hide();
            designer.Owner = this;
            
            splashManager.CloseWaitForm();
            
            

        }
    }
}