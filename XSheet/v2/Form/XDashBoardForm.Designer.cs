namespace XSheet.v2.Form
{
    partial class XDashBoardForm
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
            this.components = new System.ComponentModel.Container();
            this.dash_viewMain = new DevExpress.DashboardWin.DashboardViewer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dash_viewMain)).BeginInit();
            this.SuspendLayout();
            // 
            // dash_viewMain
            // 
            this.dash_viewMain.AllowDrop = true;
            this.dash_viewMain.AllowPrintDashboardItems = true;
            this.dash_viewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dash_viewMain.Location = new System.Drawing.Point(0, 0);
            this.dash_viewMain.Name = "dash_viewMain";
            this.dash_viewMain.Size = new System.Drawing.Size(511, 262);
            this.dash_viewMain.TabIndex = 0;
            // 
            // XDashBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 262);
            this.Controls.Add(this.dash_viewMain);
            this.Name = "XDashBoardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XDashBoard";
            ((System.ComponentModel.ISupportInitialize)(this.dash_viewMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.DashboardWin.DashboardViewer dash_viewMain;
    }
}