namespace RailwayBooking
{
    partial class BookingTicketsForm
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
            dateTimePicker1 = new DateTimePicker();
            listBox1 = new ListBox();
            comboBox1 = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            comboBox2 = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(59, 98);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 0;
            dateTimePicker1.Value = new DateTime(2026, 1, 5, 17, 3, 35, 0);
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(339, 81);
            listBox1.Margin = new Padding(5);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(364, 214);
            listBox1.TabIndex = 3;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged_1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(59, 171);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(59, 359);
            button1.Name = "button1";
            button1.Size = new Size(182, 35);
            button1.TabIndex = 5;
            button1.Text = "查詢班次";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(450, 359);
            button2.Name = "button2";
            button2.Size = new Size(182, 35);
            button2.TabIndex = 6;
            button2.Text = "訂購";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(59, 140);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 7;
            label1.Text = "起始站";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(59, 226);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 8;
            label2.Text = "終點站";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(59, 254);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(121, 23);
            comboBox2.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(450, 330);
            label3.Name = "label3";
            label3.Size = new Size(145, 15);
            label3.TabIndex = 10;
            label3.Text = "總里程：金額： 可獲得 點";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(59, 68);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 11;
            label4.Text = "出發日期";
            // 
            // BookingTicketsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(comboBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(listBox1);
            Controls.Add(dateTimePicker1);
            Name = "BookingTicketsForm";
            Text = "開始訂票";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dateTimePicker1;
        private ListBox listBox1;
        private ComboBox comboBox1;
        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private ComboBox comboBox2;
        private Label label3;
        private Label label4;
    }
}