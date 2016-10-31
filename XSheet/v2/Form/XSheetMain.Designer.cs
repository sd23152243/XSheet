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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraSplashScreen.SplashScreenManager splashManager = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::XSheet.v2.Form.WaitingScreen), true, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XSheetMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_AppName = new System.Windows.Forms.Label();
            this.lbl_Version = new System.Windows.Forms.Label();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.lbl_User = new System.Windows.Forms.Label();
            this.lbl_Appid = new System.Windows.Forms.Label();
            this.spreadsheetMain = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.rightClickBarManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.ts_multiSelect = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Exel = new DevExpress.XtraEditors.SimpleButton();
            this.dbtn_Search = new DevExpress.XtraEditors.DropDownButton();
            this.dbtn_Edit = new DevExpress.XtraEditors.DropDownButton();
            this.dbtn_New = new DevExpress.XtraEditors.DropDownButton();
            this.dbtn_Execute = new DevExpress.XtraEditors.DropDownButton();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Delete = new DevExpress.XtraEditors.SimpleButton();
            this.popupSpread = new DevExpress.XtraBars.PopupMenu(this.components);
            this.alertcontrolMain = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.popupDataCfg = new DevExpress.XtraBars.PopupMenu(this.components);
            this.timer100ms = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightClickBarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupDataCfg)).BeginInit();
            this.SuspendLayout();
            // 
            // splashManager
            // 
            splashManager.ClosingDelay = 1000;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_AppName);
            this.panel1.Controls.Add(this.lbl_Version);
            this.panel1.Controls.Add(this.lbl_Time);
            this.panel1.Controls.Add(this.lbl_User);
            this.panel1.Controls.Add(this.lbl_Appid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(955, 30);
            this.panel1.TabIndex = 0;
            // 
            // lbl_AppName
            // 
            this.lbl_AppName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_AppName.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lbl_AppName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_AppName.Location = new System.Drawing.Point(387, 1);
            this.lbl_AppName.Name = "lbl_AppName";
            this.lbl_AppName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_AppName.Size = new System.Drawing.Size(183, 26);
            this.lbl_AppName.TabIndex = 4;
            this.lbl_AppName.Text = "AppName";
            this.lbl_AppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Version
            // 
            this.lbl_Version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Version.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Version.Location = new System.Drawing.Point(830, 2);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_Version.Size = new System.Drawing.Size(70, 26);
            this.lbl_Version.TabIndex = 5;
            this.lbl_Version.Text = "version";
            this.lbl_Version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Time
            // 
            this.lbl_Time.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Time.Location = new System.Drawing.Point(208, 2);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_Time.Size = new System.Drawing.Size(70, 26);
            this.lbl_Time.TabIndex = 6;
            this.lbl_Time.Text = "time";
            this.lbl_Time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_User
            // 
            this.lbl_User.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_User.Location = new System.Drawing.Point(96, 2);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_User.Size = new System.Drawing.Size(93, 26);
            this.lbl_User.TabIndex = 7;
            this.lbl_User.Text = "user";
            this.lbl_User.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Appid
            // 
            this.lbl_Appid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Appid.Location = new System.Drawing.Point(28, 2);
            this.lbl_Appid.Name = "lbl_Appid";
            this.lbl_Appid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_Appid.Size = new System.Drawing.Size(70, 26);
            this.lbl_Appid.TabIndex = 8;
            this.lbl_Appid.Text = "appid";
            this.lbl_Appid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spreadsheetMain
            // 
            this.spreadsheetMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetMain.Location = new System.Drawing.Point(0, 30);
            this.spreadsheetMain.MenuManager = this.rightClickBarManager;
            this.spreadsheetMain.Name = "spreadsheetMain";
            this.spreadsheetMain.Options.Culture = new System.Globalization.CultureInfo("zh-CN");
            this.spreadsheetMain.Size = new System.Drawing.Size(955, 548);
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
            // rightClickBarManager
            // 
            this.rightClickBarManager.DockControls.Add(this.barDockControlTop);
            this.rightClickBarManager.DockControls.Add(this.barDockControlBottom);
            this.rightClickBarManager.DockControls.Add(this.barDockControlLeft);
            this.rightClickBarManager.DockControls.Add(this.barDockControlRight);
            this.rightClickBarManager.Form = this;
            this.rightClickBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.ts_multiSelect});
            this.rightClickBarManager.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(955, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 627);
            this.barDockControlBottom.Size = new System.Drawing.Size(955, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 627);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(955, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 627);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // ts_multiSelect
            // 
            this.ts_multiSelect.Caption = "多选";
            this.ts_multiSelect.Id = 1;
            this.ts_multiSelect.Name = "ts_multiSelect";
            this.ts_multiSelect.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ts_multiSelect_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelControl1.Controls.Add(this.btn_Exel);
            this.panelControl1.Controls.Add(this.dbtn_Search);
            this.panelControl1.Controls.Add(this.dbtn_Edit);
            this.panelControl1.Controls.Add(this.dbtn_New);
            this.panelControl1.Controls.Add(this.dbtn_Execute);
            this.panelControl1.Controls.Add(this.btn_Save);
            this.panelControl1.Controls.Add(this.btn_Cancel);
            this.panelControl1.Controls.Add(this.btn_Delete);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 578);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(955, 49);
            this.panelControl1.TabIndex = 2;
            // 
            // btn_Exel
            // 
            this.btn_Exel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Exel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Exel.Image")));
            this.btn_Exel.Location = new System.Drawing.Point(622, 7);
            this.btn_Exel.Name = "btn_Exel";
            this.btn_Exel.Size = new System.Drawing.Size(85, 35);
            this.btn_Exel.TabIndex = 4;
            this.btn_Exel.Text = "Excel";
            this.btn_Exel.Click += new System.EventHandler(this.btn_Exel_Click);
            // 
            // dbtn_Search
            // 
            this.dbtn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbtn_Search.Image = ((System.Drawing.Image)(resources.GetObject("dbtn_Search.Image")));
            this.dbtn_Search.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.dbtn_Search.Location = new System.Drawing.Point(713, 7);
            this.dbtn_Search.Name = "dbtn_Search";
            this.dbtn_Search.Size = new System.Drawing.Size(100, 35);
            this.dbtn_Search.TabIndex = 7;
            this.dbtn_Search.Text = "Search";
            this.dbtn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // dbtn_Edit
            // 
            this.dbtn_Edit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dbtn_Edit.Image = ((System.Drawing.Image)(resources.GetObject("dbtn_Edit.Image")));
            this.dbtn_Edit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.dbtn_Edit.Location = new System.Drawing.Point(109, 7);
            this.dbtn_Edit.Name = "dbtn_Edit";
            this.dbtn_Edit.Size = new System.Drawing.Size(85, 35);
            this.dbtn_Edit.TabIndex = 8;
            this.dbtn_Edit.Text = "Edit";
            this.dbtn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // dbtn_New
            // 
            this.dbtn_New.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide;
            this.dbtn_New.Image = ((System.Drawing.Image)(resources.GetObject("dbtn_New.Image")));
            this.dbtn_New.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.dbtn_New.Location = new System.Drawing.Point(18, 7);
            this.dbtn_New.Name = "dbtn_New";
            this.dbtn_New.Size = new System.Drawing.Size(85, 35);
            this.dbtn_New.TabIndex = 9;
            this.dbtn_New.Text = "New";
            this.dbtn_New.Click += new System.EventHandler(this.btn_New_Click);
            // 
            // dbtn_Execute
            // 
            this.dbtn_Execute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbtn_Execute.Image = ((System.Drawing.Image)(resources.GetObject("dbtn_Execute.Image")));
            this.dbtn_Execute.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.dbtn_Execute.Location = new System.Drawing.Point(819, 7);
            this.dbtn_Execute.Name = "dbtn_Execute";
            this.dbtn_Execute.Size = new System.Drawing.Size(110, 35);
            this.dbtn_Execute.TabIndex = 10;
            this.dbtn_Execute.Text = "Execute";
            this.dbtn_Execute.Click += new System.EventHandler(this.dbtn_Execute_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.Location = new System.Drawing.Point(509, 7);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(85, 35);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "Save";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Image")));
            this.btn_Cancel.Location = new System.Drawing.Point(415, 7);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(85, 35);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_Delete.Image")));
            this.btn_Delete.Location = new System.Drawing.Point(200, 7);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(85, 35);
            this.btn_Delete.TabIndex = 6;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // popupSpread
            // 
            this.popupSpread.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ts_multiSelect)});
            this.popupSpread.Manager = this.rightClickBarManager;
            this.popupSpread.Name = "popupSpread";
            // 
            // popupDataCfg
            // 
            this.popupDataCfg.Name = "popupDataCfg";
            // 
            // timer100ms
            // 
            this.timer100ms.Tick += new System.EventHandler(this.timer100ms_Tick);
            // 
            // XSheetMain
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(955, 627);
            this.Controls.Add(this.spreadsheetMain);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "XSheetMain";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightClickBarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupDataCfg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lbl_AppName;
        private System.Windows.Forms.Label lbl_Version;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label lbl_User;
        private System.Windows.Forms.Label lbl_Appid;
        private DevExpress.XtraEditors.DropDownButton dbtn_Search;
        private DevExpress.XtraEditors.DropDownButton dbtn_Edit;
        private DevExpress.XtraEditors.DropDownButton dbtn_New;
        private DevExpress.XtraEditors.DropDownButton dbtn_Execute;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_Delete;
        private DevExpress.XtraBars.BarManager rightClickBarManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu popupSpread;
        private DevExpress.XtraBars.Alerter.AlertControl alertcontrolMain;
        private DevExpress.XtraBars.PopupMenu popupDataCfg;
        private System.Windows.Forms.Timer timer100ms;
        private DevExpress.XtraEditors.SimpleButton btn_Exel;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarToggleSwitchItem ts_multiSelect;
    }

}