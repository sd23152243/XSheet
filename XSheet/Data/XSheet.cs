using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XSheet.Data
{
    public class XSheet
    {
        public String sheetName { get; set; }
        public Worksheet sheet { get; set; }
        public Dictionary<String, XNamed> names { get; set; }
        public XApp app { get; set; }
        public String hideflag { get; set; }
        public XSheet()
        {
            names = new Dictionary<string, XNamed>();
        }

        public void doLoadCommand(CommandExecuter cmdexe)
        {
            foreach (var namedic in names)
            {
                cmdexe.excueteCmd(namedic.Value, "Sheet_Change", null);
            }
        }

        public void PopUp(String ActionName,DataTable dt,Dictionary<int,int> selectedRows)
        {
            String path = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            path = path + "\\tmp";
            FileStream file = new FileStream(path , FileMode.OpenOrCreate);
            file.Close();
            sheet.Workbook.SaveDocument(path);
            XSheetPopUp popUp = new XSheetPopUp(path, app.cfg,ActionName,dt, selectedRows);
            popUp.Show();
            popUp.BringToFront();
        }
        
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
