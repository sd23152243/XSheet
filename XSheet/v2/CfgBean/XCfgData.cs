using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.Util;

namespace XSheet.v2.CfgBean
{
    public class XCfgData
    {
        Dictionary<String,Table> cfgtables { get; set; }
        public AppCfg app { get; set; }
        public List<SheetCfg> sheets { get; set; }
        public List<DataCfg> datas { get; set; }
        public List<CommandCfg> commands { get; set; }
        public List<ActionCfg> actions { get; set; }
        public List<DashBoardCfg> dashboards { get; set; }
        public String flag;
        public XCfgData(List<Worksheet> cfgsheets)
        {

            cfgtables = new Dictionary<string, Table>();
            flag = "OK";
            for (int i = 0; i < cfgsheets.Count; i++)
            {
                foreach (Table table in cfgsheets[i].Tables)
                {
                    cfgtables.Add(table.Name.ToUpper(), table);
                }
            }
            
            InitCfg();
        }
        //配置文件初始化读取
        public void InitCfg()
        {
            initApp();
            initData();
            initSheet();
            initCommand();
            initAction();
            initDashBoard();
       }

        private void initDashBoard()
        {
            Table table = getCfgTable("CFG_DASHBOARD");
            if (table == null)
            {
                return;
            }
            this.dashboards = CfgDataReader.readDashboard(table);
        }

        //读取app初始化信息
        private void initApp()
        {
            Table table = getCfgTable("CFG_APP");
            if (table == null)
            {
                flag = "NG";
                return;
            }
            this.app = CfgDataReader.readApp(table);
        }
        //读取range初始化信息
        private void initData()
        {
            Table table = getCfgTable("CFG_DATA");
            if (table == null)
            {
                flag = "NG";
                return;
            }
            datas = CfgDataReader.readData(table);
        }
        //读取Sheet初始化信息
        private void initSheet()
        {
            Table table = getCfgTable("CFG_SHEET");
            if (table == null)
            {
                flag = "NG";
                return;
            }
            sheets = CfgDataReader.readSheet(table);
        }
        //读取绑定初始化信息
        private void initCommand()
        {
            Table table = getCfgTable("CFG_COMMAND");
            if (table == null)
            {
                flag = "NG";
                return;
            }
            commands = CfgDataReader.readCommand(table);
        }
        //读取action初始化信息
        private void initAction()
        {
            Table table = getCfgTable("CFG_ACTION");
            if (table == null)
            {
                flag = "NG";
                return;
            }
            actions = CfgDataReader.readAction(table);

        }
        //读取配置文件中的配置表格
        private Table getCfgTable(String tableName)
        {
            Table table = null;
            try
            {
                table = cfgtables[tableName];
                if (table.Range == null)
                {
                    MessageBox.Show("名称为" + tableName + "的EXCEL Table区域配置异常，请确认配置");
                    return null;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("未找到名称为" + tableName + "的EXCEL Table区域，请确认配置");

            }
            return table;
        }

    }
}
