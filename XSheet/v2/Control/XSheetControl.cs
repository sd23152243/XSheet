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
using DevExpress.XtraBars;
using System.Drawing;
using DevExpress.XtraBars.Alerter;
using XSheet.v2.Util;
using DevExpress.Utils.Menu;

namespace XSheet.v2.Control
{
    class XSheetControl : Observer
    {
        public XCfgData cfgData { get; set; }//读取的配置项文件
        public XApp app { get; set; }//当前app内容
        public XRSheet currentSheet { get; set; }//当前显示sheet
        public XRange currentXRange { get; set; }//当前选中Range
        public Dictionary<String, SimpleButton> buttons { get; set; }//按钮统一管理
        public string executeState { get; set; }//app执行状态，是否处于空闲等
        private CommandExecuter executer;//通用命令调度器
        private AreasCollection currSelected { get; set; }//记录当前选择的区域
        private AreasCollection oldSelected { get; set; }//记录上次选择的区域
        private SpreadsheetControl spreadsheetMain { get; set; }//spreadsheet主控件
        private Dictionary<String, LabelControl> labels { get; set; }//各标签页
        private String curUserPrivilege { get; set; }
        private XSheetUser user { get; set; }
        private Dictionary<String, PopupMenu> menus { get; set; }
        private BarManager rightClickBarManager { get; set; }
        private Boolean muiltiFlag = false;
        private XtraForm form;
        private AlertControl alert;
        //构造函数
        public XSheetControl(SpreadsheetControl spreadsheetMain, Dictionary<String, SimpleButton> buttons, Dictionary<String, LabelControl> labels,Dictionary<String,PopupMenu> menus,BarManager barmanager, XtraForm form,AlertControl alert)
        {
            controlInit(spreadsheetMain, buttons, labels, "\\\\ichart3d\\XSheetModel\\XSheetTemplate20160822.xlsx",menus,barmanager,form,alert);
        }
        //带参数的初始化
        public void controlInit(SpreadsheetControl spreadsheetMain, Dictionary<String, SimpleButton> buttons, Dictionary<String, LabelControl> labels, String path, Dictionary<String, PopupMenu> menus, BarManager barmanager,XtraForm form, AlertControl alert)
        {
            this.buttons = buttons;
            this.labels = labels;
            this.spreadsheetMain = spreadsheetMain;
            this.menus = menus;
            this.rightClickBarManager = barmanager;
            AlertUtil.setAlert(alert, form);
            this.user = new XSheetUser(System.Environment.UserDomainName, System.Environment.UserName, System.Environment.MachineName, System.Environment.OSVersion.ToString());
            this.form = form;
            this.alert = alert;
            //CELLCHANGE
            executer = new CommandExecuter(user);
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
        public XSheetControl(SpreadsheetControl spreadsheetMain, Dictionary<String, SimpleButton> buttons, Dictionary<String, LabelControl> labels, String path, Dictionary<String, PopupMenu> menus, BarManager barmanager, XtraForm form, AlertControl alert)
        {
            controlInit(spreadsheetMain, buttons, labels, path, menus, barmanager,form,alert);
        }
        //文档加载事件，用于初始化
        public void spreadsheetMain_DocumentLoaded(object sender, EventArgs e)
        {
            init();

            if ((int)app.statu > 0)
            {
                executer.executeCmd(currentSheet, SysEvent.Sheet_Init);
            }
            spreadsheetMain.Document.Calculate();
        }
        //通用事件响应，用于调用各类事件
        public void EventCall(SysEvent e,int i)
        {
            if (e == SysEvent.Btn_New)
            {
                if (currentXRange.getDataTable() == null)
                {
                    executer.executeCmd(currentXRange, SysEvent.Btn_Search);
                }
                ChangeToStatu(SysStatu.Insert);
                currentXRange.newData(1);
            }
            else  if(e== SysEvent.Btn_Edit)
            {
                ChangeToStatu(SysStatu.Update);
            }
            else if(e == SysEvent.Btn_Delete)
            {
                ChangeToStatu(SysStatu.Delete);
            }
            else if (e == SysEvent.Btn_Search)
            {
                executer.executeCmd(currentXRange, e, i);
            }
            else if(e == SysEvent.Btn_Save)
            {
                switch (app.statu)
                {
                    case SysStatu.Designer:
                        break;
                    case SysStatu.Single:
                        break;
                    case SysStatu.Muilti:
                        break;
                    case SysStatu.Update:
                        executer.executeCmd(currentXRange, SysEvent.Btn_Edit, i);
                        ChangeToStatu(muiltiFlag ? SysStatu.Muilti : SysStatu.Single);
                        break;
                    case SysStatu.Delete:
                        executer.executeCmd(currentXRange, SysEvent.Btn_Delete, i);
                        ChangeToStatu(muiltiFlag ? SysStatu.Muilti : SysStatu.Single);
                        break;
                    case SysStatu.Insert:
                        executer.executeCmd(currentXRange, SysEvent.Btn_New, i);
                        ChangeToStatu(muiltiFlag ? SysStatu.Muilti : SysStatu.Single);
                        break;
                    case SysStatu.Error:
                        break;
                    case SysStatu.AppError:
                        break;
                    case SysStatu.RangeError:
                        break;
                    case SysStatu.SheetError:
                        break;
                    case SysStatu.CommandError:
                        break;
                    case SysStatu.ActionError:
                        break;
                    default:
                        break;
                }
            }
            else if (app.statu > 0)
            {
                if (e == SysEvent.Btn_Cancel)
                {
                    ChangeToStatu(muiltiFlag ? SysStatu.Muilti : SysStatu.Single);
                    executer.executeCmd(currentXRange, SysEvent.Btn_Search, 0);
                }
                else if (e == SysEvent.Btn_Save)
                {
                    executer.executeCmd(currentXRange, e);
                    ChangeToStatu(muiltiFlag ? SysStatu.Muilti : SysStatu.Single);
                }
                
                
            }
            return;
        }
        //单元格内容变更事件响应
        public void spreadsheetMain_CellValueChanged(object sender, SpreadsheetCellEventArgs e)
        {
            if (app.statu > 0)
            {
                spreadsheetMain.Document.Calculate();
                setSelectedNamed();
                if (currentXRange != null && e.OldValue != e.Value)
                {
                    if (currentXRange.getType() != "Table")
                    {
                        executer.executeCmd(currentXRange, SysEvent.Cell_Change);
                    }
                    else
                    {
                        
                        if (app.statu == SysStatu.Insert )//INSERT 状态只允许新增区域修改数据
                        {
                            int dcount = currentXRange.getDataTable().Rows.Count;
                            int selectRowCount = spreadsheetMain.SelectedCell.TopRowIndex - currentXRange.getRange().TopRowIndex;
                            if (selectRowCount < dcount)
                            {
                                spreadsheetMain.SelectedCell.Value = e.OldValue;
                            }
                        }
                        else if(app.statu != SysStatu.Update)//非UPDATE状态不允许修改数据
                        {
                            spreadsheetMain.SelectedCell.Value = e.OldValue;
                        }
                        else//Update修改数据,修改后修改区域变色
                        {
                            spreadsheetMain.SelectedCell.FillColor = Color.Yellow;
                        }
                    }
                }
            }
        }
        //鼠标按键抬起事件响应，用于释放简单资源
        public void spreadsheetMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                IList<Table> tables = spreadsheetMain.ActiveWorksheet.Tables.GetTables(spreadsheetMain.ActiveCell);
                //todo
            }
            else if (currentXRange != null && currentXRange.isSelectable() == true)
            {
                if ((int)app.statu==3 || (int)app.statu == 4)
                {
                    currentXRange.onSelect(true);
                }
                else if((int)app.statu != 5)
                {
                    currentXRange.onSelect(muiltiFlag);
                }
                
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
            app = new XApp(spreadsheetMain.Document, cfgData);
            labels["lbl_App"].Text = "APP:" + app.getFullAppName();
            labels["lbl_User"].Text = "当前用户:" + this.user.getFullUserName();
            if (app.statu == SysStatu.Designer)
            {
                MessageBox.Show("进入设计者模式！");
                return;
            }
            RefreshCurrentSheet();
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
            RefreshCurrentSheet();
            if (app.statu == SysStatu.Designer)
            {
                return;
            }
            try
            {
                currentSheet = app.getRSheetByName(e.NewActiveSheetName);
                executer.executeCmd(currentSheet, SysEvent.Sheet_Change);
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
                    executer.executeCmd(currentSheet, SysEvent.Sheet_Init);
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
            RefreshCurrentSheet();
            if (currentXRange != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    executer.executeCmd(currentXRange, SysEvent.Key_Enter);
                }
            }
        }
        //测试按钮响应，正式环境隐藏
        public void btn_Config_Click(object sender, EventArgs e)
        {
            if (this.currentXRange != null)
            {
                Range range = currentXRange.getRange();
            }
        }
        //关闭事件前判断
        public void Closed()
        {
            //TODO 后续加入判断，当前是否存在未执行完任务
        }
        //切换当前单选/多选状态,b 为true 多选， false 单选
        public void ChangeMuiltSingle(Boolean b)
        {
            muiltiFlag = b;
            if ((int)app.statu > 0 && (int)app.statu<=2)
            {
                ChangeToStatu(b ? SysStatu.Muilti : SysStatu.Single);
            }
        }
        //响应界面选择点变化事件
        public void spreadsheetMain_SelectionChanged(object sender, EventArgs e)
        {
            if ((int)app.statu > 0)
            {
                setSelectedNamed();
                ChangeButtonsStatu();
                if (currentXRange != null)
                {
                    executer.executeCmd(currentXRange, SysEvent.Select_Change);
                }
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
            rightClickBarManager.SetPopupContextMenu(spreadsheetMain, null);
            if (currentSheet.sheetName == "Config")
            {
                foreach (Table item in currentSheet.sheet.Tables)
                {
                    if (item.Name == "CFG_DATA")
                    {
                        if (RangeUtil.isInRange(areas, item.DataRange) >=0)
                        {
                            rightClickBarManager.SetPopupContextMenu(spreadsheetMain, menus["CfgData"]);
                        }
                        break;
                    }
                }
                
            }
            else
            {
                //遍历当前Sheet全部命名区域，依次判断是否在区域范围内
                foreach (var dicname in opSheet.ranges)
                {
                    XRange xname = dicname.Value;
                    int i = xname.isInRange(areas);
                    if (i >= 0)
                    {
                        this.currentXRange = xname;
                        rightClickBarManager.SetPopupContextMenu(spreadsheetMain, menus["Normal"]);
                        //当选择点为命名区域时，将当前坐标写入单元格
                        //this.currentXRange.onMouseDown();
                        break;//如果判断到第一个区域，将该区域存储为currentXRange，退出循环判断
                    }
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
            ((DropDownButton)buttons["BTN_SEARCH"]).DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide;
            ((DropDownButton)buttons["BTN_EDIT"]).DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide;
            ((DropDownButton)buttons["BTN_EXECUTE"]).DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide;
            if (currentXRange != null && executeState == "OK")
            {
                if ((int)app.statu > 0 && (int)app.statu <=2)
                {
                    List<String> valiedList = currentXRange.getValiedLFunList();
                    valiedList = filtFunlistByPrivilege(valiedList);
                    foreach (String item in valiedList)
                    {
                        switch (item)
                        {
                            case "R":
                                buttons["BTN_SEARCH"].Enabled = true;
                                Dictionary<int,XCommand > cmds = currentXRange.getCommandByEvent(SysEvent.Btn_Search);
                                if (cmds.Count>1)
                                {
                                    ((DropDownButton)buttons["BTN_SEARCH"]).DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Default;
                                    ((DropDownButton)buttons["BTN_SEARCH"]).DropDownControl = CreateDXPopupMenu(cmds);
                                }
                                break;
                            case "C":
                                buttons["BTN_NEW"].Enabled = true;
                                break;
                            case "U":
                                buttons["BTN_EDIT"].Enabled = true;
                                break;
                            case "D":
                                buttons["BTN_DELETE"].Enabled = true;
                                break;
                            case "P":
                                buttons["BTN_EXECUTE"].Enabled = true;
                                break;
                            case "RO":
                                ((DropDownButton)buttons["BTN_SEARCH"]).DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Default;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if ((int)app.statu>2)
                {
                    if (app.statu == SysStatu.Insert)
                    {
                        buttons["BTN_NEW"].Enabled = true;
                    }
                    buttons["BTN_CANCEL"].Enabled = true;
                    buttons["BTN_SAVE"].Enabled = true;

                }                
            }
        }

        private IDXDropDownControl CreateDXPopupMenu(Dictionary<int, XCommand> cmds)
        {
            DXPopupMenu menu = new DXPopupMenu();
            foreach (var item in cmds)
            {
                if (item.Key==0)
                {
                    continue;
                }
                DXMenuItem ditem = new DXMenuItem();
                ditem.Caption = item.Key.ToString()+":"+item.Value.CommandName;
                ditem.Click += OnItemClick;
                ditem.Tag =item.Value.e;
                menu.Items.Add(ditem);
            }
            
            return menu;
        }

        private void OnItemClick(object sender, EventArgs e)
        {
            DXMenuItem item = (DXMenuItem)sender;
            int i = int.Parse(item.Caption.Split(':')[0]);
            EventCall((SysEvent)item.Tag, i);
        }

        //刷新当前Sheet
        private void RefreshCurrentSheet()
        {
            this.currentSheet = app.getRSheetByName(spreadsheetMain.ActiveWorksheet.Name);
            curUserPrivilege = user.getPrivilege(currentSheet);
        }
        //获取用户权限
        public string GetUserPrivilege()
        {
            return user.getPrivilege(currentSheet);
        }

        public List<string> filtFunlistByPrivilege(List<String> funcList)
        {
            String privilege = GetUserPrivilege();
            for (int i = funcList.Count-1; i >=0; i--)
            {
                if (!privilege.Contains(funcList[i][0]))
                {
                    funcList.RemoveAt(i);
                }
            }
            return funcList;
        }

        //状态变化
        private void ChangeToStatu(SysStatu newstatu)
        {
            app.statu = newstatu;
            labels["lbl_User"].Text = app.statu.ToString();
            AlertUtil.Show( "状态变更", "状态变更为" + newstatu);
            ChangeButtonsStatu();
        }
    }
}