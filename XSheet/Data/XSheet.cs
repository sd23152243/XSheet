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
        public Dictionary<String, XNamed> names { get; set; }
        public XApp app { get; set; }
        public XSheet()
        {
            names = new Dictionary<string, XNamed>();
        }
    }
}
