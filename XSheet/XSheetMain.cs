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
using System.ComponentModel.DataAnnotations;
using System.IO;
using DevExpress.XtraLayout.Helpers;
using DevExpress.XtraLayout;

namespace XSheet
{
    public partial class XSheetMain : DevExpress.XtraEditors.XtraForm
    {
        private Dictionary<String, SimpleButton> buttons { get; set; }
        private XSheetControl control { get; set; }
        private Dictionary<String, LabelControl> labels { get; set; }
        public XSheetMain()
        {
            InitializeComponent();
            init();
            this.control = new XSheetControl(spreadsheetMain, buttons, labels);
        }

        private void init()
        {
            buttons = new Dictionary<string, SimpleButton>();
            /*提取按钮，将按钮与自定义事件名称绑定*/
            buttons.Add("Btn_Submit".ToUpper(), btn_Submit);
            buttons.Add("Btn_Download".ToUpper(), btn_Download);
            buttons.Add("Btn_Search".ToUpper(), btn_Search);
            buttons.Add("Btn_Execute".ToUpper(), btn_Exe);
            buttons.Add("Btn_Delete".ToUpper(), btn_Delete);
            buttons.Add("Btn_Edit".ToUpper(), btn_Edit);
            buttons.Add("Btn_New".ToUpper(), btn_New);

            labels = new Dictionary<String, LabelControl>();
            labels.Add("lbl_App", this.lbl_App);
            labels.Add("lbl_User", this.lbl_User);
        }

        public XSheetMain(String path)
        {
            InitializeComponent();
            init();
            this.control = new XSheetControl(spreadsheetMain, buttons, labels,path);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Search");
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Download");
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_New");
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Edit");
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Delete");
        }

        private void btn_Exe_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Execute");
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            control.EventCall("Btn_Submit");
        }

        private void spreadsheetMain_ActiveSheetChanged(object sender, DevExpress.Spreadsheet.ActiveSheetChangedEventArgs e)
        {
            control.spreadsheetMain_ActiveSheetChanged(sender, e);
        }

        private void spreadsheetMain_DocumentLoaded(object sender, EventArgs e)
        {
            control.spreadsheetMain_DocumentLoaded(sender, e);
        }

        private void spreadsheetMain_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e)
        {
            control.spreadsheetMain_CellValueChanged(sender, e);
        }

        private void spreadsheetMain_MouseUp(object sender, MouseEventArgs e)
        {
            control.spreadsheetMain_MouseUp(sender, e);
        }

        private void spreadsheetMain_SelectionChanged(object sender, EventArgs e)
        {
            control.spreadsheetMain_SelectionChanged(sender, e);
        }

        private void spreadsheetMain_HyperlinkClick(object sender, DevExpress.XtraSpreadsheet.HyperlinkClickEventArgs e)
        {
            control.spreadsheetMain_HyperlinkClick(sender, e);
        }
    }
}