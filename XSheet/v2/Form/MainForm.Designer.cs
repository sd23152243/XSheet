namespace XSheet.v2.Form
{
    partial class MainForm
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
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            this.navButton2 = new DevExpress.XtraBars.Navigation.NavButton();
            this.tileBar = new DevExpress.XtraBars.Navigation.TileBar();
            this.tbGroup = new DevExpress.XtraBars.Navigation.TileBarGroup();
            this.tileBarItem1 = new DevExpress.XtraBars.Navigation.TileBarItem();
            this.tileBarItem2 = new DevExpress.XtraBars.Navigation.TileBarItem();
            this.tileBarItem3 = new DevExpress.XtraBars.Navigation.TileBarItem();
            this.tileBarItem4 = new DevExpress.XtraBars.Navigation.TileBarItem();
            this.lbl_user = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Designer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // navButton2
            // 
            this.navButton2.Caption = "Main Menu";
            this.navButton2.IsMain = true;
            this.navButton2.Name = "navButton2";
            // 
            // tileBar
            // 
            this.tileBar.AllowDrag = false;
            this.tileBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tileBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.tileBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tileBar.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            this.tileBar.Groups.Add(this.tbGroup);
            this.tileBar.Location = new System.Drawing.Point(0, 98);
            this.tileBar.MaxId = 10;
            this.tileBar.Name = "tileBar";
            this.tileBar.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollButtons;
            this.tileBar.Size = new System.Drawing.Size(620, 126);
            this.tileBar.TabIndex = 0;
            this.tileBar.Text = "tileBar1";
            // 
            // tbGroup
            // 
            this.tbGroup.Items.Add(this.tileBarItem1);
            this.tbGroup.Items.Add(this.tileBarItem2);
            this.tbGroup.Items.Add(this.tileBarItem3);
            this.tbGroup.Items.Add(this.tileBarItem4);
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.Text = "APP选择";
            // 
            // tileBarItem1
            // 
            this.tileBarItem1.AppearanceItem.Normal.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.tileBarItem1.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileBarItem1.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement1.Text = "人员管理系统";
            this.tileBarItem1.Elements.Add(tileItemElement1);
            this.tileBarItem1.Id = 6;
            this.tileBarItem1.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Medium;
            this.tileBarItem1.Name = "tileBarItem1";
            toolTipTitleItem1.Text = "tttt";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "ttttttttttt";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.tileBarItem1.SuperTip = superToolTip1;
            // 
            // tileBarItem2
            // 
            this.tileBarItem2.AppearanceItem.Normal.BackColor = System.Drawing.Color.LimeGreen;
            this.tileBarItem2.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileBarItem2.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement2.Text = "考勤系统";
            this.tileBarItem2.Elements.Add(tileItemElement2);
            this.tileBarItem2.Id = 7;
            this.tileBarItem2.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Medium;
            this.tileBarItem2.Name = "tileBarItem2";
            // 
            // tileBarItem3
            // 
            this.tileBarItem3.AppearanceItem.Normal.BackColor = System.Drawing.Color.Orange;
            this.tileBarItem3.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileBarItem3.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement3.Text = "EFC维护";
            this.tileBarItem3.Elements.Add(tileItemElement3);
            this.tileBarItem3.Id = 8;
            this.tileBarItem3.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Medium;
            this.tileBarItem3.Name = "tileBarItem3";
            // 
            // tileBarItem4
            // 
            this.tileBarItem4.AppearanceItem.Normal.BackColor = System.Drawing.Color.Red;
            this.tileBarItem4.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileBarItem4.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement4.Text = "iChart维护";
            this.tileBarItem4.Elements.Add(tileItemElement4);
            this.tileBarItem4.Id = 9;
            this.tileBarItem4.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Medium;
            this.tileBarItem4.Name = "tileBarItem4";
            // 
            // lbl_user
            // 
            this.lbl_user.AutoSize = true;
            this.lbl_user.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lbl_user.ForeColor = System.Drawing.Color.White;
            this.lbl_user.Location = new System.Drawing.Point(128, 32);
            this.lbl_user.Name = "lbl_user";
            this.lbl_user.Size = new System.Drawing.Size(128, 27);
            this.lbl_user.TabIndex = 2;
            this.lbl_user.Text = "SRF\\999999";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(35, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 36);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hello!";
            // 
            // btn_Designer
            // 
            this.btn_Designer.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_Designer.FlatAppearance.BorderSize = 0;
            this.btn_Designer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Designer.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Designer.ForeColor = System.Drawing.Color.White;
            this.btn_Designer.Location = new System.Drawing.Point(495, 12);
            this.btn_Designer.Name = "btn_Designer";
            this.btn_Designer.Size = new System.Drawing.Size(113, 39);
            this.btn_Designer.TabIndex = 5;
            this.btn_Designer.Text = "Designer";
            this.btn_Designer.UseVisualStyleBackColor = false;
            this.btn_Designer.Click += new System.EventHandler(this.btn_Designer_Click);
            // 
            // MainForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.Navy;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 224);
            this.Controls.Add(this.btn_Designer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_user);
            this.Controls.Add(this.tileBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Navigation.NavButton navButton2;
        private DevExpress.XtraBars.Navigation.TileBar tileBar;
        private DevExpress.XtraBars.Navigation.TileBarGroup tbGroup;
        private DevExpress.XtraBars.Navigation.TileBarItem tileBarItem1;
        private DevExpress.XtraBars.Navigation.TileBarItem tileBarItem2;
        private DevExpress.XtraBars.Navigation.TileBarItem tileBarItem3;
        private DevExpress.XtraBars.Navigation.TileBarItem tileBarItem4;
        private System.Windows.Forms.Label lbl_user;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Designer;
    }
}