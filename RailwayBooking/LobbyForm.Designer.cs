namespace RailwayBooking
{
    partial class LobbyForm
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
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(211, 371);
            button1.Name = "button1";
            button1.Size = new Size(88, 45);
            button1.TabIndex = 0;
            button1.Text = "重設密碼";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(74, 47);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 1;
            label1.Text = "歡迎登入 ";
            // 
            // button2
            // 
            button2.Location = new Point(305, 371);
            button2.Name = "button2";
            button2.Size = new Size(88, 45);
            button2.TabIndex = 2;
            button2.Text = "開始訂票";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(399, 371);
            button3.Name = "button3";
            button3.Size = new Size(88, 45);
            button3.TabIndex = 3;
            button3.Text = "查詢車票";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(493, 371);
            button4.Name = "button4";
            button4.Size = new Size(88, 45);
            button4.TabIndex = 4;
            button4.Text = "查詢列車動態";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // LobbyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "LobbyForm";
            Text = "主頁";
            Load += LobbyForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}