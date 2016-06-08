using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.IO;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using XSheet.Action;
using XSheet.Data;

namespace XSheet
{
    public partial class XSheetDesigner : RibbonForm
    {
        public XSheetDesigner()
        {
            InitializeComponent();
            InitSkinGallery();
            IList<Range> Ranges = spreadsheetMain.ActiveCell.Dependents;

        }
        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        private void btn_Exe_Click(object sender, EventArgs e)
        {
            String path = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            FileStream file = new FileStream(path+"/tmp", FileMode.OpenOrCreate);
            file.Close();
            this.spreadsheetMain.SaveDocument(path+"/tmp");
        }

        private void spreadsheetMain_SelectionChanged(object sender, EventArgs e)
        {
            SpreadsheetControl tmpsheet = (SpreadsheetControl)sender;
            IList<Range> Ranges = tmpsheet.ActiveCell.Dependents;
            DefinedNameCollection names = tmpsheet.ActiveWorksheet.DefinedNames;
            String tt = "";
            foreach (Range range in Ranges)
            {
                foreach (DefinedName name in names)
                {
                    if (name.Range == range)
                    {
                        tt += name.Name;
                    }
                }
            }
            tmpsheet.ActiveCell.Value = tt;
            
        }

        private void spreadsheetMain_DocumentLoaded(object sender, EventArgs e)
        {

        }
    }
}