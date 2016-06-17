using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.Data
{
    public class XSheet
    {
        public String sheetName { get; set; }
        public Worksheet sheet { get; set; }
        public Dictionary<String, XNamedRange> names { get; set; }
        public Dictionary<String,Table> tables { get; set; }
        
        public XSheet()
        {
            names = new Dictionary<string, XNamedRange>();
            tables = new Dictionary<string, Table>();
        }

        public void initTables()
        {
            try
            {
                if (sheet.Tables.Count>0)
                {
                    foreach (Table table in sheet.Tables)
                    {
                        tables.Add(table.Name, table);
                    }
                }
                
            }
            catch (Exception e)
            {

                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            
        }
    }
}
