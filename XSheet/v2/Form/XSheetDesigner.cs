using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.IO;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraEditors;
using System.Drawing;
using DevExpress.Utils.Menu;
using XSheet.v2.Control;

namespace XSheet.v2.Form
{
    public partial class XSheetDesigner : RibbonForm//,Observer
    {
        private Dictionary<String, SimpleButton> buttons { get; set; }
        private XSheetControl control { get; set; }
        private Dictionary<String, LabelControl> labels { get; set; }
        /*public XCfgData cfgData { get; set; }
        public XApp app { get; set; }
        public XSheet.Data.XSheet currentSheet{ get; set;}
        public XNamed currentXNamed { get; set; }
        
        public string executeState { get; set; }
        public string appstatu { get; set; }
        private CommandExecuter executer;
        private AreasCollection oldSelected { get; set; }*/
        public XSheetDesigner()
        {
            InitializeComponent();
            InitSkinGallery();
            buttons = new Dictionary<string, SimpleButton>();
            /*提取按钮，将按钮与自定义事件名称绑定*/
            buttons.Add("Btn_Search".ToUpper(), dbtn_Search);
            buttons.Add("Btn_Execute".ToUpper(), dbtn_Execute);
            buttons.Add("Btn_Delete".ToUpper(), btn_Delete);
            buttons.Add("Btn_Edit".ToUpper(),dbtn_Edit);
            buttons.Add("Btn_New".ToUpper(), dbtn_New);

            labels = new Dictionary<String, LabelControl>();
            labels.Add("lbl_App", this.lbl_App);
            labels.Add("lbl_User", this.lbl_User);
            //CELLCHANGE
            /*executer = new CommandExecuter();
            executer.Attach(this);
            executeState = "OK";
            //加载文档，后续根据不同设置配置，待修改TODO
            spreadsheetMain.Document.LoadDocument("\\\\ichart3d\\XSheetModel\\XSheet模板设计.xlsx");*/
            this.control = new XSheetControl(spreadsheetMain, buttons,labels);

        }
        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        private void spreadsheetMain_SelectionChanged(object sender, EventArgs e)
        {
            control.spreadsheetMain_SelectionChanged(sender, e);
        }
        //
        /*public void setSelectedNamed()
        {
            AreasCollection areas = spreadsheetMain.Selection.Areas;
            XSheet.Data.XSheet opSheet = app.getSheets()[spreadsheetMain.ActiveWorksheet.Name];
            if (currentXNamed != null && RangeUtil.isInRange(areas, currentXNamed.getRange()) < 0)
            {
                this.currentXNamed = null;
            }
            foreach (var dicname in opSheet.names)
            {
                XNamed xname = dicname.Value;
                int i = xname.isInRange(areas);
                if (i >= 0)
                {
                    this.currentXNamed = xname;
                    this.currentXNamed.setSelectIndex(spreadsheetMain.Selection.TopRowIndex, spreadsheetMain.Selection.LeftColumnIndex);
                }
            }
        }

        private void ChangeButtonsStatu()
        {
            foreach (var btndic in buttons)
            {
                btndic.Value.Enabled = false;
            }
            if (currentXNamed != null && executeState == "OK" && appstatu == "OK")
            {
                foreach (var commandDic in currentXNamed.commands)
                {
                    if (buttons.ContainsKey(commandDic.Key.ToUpper()))
                    {
                        if (currentXNamed.dt != null || commandDic.Key == "BTN_SEARCH")
                        {
                            setBtnStatuOn(commandDic.Key);
                        }
                        
                    }
                }
            }
        }*/
        private void spreadsheetMain_DocumentLoaded(object sender, EventArgs e)
        {
            control.spreadsheetMain_DocumentLoaded(sender, e);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Search");
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Download");
        }
        
        private void btn_New_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_New");
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Edit");
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Delete");
        }

        private void btn_Exe_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Execute");
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Submit");
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
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                popupSpread.ShowPopup(p);
                IList<Table>tables =spreadsheetMain.ActiveWorksheet.Tables.GetTables(spreadsheetMain.ActiveCell);
                //todo
               
            }
            else
            {
                control.spreadsheetMain_MouseUp(sender, e);
            }
            //oldSelected = null;
        }

        /*public void init()
        {
            cfgData = new XCfgData(spreadsheetMain.Document.Worksheets["Config"]);
            this.appstatu = cfgData.flag;
            if (appstatu != "OK")
            {
                MessageBox.Show("配置文件加载出错，请确认配置文件");
                return;
            }
            app = new XApp(spreadsheetMain.Document, cfgData);
            this.appstatu = app.flag;
            if (appstatu != "OK")
            {
                MessageBox.Show("XSheet初始化出错，请确认配置文件");
                return;
            }
            this.lbl_App.Text = "APP:" + app.appName;
            this.lbl_User.Text = "当前用户:" + app.user;
            try
            {
                currentSheet = app.getSheets()[spreadsheetMain.ActiveWorksheet.Name];
            }
            catch (Exception)
            {
                MessageBox.Show(spreadsheetMain.ActiveWorksheet.Name +" 未配置在Config文件Sheet列表中");
            }
        }

        private void setBtnStatuOn(String eventType)
        {
            buttons[eventType.ToUpper()].Enabled = true;
            /*this.btn_Submit = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Download = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Search = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Exe = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Delete = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Edit = new DevExpress.XtraEditors.SimpleButton();
            this.btn_New = new DevExpress.XtraEditors.SimpleButton();
        }

        public void UpdateCmdStatu(String statu)
        {
            this.executeState = statu;
            ChangeButtonsStatu();
        }
        */
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

        private void dropDownButton1_Click(object sender, EventArgs e)
        {
            alertcontrolMain.Show(this, "test", "HelloWorld");
            dbtn_Execute.DropDownControl = CreateDXPopupMenu();
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
            MessageBox.Show(ts_multiSelect.Checked.ToString());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}