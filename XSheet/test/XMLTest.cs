using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.test
{
    class XMLTest
    {
        private SpreadsheetControl sheetcontrol;

        private XMLTest()
        {

        }
        public XMLTest(SpreadsheetControl sheetcontrol)

        {
            IWorkbook workbook = sheetcontrol.Document;
            Worksheet sheet = workbook.Worksheets["Config"];
            Worksheet sheet2 = workbook.Worksheets["SheetTest"];
            Range range = sheet.Range["CFG_Range"];
            String str = range[0, 3].Value.ToString();
            sheet2["B3"].Value = str.ToUpper();




            /*IWorkbook workbook = sheetcontrol.Document;
            Worksheet sheet = workbook.Worksheets["Config"];
            Worksheet sheet2 = workbook.Worksheets["SheetTest"];
            String str = sheet["f14"].Value.ToString();
            sheet2["B3"].Value = str.ToUpper();*/

            /*IWorkbook workbook1 = sheetcontrol.Document;
            WorksheetCollection sheets = workbook.Worksheets;
            Worksheet sheet2 = sheets["xx"];*/

            /* this.sheetcontrol = sheetcontrol;
             CPU cpu = new CPU();
             cpu.setSpeed(2200);

             HardDisk disk = new HardDisk();
             disk.setAmount(200);

             PC pc = new PC();
             pc.setCPU(cpu);
             pc.setHardDisk(disk);
             pc.show();*/

            /*Person xml = new Person("xumeiling", "girl", 23,165,'A');*/
            /* xml.name = "xumeiling";
             xml.age = 23;
             xml.height = 165;
             xml.score = 'A';
             xml.sex = "girl";*/
            /* xml.Introduce(); */

            /* Person pgm = new Person("panguanmign", "girl", 23, 165,'A');*/
            /*pgm.name = "panguanmign";
            pgm.age = 30;
            pgm.height = 170;*/
            /* pgm.Introduce();
            pgm.Introduce("songqirong");
            pgm.Introduce();*/

        }
    }
}

