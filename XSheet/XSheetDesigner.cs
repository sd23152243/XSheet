using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.IO;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using XSheet.Data.Action;
using XSheet.Data;
using XSheet.CfgData;
using XSheet.Util;
using XSheet.test;
using DevExpress.XtraEditors;

namespace XSheet
{
    public partial class XSheetDesigner : RibbonForm,Observer
    {
        public XCfgData cfgData { get; set; }
        public XApp app { get; set; }
        public XSheet.Data.XSheet currentSheet{ get; set;}
        public XNamed currentXNamed { get; set; }
        public Dictionary<String, SimpleButton> buttons { get; set; }
        public string executeState { get; set; }
        public string appstatu { get; set; }
        private CommandExecuter executer;
        private AreasCollection oldSelected { get; set; }
        public XSheetDesigner()
        {
            this.appstatu = "OK";
            InitializeComponent();
            InitSkinGallery();
            buttons = new Dictionary<string, SimpleButton>();
            /*提取按钮，将按钮与自定义事件名称绑定*/
            buttons.Add("Btn_Submit".ToUpper(), btn_Submit);
            buttons.Add("Btn_Download".ToUpper(), btn_Download);
            buttons.Add("Btn_Search".ToUpper(), btn_Search);
            buttons.Add("Btn_Execute".ToUpper(), btn_Exe);
            buttons.Add("Btn_Delete".ToUpper(), btn_Delete);
            buttons.Add("Btn_Edit".ToUpper(), btn_Edit);
            buttons.Add("Btn_New".ToUpper(), btn_New);
            //CELLCHANGE
            executer = new CommandExecuter();
            executer.Attach(this);
            executeState = "OK";
            /*加载文档，后续根据不同设置配置，待修改TODO*/
            spreadsheetMain.Document.LoadDocument("\\\\ichart3d\\XSheetModel\\XSheet模板设计.xlsx");

        }
        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        

        private void spreadsheetMain_SelectionChanged(object sender, EventArgs e)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            setSelectedNamed();
            ChangeButtonsStatu();
            if (currentXNamed != null)
            {
                
                AreasCollection areas = spreadsheetMain.Selection.Areas;
                Range srange = areas[areas.Count - 1];
                for (int row=0;row<srange.RowCount;row++)
                {
                    int rn = currentXNamed.getRowIndexByRange(srange[row,0]);
                    if (rn >= 0)
                    {
                        currentXNamed.selectRow(rn);
                    }
                }
                if (oldSelected != null)
                {
                    Range orange = oldSelected[oldSelected.Count - 1];
                    for (int row = 0; row < orange.RowCount; row++)
                    {
                        int rn = currentXNamed.getRowIndexByRange(orange[row, 0]);
                        if (rn >= 0)
                        {
                            currentXNamed.UnselectRow(rn);
                        }
                    }
                }
            }
            oldSelected = spreadsheetMain.Selection.Areas;


        }
        //
        private void setSelectedNamed()
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
        }
        private void spreadsheetMain_DocumentLoaded(object sender, EventArgs e)
        {
            init();
            if (appstatu.ToUpper() == "OK")
            {
                if (spreadsheetMain.Document.Worksheets.ActiveWorksheet.Name != cfgData.app.defaultSheetName && cfgData.app.defaultSheetName != null)
                {
                    spreadsheetMain.Document.Worksheets.ActiveWorksheet = spreadsheetMain.Document.Worksheets[cfgData.app.defaultSheetName];
                }
                else
                {
                    currentSheet.doLoadCommand(executer);
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            executer.excueteCmd(currentXNamed, "Btn_Search", null);
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            executer.excueteCmd(currentXNamed, "Btn_Download", null);
        }
        
        private void btn_New_Click(object sender, EventArgs e)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            executer.excueteCmd(currentXNamed, "Btn_New", null);
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            executer.excueteCmd(currentXNamed, "Btn_Edit", null);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            executer.excueteCmd(currentXNamed, "Btn_Delete", null);
        }

        private void btn_Exe_Click(object sender, EventArgs e)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            executer.excueteCmd(currentXNamed, "Btn_Execute", null);
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            executer.excueteCmd(currentXNamed, "Btn_Submit", null);
        }

        private void spreadsheetMain_CellValueChanged(object sender, SpreadsheetCellEventArgs e)
        {
            spreadsheetMain.Document.Calculate();
            if (e.OldValue != e.Value && currentXNamed != null)
            {
                executer.excueteCmd(currentXNamed, "Cell_Change", null);
            }
        }

        private void spreadsheetMain_MouseUp(object sender, MouseEventArgs e)
        {
            oldSelected = null;
        }

        public void init()
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
            this.btn_New = new DevExpress.XtraEditors.SimpleButton();*/
        }

        public void UpdateCmdStatu(String statu)
        {
            this.executeState = statu;
            ChangeButtonsStatu();
        }

        private void spreadsheetMain_ActiveSheetChanged(object sender, ActiveSheetChangedEventArgs e)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            try
            {
                currentSheet = app.getSheets()[e.NewActiveSheetName];
                currentSheet.doLoadCommand(executer);
                app.setSheetVisiable(e.NewActiveSheetName);
            }
            catch (Exception)
            {
                spreadsheetMain.Document.Worksheets[e.OldActiveSheetName].Cells[0, 0].Select();
                spreadsheetMain.Document.Worksheets.ActiveWorksheet = spreadsheetMain.Document.Worksheets[e.OldActiveSheetName];
            }
        }

        private void spreadsheetMain_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
        {
            if(e.IsExternal== false)
            {
                String oldName = spreadsheetMain.ActiveWorksheet.Name;
                String name = e.TargetRange.Worksheet.Name;
                app.setSheetVisiable(name);
                try
                {
                    currentSheet = app.getSheets()[name];
                    currentSheet.doLoadCommand(executer);
                    app.setSheetVisiable(name);
                }
                catch (Exception)
                {
                    spreadsheetMain.Document.Worksheets[oldName].Cells[0, 0].Select();
                    spreadsheetMain.Document.Worksheets.ActiveWorksheet = spreadsheetMain.Document.Worksheets[oldName];
                }


            }
        }

        private void spreadsheetMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(e.KeyChar.ToString());
        }
    }
}