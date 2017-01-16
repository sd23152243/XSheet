namespace tools
{
    partial class Form_Tools
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_Input = new DevExpress.XtraEditors.TextEdit();
            this.txt_Des = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Input.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Des.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_Input
            // 
            this.txt_Input.Location = new System.Drawing.Point(59, 13);
            this.txt_Input.Name = "txt_Input";
            this.txt_Input.Size = new System.Drawing.Size(241, 20);
            this.txt_Input.TabIndex = 0;
            this.txt_Input.EditValueChanged += new System.EventHandler(this.txt_Input_EditValueChanged);
            // 
            // txt_Des
            // 
            this.txt_Des.Location = new System.Drawing.Point(59, 60);
            this.txt_Des.Name = "txt_Des";
            this.txt_Des.Size = new System.Drawing.Size(241, 20);
            this.txt_Des.TabIndex = 1;
            this.txt_Des.EditValueChanged += new System.EventHandler(this.txt_Des_EditValueChanged);
            // 
            // Form_Tools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 112);
            this.Controls.Add(this.txt_Des);
            this.Controls.Add(this.txt_Input);
            this.Name = "Form_Tools";
            this.Text = "Tools";
            ((System.ComponentModel.ISupportInitialize)(this.txt_Input.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Des.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txt_Input;
        private DevExpress.XtraEditors.TextEdit txt_Des;
    }
}

