namespace XSheet.v2.Form
{
    partial class XSheetMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        ///
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XSheetMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_User = new DevExpress.XtraEditors.LabelControl();
            this.lbl_App = new DevExpress.XtraEditors.LabelControl();
            this.spreadsheetMain = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Submit = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Download = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Search = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Exe = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Delete = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Edit = new DevExpress.XtraEditors.SimpleButton();
            this.btn_New = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_User);
            this.panel1.Controls.Add(this.lbl_App);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1020, 30);
            this.panel1.TabIndex = 0;
            // 
            // lbl_User
            // 
            this.lbl_User.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_User.Location = new System.Drawing.Point(858, 10);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(52, 14);
            this.lbl_User.TabIndex = 1;
            this.lbl_User.Text = "当前用户:";
            // 
            // lbl_App
            // 
            this.lbl_App.Location = new System.Drawing.Point(12, 10);
            this.lbl_App.Name = "lbl_App";
            this.lbl_App.Size = new System.Drawing.Size(26, 14);
            this.lbl_App.TabIndex = 0;
            this.lbl_App.Text = "App:";
            // 
            // spreadsheetMain
            // 
            this.spreadsheetMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetMain.Location = new System.Drawing.Point(0, 30);
            this.spreadsheetMain.Name = "spreadsheetMain";
            this.spreadsheetMain.Options.Culture = new System.Globalization.CultureInfo("zh-CN");
            this.spreadsheetMain.Size = new System.Drawing.Size(1020, 581);
            this.spreadsheetMain.TabIndex = 1;
            this.spreadsheetMain.Text = "spreadsheetControl1";
            this.spreadsheetMain.DocumentLoaded += new System.EventHandler(this.spreadsheetMain_DocumentLoaded);
            this.spreadsheetMain.SelectionChanged += new System.EventHandler(this.spreadsheetMain_SelectionChanged);
            this.spreadsheetMain.ActiveSheetChanged += new DevExpress.Spreadsheet.ActiveSheetChangedEventHandler(this.spreadsheetMain_ActiveSheetChanged);
            this.spreadsheetMain.CellValueChanged += new DevExpress.XtraSpreadsheet.CellValueChangedEventHandler(this.spreadsheetMain_CellValueChanged);
            this.spreadsheetMain.HyperlinkClick += new DevExpress.XtraSpreadsheet.HyperlinkClickEventHandler(this.spreadsheetMain_HyperlinkClick);
            this.spreadsheetMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spreadsheetMain_KeyDown);
            this.spreadsheetMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.spreadsheetMain_MouseUp);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_Submit);
            this.panelControl1.Controls.Add(this.btn_Download);
            this.panelControl1.Controls.Add(this.btn_Search);
            this.panelControl1.Controls.Add(this.btn_Exe);
            this.panelControl1.Controls.Add(this.btn_Delete);
            this.panelControl1.Controls.Add(this.btn_Edit);
            this.panelControl1.Controls.Add(this.btn_New);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 611);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1020, 49);
            this.panelControl1.TabIndex = 2;
            // 
            // btn_Submit
            // 
            this.btn_Submit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Submit.Image = ((System.Drawing.Image)(resources.GetObject("btn_Submit.Image")));
            this.btn_Submit.Location = new System.Drawing.Point(916, 4);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(92, 38);
            this.btn_Submit.TabIndex = 2;
            this.btn_Submit.Text = "Submit";
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // btn_Download
            // 
            this.btn_Download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Download.Image = ((System.Drawing.Image)(resources.GetObject("btn_Download.Image")));
            this.btn_Download.Location = new System.Drawing.Point(800, 5);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(110, 38);
            this.btn_Download.TabIndex = 3;
            this.btn_Download.Text = "DownLoad";
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btn_Search.Image = ((System.Drawing.Image)(resources.GetObject("btn_Search.Image")));
            this.btn_Search.Location = new System.Drawing.Point(572, 5);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(92, 38);
            this.btn_Search.TabIndex = 4;
            this.btn_Search.Text = "Search";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_Exe
            // 
            this.btn_Exe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btn_Exe.Image = ((System.Drawing.Image)(resources.GetObject("btn_Exe.Image")));
            this.btn_Exe.Location = new System.Drawing.Point(471, 5);
            this.btn_Exe.Name = "btn_Exe";
            this.btn_Exe.Size = new System.Drawing.Size(92, 38);
            this.btn_Exe.TabIndex = 5;
            this.btn_Exe.Text = "Execute";
            this.btn_Exe.Click += new System.EventHandler(this.btn_Exe_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_Delete.Image")));
            this.btn_Delete.Location = new System.Drawing.Point(203, 5);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(87, 37);
            this.btn_Delete.TabIndex = 6;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Edit
            // 
            this.btn_Edit.Image = ((System.Drawing.Image)(resources.GetObject("btn_Edit.Image")));
            this.btn_Edit.Location = new System.Drawing.Point(110, 5);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(87, 37);
            this.btn_Edit.TabIndex = 7;
            this.btn_Edit.Text = "Edit";
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // btn_New
            // 
            this.btn_New.Image = ((System.Drawing.Image)(resources.GetObject("btn_New.Image")));
            this.btn_New.Location = new System.Drawing.Point(15, 5);
            this.btn_New.Name = "btn_New";
            this.btn_New.Size = new System.Drawing.Size(87, 37);
            this.btn_New.TabIndex = 8;
            this.btn_New.Text = "New";
            this.btn_New.Click += new System.EventHandler(this.btn_New_Click);
            // 
            // XSheetMain
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(1020, 660);
            this.Controls.Add(this.spreadsheetMain);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "XSheetMain";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lbl_User;
        private DevExpress.XtraEditors.LabelControl lbl_App;
        private DevExpress.XtraEditors.SimpleButton btn_Submit;
        private DevExpress.XtraEditors.SimpleButton btn_Download;
        private DevExpress.XtraEditors.SimpleButton btn_Search;
        private DevExpress.XtraEditors.SimpleButton btn_Exe;
        private DevExpress.XtraEditors.SimpleButton btn_Delete;
        private DevExpress.XtraEditors.SimpleButton btn_Edit;
        private DevExpress.XtraEditors.SimpleButton btn_New;
    }

}