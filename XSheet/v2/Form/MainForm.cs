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
using XSheet.v2.CfgBean;
using System.IO;

namespace XSheet.v2.Form
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        XSheetUser user { get; set; }
        private SplashScreenManager splashManager;
        private List<TileBarItem> items;
        private List<AppCfg> apps;
        public MainForm()
        {
            try
            {
                user = new XSheetUser(System.Environment.UserDomainName, System.Environment.UserName, System.Environment.MachineName, System.Environment.OSVersion.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return;
            }
            
            InitializeComponent();
            this.splashManager = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::XSheet.v2.Form.process), true, true);
            this.splashManager.ShowWaitForm();
            init();
            this.splashManager.CloseWaitForm();
        }

        private void init()
        {
            if (!user.logAsDesigner)
            {
                btn_Designer.Visible = false;
            }
            lbl_user.Text = user.getFullUserName();
            this.items = user.getItemList();
            tbGroup.Items.Clear();
            foreach (TileBarItem item in items)
            {
                item.ItemClick += Item_ItemClick;
                tbGroup.Items.Add(item);
            }
        }

        private void Item_ItemClick(object sender, TileItemEventArgs e)
        {
            if (e.Item.Tag != null || File.Exists(e.Item.Tag.ToString()))
            {
                splashManager.ShowWaitForm();
                XSheetMain xshet = new XSheetMain(e.Item.Tag.ToString());
                this.Hide();
                xshet.Owner = this;
                splashManager.CloseWaitForm();
            }

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