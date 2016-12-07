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
using DevExpress.Spreadsheet;

namespace XSheet.v2.Form
{
    public partial class DictionaryForm : DevExpress.XtraEditors.XtraForm
    {
        bool saveAll = false;
        IWorkbook book;
        public DictionaryForm()
        {
            InitializeComponent();
        }
        public DictionaryForm(IWorkbook book)
        {
            InitializeComponent();
            this.book = book;
            saveFileDialog1.FileName = book.Worksheets.ActiveWorksheet.Name;
        }


        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textEdit1.EditValue = saveFileDialog1.FileName.ToString();
        }


        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            this.saveAll = !saveAll;
        }

        private void btn_SelectPath_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                export();
            }
            catch (Exception ee)
            {
                this.Show();
                MessageBox.Show(ee.ToString());
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void export()
        {
            splashManager.ShowWaitForm();
            IWorkbook newbook;
            if (saveAll)
            {
                newbook = this.book;
            }
            else
            {
                newbook = new Workbook();
                newbook.Worksheets[0].Name = book.Worksheets.ActiveWorksheet.Name;
                newbook.Worksheets[0].CopyFrom(book.Worksheets.ActiveWorksheet);

            }
            String path = textEdit1.EditValue.ToString();

            newbook.SaveDocument(path);
            System.Diagnostics.Process.Start(path);
            splashManager.CloseWaitForm();
            this.Dispose();
           
        }
    }
}