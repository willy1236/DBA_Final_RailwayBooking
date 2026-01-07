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
            button5 = new Button();
            panel1 = new Panel();
            label2 = new Label();
            panel2 = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel3 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(384, 258);
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
            label1.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label1.Location = new Point(58, 47);
            label1.Name = "label1";
            label1.Size = new Size(91, 24);
            label1.TabIndex = 1;
            label1.Text = "歡迎登入 ";
            // 
            // button2
            // 
            button2.Location = new Point(45, 174);
            button2.Name = "button2";
            button2.Size = new Size(88, 45);
            button2.TabIndex = 2;
            button2.Text = "開始訂票";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(45, 256);
            button3.Name = "button3";
            button3.Size = new Size(88, 45);
            button3.TabIndex = 3;
            button3.Text = "查詢車票";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(45, 340);
            button4.Name = "button4";
            button4.Size = new Size(88, 45);
            button4.TabIndex = 4;
            button4.Text = "查詢列車動態";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(495, 258);
            button5.Name = "button5";
            button5.Size = new Size(88, 45);
            button5.TabIndex = 5;
            button5.Text = "個人資料";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(187, 450);
            panel1.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label2.Location = new Point(27, 68);
            label2.Name = "label2";
            label2.Size = new Size(124, 24);
            label2.TabIndex = 2;
            label2.Text = "鐵路訂票系統";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(224, 224, 224);
            panel2.Controls.Add(flowLayoutPanel1);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(button5);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(187, 115);
            panel2.Name = "panel2";
            panel2.Size = new Size(613, 335);
            panel2.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(33, 59);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(287, 244);
            flowLayoutPanel1.TabIndex = 6;
            flowLayoutPanel1.WrapContents = false;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.Window;
            panel3.Controls.Add(label1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(187, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(613, 109);
            panel3.TabIndex = 8;
            // 
            // LobbyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Name = "LobbyForm";
            Text = "訂票系統主頁";
            Load += LobbyForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Label label1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}