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
            foreach (var datarow in selectedRows)
            {
                if (datarow.Value % 2 == 1)
                {
                    selectedRowsList.Add(datarow.Key);
                }
            }
            init();
        }
        private void init()
        {
            
            String sheetName = xAction.dRange.Name + "_PopUp";
            String rangeName = xAction.dRange.Name + "_PopUp";
            DataTable ndt = dt.Clone();
            foreach (int rowNum in selectedRowsList)
            {
                DataRow row = ndt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row[i] = dt.Rows[rowNum][i];
                        
                }
                ndt.Rows.Add(row);
            }
            foreach (Worksheet sheet in spreadsheetControl1.Document.Worksheets)
            {
                if (sheet.Name == sheetName)
                {
                    this.name = app.names[rangeName];
                    this.name.fill(ndt);
                    //this.name = XNamedFactory.getXNamed(app.)
                    //xsheetdic.Value.names[xAction.dRange + "_PopUp"].fill(dt);
                }
                else
                {
                    sheet.VisibilityType = WorksheetVisibilityType.VeryHidden;
                }
            }
            foreach (var xsheetdic in app.getSheets())
            {
                
            }
        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            String Sql = xAction.dRange.getSqlStatement();
            DbDataAdapter da =  DBUtil.getDbDataAdapter(xAction.dRange.cfg.serverName, Sql);
            //da.MissingMappingAction = MissingMappingAction.Passthrough;
            //da.MissingSchemaAction = MissingSchemaAction.
            //DataTable ndt = SheetUtil.ExportRangeStopOnEmptyRow(this.name.getRange(), true, true);
            Range range = this.name.getRange();
            int i = 0;
            int topRowIndex = range.TopRowIndex;
            int leftColumnIndex = range.LeftColumnIndex;
            Worksheet sheet = range.Worksheet;
            while (sheet[i+topRowIndex+1,leftColumnIndex].Value.ToString().Length>0)
            {
                
                int rowindex = i + topRowIndex+1;
                DataRow row = null;
                if (sheet[rowindex,leftColumnIndex].Tag != null)
                {
                    int rowListNO = int.Parse(sheet[rowindex, leftColumnIndex].Tag.ToString());
                    row = dt.Rows[selectedRowsList[i]];
                }
                else
                {
                    row = dt.NewRow();
                    dt.Rows.Add(row);
                }
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    int colindex = j + leftColumnIndex;
                    if (row[j].ToString() != sheet[rowindex, colindex].Value.ToString())
                    {

                        Type t = row[j].GetType();

                        if (t.Name == "Decimal")
                        {
                            Decimal num = Convert.ToDecimal(sheet[rowindex, colindex].Value.ToString());
                            row[j] = (object)num;
                        }
                        else
                        {
                            row[j] =sheet[rowindex, colindex].Value.ToString();
                        }
                    }
                }
                i++;
            }
            
            try
            {
                da.Update(dt);
            }
            catch (SqlException ee)
            {
                MessageBox.Show(ee.Message);
            }

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}