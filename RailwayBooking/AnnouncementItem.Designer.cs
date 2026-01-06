namespace RailwayBooking
{
    partial class AnnouncementItem
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            lblDate = new Label();
            lblTitle = new Label();
            SuspendLayout();
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.ForeColor = SystemColors.ButtonShadow;
            lblDate.Location = new Point(3, 33);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(42, 15);
            lblDate.TabIndex = 0;
            lblDate.Text = "label1";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.ForeColor = SystemColors.MenuText;
            lblTitle.Location = new Point(3, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(42, 15);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "label2";
            // 
            // AnnouncementItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblTitle);
            Controls.Add(lblDate);
            Name = "AnnouncementItem";
            Size = new Size(500, 60);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDate;
        private Label lblTitle;
    }
}
