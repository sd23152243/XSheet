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
        private CommandExecuter executer;
        public XSheetDesigner()
        {
            InitializeComponent();
            InitSkinGallery();
            buttons = new Dictionary<string, SimpleButton>();
            buttons.Add("SUBMIT", btn_Submit);
            buttons.Add("DOWNLOAD", btn_Download);
            buttons.Add("SEARCH", btn_Search);
            buttons.Add("EXECUTE", btn_Exe);
            buttons.Add("DELETE", btn_Delete);
            buttons.Add("EDIT", btn_Edit);
            buttons.Add("NEW", btn_New);
            executer = new CommandExecuter();
            executer.Attach(this);
            executeState = "OK";
            spreadsheetMain.Document.LoadDocument("\\\\ichart3d\\XSheetModel\\XSheet模板设计.xlsx");

        }
        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        private void btn_Exe_Click(object sender, EventArgs e)
        {
            String path = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            FileStream file = new FileStream(path+"/tmp", FileMode.OpenOrCreate);
            file.Close();
            this.spreadsheetMain.SaveDocument(path+"/tmp");
        }

        private void spreadsheetMain_SelectionChanged(object sender, EventArgs e)
        {
            //spc.ActiveCell.Value = this.currentXNamed == null ? "null" : this.currentXNamed.Name;
            setSelectedNamed();
            ChangeButtonsStatu();
            //tmpsheet.ActiveCell.Value = tt;
            
        }

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
                }
            }

        }

        private void ChangeButtonsStatu()
        {
            foreach (var btndic in buttons)
            {
                btndic.Value.Enabled = false;
            }
            if (currentXNamed != null && executeState == "OK")
            {
                foreach (var commandDic in currentXNamed.commands)
                {
                    if (buttons.ContainsKey(commandDic.Key.ToUpper()))
                    {
                        setBtnStatuOn(commandDic.Key);
                    }

                }
            }
            
        }
        private void spreadsheetMain_DocumentLoaded(object sender, EventArgs e)
        {
            init();   
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            executer.excueteCmd(currentXNamed,"SEARCH", null);

        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            this.currentXNamed.doCommand("DOWNLOAD", null);
        }
        
        public void init()
        {
            cfgData = new XCfgData(spreadsheetMain.Document.Worksheets["Config"]);
            app = new XApp(spreadsheetMain.Document,cfgData);
            this.lbl_App.Text ="APP:" +app.appName;
            this.lbl_User.Text ="当前用户:"+ app.user;
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            this.currentXNamed.doCommand("NEW", null);
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
            try
            {
                currentSheet = app.getSheets()[e.NewActiveSheetName];
            }
            catch (Exception)
            {
                spreadsheetMain.Document.Worksheets[e.OldActiveSheetName].Cells[0, 0].Select();
                spreadsheetMain.Document.Worksheets.ActiveWorksheet = spreadsheetMain.Document.Worksheets[e.OldActiveSheetName];
            }
        }
    }
}