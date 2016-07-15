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
using XSheet.Data;
using DevExpress.Spreadsheet;
using XSheet.CfgData;
using System.Data.Common;
using XSheet.Util;
using System.Data.SqlClient;
using XSheet.Data.PopUpAction;

namespace XSheet
{
    public partial class XSheetPopUp : DevExpress.XtraEditors.XtraForm
    {
        private XCfgData cfg { get; set; }
        private XAction xAction;
        private DataTable dt;
        private XApp app;
        private XNamed name;
        private Dictionary<int, int> selectedRows;
        private List<int> selectedRowsList;
        private XSheetPopUp()
        {
            InitializeComponent();
            spreadsheetControl1.LoadDocument("tmp/tmp");
        }

        public XSheetPopUp(String path, XCfgData cfg,String actionName,DataTable dt,Dictionary<int, int> selectedRows)
        {
            InitializeComponent();
            spreadsheetControl1.LoadDocument(path);
            app = new XApp(spreadsheetControl1.Document, cfg);
            xAction = app.actions[actionName];
            this.cfg = cfg;
            this.dt = dt;
            this.selectedRows = selectedRows;
            selectedRowsList = new List<int>();
            if (selectedRows != null)
            {
                foreach (var datarow in selectedRows)
                {
                    if (datarow.Value % 2 == 1)
                    {
                        selectedRowsList.Add(datarow.Key);
                    }
                }

            }
            init();
        }
        private void init()
        {
            String sheetName = xAction.dRange.Name + "_PopUp";
            String rangeName = xAction.dRange.Name + "_PopUp";
            this.name = app.names[rangeName];
            DataTable ndt = null;
            if (dt != null)
            {
                ndt = dt.Clone();
                foreach (int rowNum in selectedRowsList)
                {
                    DataRow row = ndt.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        row[i] = dt.Rows[rowNum][i];

                    }
                    ndt.Rows.Add(row);
                }

            }
            
            foreach (Worksheet sheet in spreadsheetControl1.Document.Worksheets)
            {
                if (sheet.Name == sheetName)
                {
                    if (ndt != null)
                    {
                        this.name.fill(ndt);
                    }
                    sheet.VisibilityType = WorksheetVisibilityType.Visible;
                    //this.name = XNamedFactory.getXNamed(app.)
                    //xsheetdic.Value.names[xAction.dRange + "_PopUp"].fill(dt);
                }
                else
                {
                    sheet.VisibilityType = WorksheetVisibilityType.VeryHidden;
                }
            }
            spreadsheetControl1.Document.Worksheets.ActiveWorksheet = spreadsheetControl1.Document.Worksheets[sheetName];
        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            String actionName = xAction.cfg.actionType;
            //PopUpActionFactory factory = new PopUpActionFactory();
            InterfacePopUpAction action = PopUpActionFactory.getAction(actionName);
            
            action.doAction(xAction.dRange.cfg.serverName,xAction.dRange.getSqlStatement(),name,dt, selectedRowsList);
            this.Dispose(); 

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}