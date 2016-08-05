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
        //构造函数
        public XSheetControl(SpreadsheetControl spreadsheetMain, Dictionary<String, SimpleButton> buttons, Dictionary<String, LabelControl> labels)
        {
            controlInit(spreadsheetMain, buttons, labels, "\\\\ichart3d\\XSheetModel\\在库管理系统.xlsx");
        }
        //带参数的初始化
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
                spreadsheetMain.Dispose();
            }
            
        }
        //带参数的构造函数
        public XSheetControl(SpreadsheetControl spreadsheetMain, Dictionary<String, SimpleButton> buttons, Dictionary<String, LabelControl> labels,String path)
        {
            controlInit(spreadsheetMain, buttons, labels, path);
        }
        //响应界面选择点变化事件
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
                executer.excueteCmd(currentXNamed, "Select_Change");
            }
            //oldSelected = spreadsheetMain.Selection.Areas;
        }
        //根据当前选择点，判断选择区域
        public void setSelectedNamed()
        {
            AreasCollection areas = spreadsheetMain.Selection.Areas;
            XSheet.Data.XSheet opSheet = app.getSheets()[spreadsheetMain.ActiveWorksheet.Name];
            if (currentXNamed != null && RangeUtil.isInRange(areas, currentXNamed.getRange()) < 0)
            {
                this.currentXNamed = null;
            }
            //遍历当前Sheet全部命名区域，依次判断是否在区域范围内
            foreach (var dicname in opSheet.names)
            {
                XNamed xname = dicname.Value;
                int i = xname.isInRange(areas);
                if (i >= 0)
                {
                    this.currentXNamed = xname;
                    //当选择点为命名区域时，将当前坐标写入单元格
                    this.currentXNamed.setSelectIndex(spreadsheetMain.Selection.TopRowIndex, spreadsheetMain.Selection.LeftColumnIndex);
                    break;//如果判断到第一个区域，将该区域存储为currentXNamed，退出循环判断
                }
            }
        }
        //函数，根据当前各类情况，改变各个按钮的状态
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
        //文档加载事件，用于初始化
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
        //通用事件响应，用于调用各类事件
        public void EventCall(String Event)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            executer.excueteCmd(currentXNamed, Event);
        }
        //单元格内容变更事件响应
        public void spreadsheetMain_CellValueChanged(object sender, SpreadsheetCellEventArgs e)
        {
            spreadsheetMain.Document.Calculate();
            setSelectedNamed();
            if (e.OldValue != e.Value)
            {
                executer.excueteCmd(currentXNamed, "Cell_Change");
            }
        }
        //鼠标按键抬起事件响应，用于释放简单资源
        public void spreadsheetMain_MouseUp(object sender, MouseEventArgs e)
        {            
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
                /*if (oldSelected != null)
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
                }*/
                this.currentXNamed.drawSelectedRows();//在鼠标松开后，同一绘制选择
            }
        }
        //控制器初始化，读取Config，初始化整个APP
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
        //私有方法，将传入按钮设为可用
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
        //封装方法，当状态变化时，调用按钮状态改变
        public void UpdateCmdStatu(String statu)
        {
            this.executeState = statu;
            ChangeButtonsStatu();
        }
        //Sheet激活时触发，用于响应Sheet切换事件
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
        //超链接事件响应
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
        //键盘事件响应，未完成
        public void spreadsheetMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentXNamed != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    executer.excueteCmd(currentXNamed, "Enter_Press");
                }
            }
            
            
        }
        //测试按钮响应，正式环境隐藏
        public void btn_Config_Click(object sender, EventArgs e)
        {
            
        }
        //关闭事件响应
        public void XSheetDesigner_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show("Close");
        }
    }
}
