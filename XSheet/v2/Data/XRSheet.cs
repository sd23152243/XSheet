using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using XSheet.Data;
using XSheet.v2.Data.XSheetRange;

namespace XSheet.v2.Data
{
    public class XRSheet
    {
        public String sheetName { get; set; }
        public Worksheet sheet { get; set; }
        public Dictionary<String, XRange> ranges { get; set; }
        public XApp app { get; set; }
        public String hideflag { get; set; }
        public XRSheet(Worksheet sheet)
        {
            ranges = new Dictionary<string, XRange>();
            this.sheet = sheet;
            this.sheetName = sheet.Name;
        }

        public void doLoadCommand(CommandExecuter cmdexe)
        {
            foreach (var range in ranges.Values)
            {
                cmdexe.excueteCmd(range, "Sheet_Change");
            }
        }

        /*public void PopUp(String ActionName,DataTable dt,Dictionary<int,int> selectedRows)
        {
            String path = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            path = path + "\\tmp";
            FileStream file = new FileStream(path , FileMode.OpenOrCreate);
            file.Close();
            sheet.Workbook.SaveDocument(path);
            XSheetPopUp popUp = new XSheetPopUp(path, app.cfg,ActionName,dt, selectedRows,this.sheetName);
            popUp.Show();
            popUp.BringToFront();
        }*/
        
        public void Hidden()
        {
            this.sheet.VisibilityType = WorksheetVisibilityType.Hidden;
        }

        private void VeryHidden()
        {
            this.sheet.VisibilityType = WorksheetVisibilityType.VeryHidden;
        }

        public void setVisable(String Name)
        {
            if (this.hideflag =="1" && this.sheet.Name!= Name)
            {
                VeryHidden();
            }
            else
            {
                this.sheet.VisibilityType = WorksheetVisibilityType.Visible;
            }
        }

    }
}
