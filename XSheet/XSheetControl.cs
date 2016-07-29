using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.CfgData;
using XSheet.Data;
using XSheet.Util;

namespace XSheet
{
    class XSheetControl: Observer
    {
        public XCfgData cfgData { get; set; }
        public XApp app { get; set; }
        public XSheet.Data.XSheet currentSheet { get; set; }
        public XNamed currentXNamed { get; set; }
        public Dictionary<String, SimpleButton> buttons { get; set; }
        public string executeState { get; set; }
        public string appstatu { get; set; }
        private CommandExecuter executer;
        private AreasCollection oldSelected { get; set; }
        private SpreadsheetControl spreadsheetMain { get; set; }
        private Dictionary<String, LabelControl> labels { get; set; }
        public XSheetControl(SpreadsheetControl spreadsheetMain, Dictionary<String, SimpleButton> buttons, Dictionary<String, LabelControl> labels)
        {
            controlInit(spreadsheetMain, buttons, labels, "\\\\ichart3d\\XSheetModel\\在库管理系统.xlsx");
        }

        public void controlInit(SpreadsheetControl spreadsheetMain, Dictionary<String, SimpleButton> buttons, Dictionary<String, LabelControl> labels, String path)
        {
            this.buttons = buttons;
            this.labels = labels;
            this.spreadsheetMain = spreadsheetMain;
            //CELLCHANGE
            executer = new CommandExecuter();
            executer.Attach(this);
            executeState = "OK";
            /*加载文档，后续根据不同设置配置，待修改TODO*/
            try
            {
                spreadsheetMain.Document.LoadDocument(path);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

        public XSheetControl(SpreadsheetControl spreadsheetMain, Dictionary<String, SimpleButton> buttons, Dictionary<String, LabelControl> labels,String path)
        {
            controlInit(spreadsheetMain, buttons, labels, path);
        }
        public void spreadsheetMain_SelectionChanged(object sender, EventArgs e)
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
                for (int row = 0; row < srange.RowCount; row++)
                {
                    int rn = currentXNamed.getRowIndexByRange(srange[row, 0]);
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
                executer.excueteCmd(currentXNamed, "Select_Change");
            }
            oldSelected = spreadsheetMain.Selection.Areas;
        }
        //
        public void setSelectedNamed()
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
        public void spreadsheetMain_DocumentLoaded(object sender, EventArgs e)
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
            spreadsheetMain.Document.Calculate();
        }

        public void EventCall(String Event)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            executer.excueteCmd(currentXNamed, Event);
        }

        public void spreadsheetMain_CellValueChanged(object sender, SpreadsheetCellEventArgs e)
        {
            spreadsheetMain.Document.Calculate();
            setSelectedNamed();
            if (e.OldValue != e.Value)
            {
                executer.excueteCmd(currentXNamed, "Cell_Change");
            }
        }

        public void spreadsheetMain_MouseUp(object sender, MouseEventArgs e)
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
            labels["lbl_App"].Text = "APP:" + app.appName;
            labels["lbl_User"].Text = "当前用户:" + app.user;
            try
            {
                currentSheet = app.getSheets()[spreadsheetMain.ActiveWorksheet.Name];
            }
            catch (Exception)
            {
                MessageBox.Show(spreadsheetMain.ActiveWorksheet.Name + " 未配置在Config文件Sheet列表中");
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

        public void spreadsheetMain_ActiveSheetChanged(object sender, ActiveSheetChangedEventArgs e)
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

        public void spreadsheetMain_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
        {
            if (e.IsExternal == false)
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

        public void spreadsheetMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(e.ToString());
        }

        public void btn_Config_Click(object sender, EventArgs e)
        {

        }

        public void XSheetDesigner_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show("Close");
        }
    }
}
