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
using XSheet.CfgData;
using XSheet.Util;
using XSheet.test;

namespace XSheet
{
    public partial class XSheetDesigner : RibbonForm
    {
        public XSheetCfgData cfgData { get; set; }
        public XApp app { get; set; }
        public XSheetDesigner()
        {
            InitializeComponent();
            InitSkinGallery();
            spreadsheetMain.Document.LoadDocument("\\\\ichart3d\\XSheetModel\\XSheet模板设计.xlsx");

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
            AreasCollection areas = tmpsheet.Selection.Areas;
            
            DefinedNameCollection names = tmpsheet.ActiveWorksheet.DefinedNames;
            DefinedNameCollection names2 = tmpsheet.Document.DefinedNames;
            names.Concat<DefinedName>(names2);
            String tt = "";
            foreach (Range range in areas)
            {
                foreach (DefinedName name in names)
                {
                    if (name.Range == range)
                    {
                        tt += name.Name;
                    }
                }
                foreach (DefinedName name in names2)
                {
                    if (name.Range == range)
                    {
                        tt += name.Name;
                    }
                }
            }
            //tmpsheet.ActiveCell.Value = tt;
            
        }

        private void spreadsheetMain_DocumentLoaded(object sender, EventArgs e)
        {
            
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            //spreadsheetMain.ActiveWorksheet.Tables[0].Range = spreadsheetMain.ActiveWorksheet.Range["F27:G28"];
            XMLTest test = new XMLTest(spreadsheetMain);
        }
    }
}