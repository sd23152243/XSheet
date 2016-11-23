using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using XSheet.v2.Control;
using XSheet.v2.Data;
using System.IO;
using DevExpress.XtraBars;
using XSheet.Util;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using DevExpress.Utils.Menu;

namespace XSheet.v2.Form
{
    public partial class XSheetMain : DevExpress.XtraEditors.XtraForm
    {
        private Dictionary<String, SimpleButton> buttons { get; set; }
        private XSheetControl control { get; set; }
        private Dictionary<String, Label> labels { get; set; }
        Dictionary<String, PopupMenu> menus = new Dictionary<string, PopupMenu>();
        public XSheetMain()
        {
            this.Hide();
            InitializeComponent();
            DateTime date = DateTime.Now;
            //StreamWriter sw = new StreamWriter(date.ToLongDateString() + ".txt", true);
            //TextWriter temp = Console.Out;
            
            setDefaultParam();
            //Console.SetOut(sw);
            //Console.WriteLine("beforeDsp:" + date.ToString());
            //sw.Close();
            //Console.SetOut(temp);
            this.control = new XSheetControl(spreadsheetMain, buttons, labels, menus, rightClickBarManager, this, alertcontrolMain);
            timer100ms.Start();
            //date = DateTime.Now;
            //sw = new StreamWriter(date.ToLongDateString() + ".txt", true);
            //Console.SetOut(sw);
            //Console.WriteLine("end:" + date.ToString());
            //sw.Close();
            //Console.SetOut(temp);
        }

        public XSheetMain(String path)
        {
            InitializeComponent();
            setDefaultParam();

            this.control = new XSheetControl(spreadsheetMain, buttons, labels, path, menus, rightClickBarManager, this, alertcontrolMain);
            timer100ms.Start();
        }

        private void setDefaultParam()
        {
            buttons = new Dictionary<string, SimpleButton>();
            /*提取按钮，将按钮与自定义事件名称绑定*/
            buttons.Add("Btn_Search".ToUpper(), dbtn_Search);
            buttons.Add("Btn_Execute".ToUpper(), dbtn_Execute);
            buttons.Add("Btn_Delete".ToUpper(), btn_Delete);
            buttons.Add("Btn_Edit".ToUpper(), dbtn_Edit);
            buttons.Add("Btn_New".ToUpper(), dbtn_New);
            buttons.Add("Btn_Cancel".ToUpper(), btn_Cancel);
            buttons.Add("Btn_Save".ToUpper(), btn_Save);
            labels = new Dictionary<String, Label>();
            labels.Add("lbl_AppID", this.lbl_Appid);
            labels.Add("lbl_User", this.lbl_User);
            labels.Add("lbl_AppName", this.lbl_AppName);
            labels.Add("lbl_Time", this.lbl_Time);
            labels.Add("lbl_Version", this.lbl_Version);
            //CELLCHANGE

            /*executer = new CommandExecuter();
            executer.Attach(this);
            executeState = "OK";
            //加载文档，后续根据不同设置配置，待修改TODO
            spreadsheetMain.Document.LoadDocument("\\\\ichart3d\\XSheetModel\\XSheet模板设计.xlsx");*/

            menus.Add("Normal", popupSpread);
            menus.Add("CfgData", popupDataCfg);
        }

        private void spreadsheetMain_SelectionChanged(object sender, EventArgs e)
        {

            control.spreadsheetMain_SelectionChanged(sender, e);
        }

        private void spreadsheetMain_DocumentLoaded(object sender, EventArgs e)
        {
            control.spreadsheetMain_DocumentLoaded(sender, e);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            control.EventCall(SysEvent.Btn_Search, 0);
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            control.EventCall(SysEvent.Btn_New, 0);
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            control.EventCall(SysEvent.Btn_Edit, 0);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            control.EventCall(SysEvent.Btn_Delete, 0);
        }

        private void btn_Exe_Click(object sender, EventArgs e)
        {
            control.EventCall(SysEvent.Btn_Exe, 0);
        }

        private void spreadsheetMain_CellValueChanged(object sender, SpreadsheetCellEventArgs e)
        {
            control.spreadsheetMain_CellValueChanged(sender, e);
            /*spreadsheetMain.Document.Calculate();
            setSelectedNamed();
            if (e.OldValue != e.Value)
            {
                executer.excueteCmd(currentXNamed, "Cell_Change");
            }*/
        }

        private void spreadsheetMain_MouseUp(object sender, MouseEventArgs e)
        {
            control.spreadsheetMain_MouseUp(sender, e);
            //oldSelected = null;
        }

        private void spreadsheetMain_ActiveSheetChanged(object sender, ActiveSheetChangedEventArgs e)
        {
            control.spreadsheetMain_ActiveSheetChanged(sender, e);
        }

        private void spreadsheetMain_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
        {
            control.spreadsheetMain_HyperlinkClick(sender, e);
        }

        private void btn_Config_Click(object sender, EventArgs e)
        {
            control.btn_Config_Click(sender, e);
        }

        private void XSheetDesigner_FormClosed(object sender, FormClosedEventArgs e)
        {
            control.Closed();
            //MessageBox.Show("Close");
        }

        private void spreadsheetMain_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private DXPopupMenu CreateDXPopupMenu()
        {
            DXPopupMenu menu = new DXPopupMenu();
            menu.Items.Add(new DXMenuItem("Item", OnItemClick));
            menu.Items.Add(new DXMenuCheckItem("CheckItem", false, null, OnItemClick));
            DXSubMenuItem subMenu = new DXSubMenuItem("SubMenu");
            subMenu.Items.Add(new DXMenuItem("SubItem", OnItemClick));
            menu.Items.Add(subMenu);
            return menu;
        }
        private void OnItemClick(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            XtraMessageBox.Show(item.Caption);
        }

        private void ts_multiSelect_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            //alertcontrolMain.Show(this,"notice",ts_multiSelect.Checked.ToString());
            control.ChangeMuiltSingle(ts_multiSelect.Checked);
        }

        private void btn_DesignSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            control.EventCall(SysEvent.Event_Search, 0);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            control.EventCall(SysEvent.Btn_Cancel, 0);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            control.EventCall(SysEvent.Btn_Save, 0);
        }

        private void dbtn_Execute_Click(object sender, EventArgs e)
        {
            control.EventCall(SysEvent.Btn_Exe, 0);
        }

        private void timer100ms_Tick(object sender, EventArgs e)
        {
            this.lbl_Time.Text = DateTime.Now.ToLongTimeString();
        }

        private void btn_Flash_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btn_Encode_ItemClick(object sender, ItemClickEventArgs e)
        {
            String str = spreadsheetMain.ActiveCell.DisplayText;
            String key = DESUtil.GenerateKey();
            String[] strs = new string[] {
            "Provider=IBMDA400;Data Source=172.31.96.210;User Id=ITSDTS;Password = STD008;",
            "Data Source=srf-sql;User Id=MARS_E;Password = rs@996t!ty",
            "Data Source=ichart3d;User Id=MARS_E;Password = rs@996t!ty",
            "Data Source=ichart3d;User Id=MARS_E;Password = rs@996t!ty"};
            str = DESUtil.EncryptString(str, key);
            Console.WriteLine(str);
            foreach (string item in strs)
            {
                Console.WriteLine("//////////////////////");
                Console.WriteLine(item);
                Console.WriteLine(" ");
                String tmp = DESUtil.EncryptString(item, key);
                Console.WriteLine(tmp);
                Console.WriteLine(" ");
                Console.WriteLine(DESUtil.DecryptString(tmp, key));

            }
        }

        private void btn_Exel_Click(object sender, EventArgs e)
        {
            //spreadsheetMain.Document.SaveDocument("tmp.xlsx");
            //System.Diagnostics.Process.Start("tmp.xlsx");
            //XDashBoard dashborad = new XDashBoard();
            //dashborad.Show();
            /**/
            DictionaryForm select =new DictionaryForm(this.spreadsheetMain.Document);
            //select.Show();
            select.Show();
        }
    }
}