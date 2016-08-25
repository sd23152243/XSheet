using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.v2.CfgBean;
using XSheet.Data;
using XSheet.Util;
using XSheet.v2.Data.XSheetRange;
using XSheet.v2.Data;
using XSheet.v2.Privilege;

namespace XSheet.v2.Control
{
    class XSheetControl: Observer
    {
        public XCfgData cfgData { get; set; }//读取的配置项文件
        public XApp app { get; set; }//当前app内容
        public XRSheet currentSheet { get; set; }//当前显示sheet
        public XRange currentXRange { get; set; }//当前选中Range
        public Dictionary<String, SimpleButton> buttons { get; set; }//按钮统一管理
        public string executeState { get; set; }//app执行状态，是否处于空闲等
        public string appstatu { get; set; }//app当前状态,是否存在配置错误等
        public string appmode { get; set; }//app当前模式，单选还是多选
        private CommandExecuter executer;//通用命令调度器
        private AreasCollection currSelected { get; set; }//记录当前选择的区域
        private AreasCollection oldSelected { get; set; }//记录上次选择的区域
        private SpreadsheetControl spreadsheetMain { get; set; }//spreadsheet主控件
        private Dictionary<String, LabelControl> labels { get; set; }//各标签页
        private String curUserPrivilege { get; set; }
        private XSheetUser user { get; set; }
        //构造函数
        public XSheetControl(SpreadsheetControl spreadsheetMain, Dictionary<String, SimpleButton> buttons, Dictionary<String, LabelControl> labels)
        {
            controlInit(spreadsheetMain, buttons, labels, "\\\\ichart3d\\XSheetModel\\XSheetTemplate20160822.xlsx");
        }
        //带参数的初始化
        public void controlInit(SpreadsheetControl spreadsheetMain, Dictionary<String, SimpleButton> buttons, Dictionary<String, LabelControl> labels, String path)
        {
            this.buttons = buttons;
            this.labels = labels;
            this.spreadsheetMain = spreadsheetMain;
            this.user = new XSheetUser(System.Environment.UserDomainName, System.Environment.UserName, System.Environment.MachineName, System.Environment.OSVersion.ToString());
            //CELLCHANGE
            executer = new CommandExecuter(user);
            executer.Attach(this);
            executeState = "OK";
            appstatu = "OK";
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
        //文档加载事件，用于初始化
        public void spreadsheetMain_DocumentLoaded(object sender, EventArgs e)
        {
            init();
            if (appstatu.ToUpper() == "OK")
            {
                executer.excueteCmd(currentSheet, SysEvent.Sheet_Init);
        }
            spreadsheetMain.Document.Calculate();
        }
        //通用事件响应，用于调用各类事件
        public void EventCall(SysEvent e)
        {
            if (appstatu.ToUpper() != "OK")
            {
                return;
            }
            executer.excueteCmd(currentXRange, e);
        }
        //单元格内容变更事件响应
        public void spreadsheetMain_CellValueChanged(object sender, SpreadsheetCellEventArgs e)
        {
            spreadsheetMain.Document.Calculate();
            setSelectedNamed();
            if (e.OldValue != e.Value)
            {
                executer.excueteCmd(currentXRange, SysEvent.Cell_Change);
            }
        }
        //鼠标按键抬起事件响应，用于释放简单资源
        public void spreadsheetMain_MouseUp(object sender, MouseEventArgs e)
        {            
            if (currentXRange != null && currentXRange.isSelectable() == true)
            {
                
            }
        }
        //控制器初始化，读取Config，初始化整个APP
        public void init()
        {
            Worksheet cfgsheet = null;
            try
            {
                cfgsheet = spreadsheetMain.Document.Worksheets["Config"];
            }
            catch (Exception)
            {

                MessageBox.Show("当前App中缺少Config配置页，请确认文件未损坏货配置页名称正确");
            }
            
            cfgData = new XCfgData(cfgsheet);
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
            labels["lbl_App"].Text = "APP:" + app.getFullAppName();
            labels["lbl_User"].Text = "当前用户:" + this.user.getFullUserName();
            try
            {
                currentSheet = app.getRSheetByName(spreadsheetMain.ActiveWorksheet.Name);
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
                currentSheet = app.getRSheetByName(e.NewActiveSheetName);
                executer.excueteCmd(currentSheet, SysEvent.Sheet_Change);
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
                    currentSheet = app.getRSheetByName(name);
                    executer.excueteCmd(currentSheet, SysEvent.Sheet_Init);
                    app.setSheetVisiable(name);
                }
                catch (Exception)
                {
                    spreadsheetMain.Document.Worksheets[oldName].Cells[0, 0].Select();
                    spreadsheetMain.Document.Worksheets.ActiveWorksheet = spreadsheetMain.Document.Worksheets[oldName];
                }
            }
        }
        //键盘事件响应
        public void spreadsheetMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentXRange != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    executer.excueteCmd(currentXRange, SysEvent.Key_Enter);
                }
            }
            
            
        }
        //测试按钮响应，正式环境隐藏
        public void btn_Config_Click(object sender, EventArgs e)
        {
            if (this.currentXRange!= null)
            {
                Range range = currentXRange.getRange();
            }
        }
        //关闭事件前判断
        public void Closed()
        {
            //TODO 后续加入判断，当前是否存在未执行完任务
        }
        //切换当前单选/多选状态
        public void ChangeMuiltSingle(String sts)
        {
            //TODO
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
            if (currentXRange != null)
            {
                executer.excueteCmd(currentXRange, SysEvent.Select_Change);
            }
            //oldSelected = spreadsheetMain.Selection.Areas;
        }
        //根据当前选择点，判断选择区域
        public void setSelectedNamed()
        {
            AreasCollection areas = spreadsheetMain.Selection.Areas;
            XRSheet opSheet = app.getRSheetByName(spreadsheetMain.ActiveWorksheet.Name);
            if (currentXRange != null && RangeUtil.isInRange(areas, currentXRange.getRange()) < 0)
            {
                this.currentXRange = null;
            }
            //遍历当前Sheet全部命名区域，依次判断是否在区域范围内
            foreach (var dicname in opSheet.ranges)
            {
                XRange xname = dicname.Value;
                int i = xname.isInRange(areas);
                if (i >= 0)
                {
                    this.currentXRange = xname;
                    //当选择点为命名区域时，将当前坐标写入单元格
                    //this.currentXRange.onMouseDown();
                    break;//如果判断到第一个区域，将该区域存储为currentXRange，退出循环判断
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
            /*if (currentXRange != null && executeState == "OK" && appstatu == "OK")
            {
                foreach (var commandDic in currentXRange.commands)
                {
                    String strevent = commandDic.Key.ToString();
                    if (buttons.ContainsKey(strevent))
                    {
                        if (currentXRange.isDataValied() || commandDic.Key == "BTN_SEARCH")
                        {
                            setBtnStatuOn(commandDic.Key);
                        }
                    }
                }
            }*/
        }
    }
}
