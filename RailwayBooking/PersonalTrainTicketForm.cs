using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RailwayBooking
{
    public partial class PersonalTrainTicketForm : Form
    {
        SqlConnection conn = new(Global.conn_str);
        SqlCommand command;
        SqlDataReader reader;

        public PersonalTrainTicketForm()
        {
            InitializeComponent();
            conn.Open();

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.LabelEdit = false;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("訂單編號", 60);
            listView1.Columns.Add("車次", 50);
            listView1.Columns.Add("車種", 80);
            listView1.Columns.Add("乘車日期", 80);
            listView1.Columns.Add("起點站", 80);
            listView1.Columns.Add("上車時間", 80);
            listView1.Columns.Add("終點站", 80);
            listView1.Columns.Add("下車時間", 80);
            listView1.Columns.Add("座位", 70);
            listView1.Columns.Add("總金額", 70);
            listView1.Columns.Add("訂購時間", 130);
            listView1.Columns.Add("付款方式", 120);
            listView1.Columns.Add("訂票到期時間", 140);
            listView1.Columns.Add("付款時間", 140);
            listView1.Columns.Add("取消時間", 140);
            reload_booking();
        }

        private void PersonalTrainTicketForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new();

            // 設定紙張大小
            pd.DefaultPageSettings.PaperSize = new PaperSize("CustomTicket", 300, 500);

            pd.PrintPage += new PrintPageEventHandler(this.TicketLayout);

            // 顯示預覽或直接列印
            PrintPreviewDialog ppd = new PrintPreviewDialog { Document = pd };
            ppd.Size = new Size(300, 500);
            if (ppd.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }

        private void TicketLayout(object sender, PrintPageEventArgs e)
        {
            if (listView1.SelectedItems[0] is not ListViewItem)
            {
                MessageBox.Show("未選擇車票");
                return;
            }
            var selectedItem = (ListViewItem)listView1.SelectedItems[0];

            Graphics g = e.Graphics;
            Font titleFont = new("微軟正黑體", 16, FontStyle.Bold);
            Font contentFont = new("微軟正黑體", 10);
            Font smallFont = new("微軟正黑體", 8);

            int y = 20; // 垂直起始點

            //  標題
            g.DrawString("台灣鐵道車票", titleFont, Brushes.Black, 50, y);
            y += 40;

            // 分隔線
            g.DrawLine(Pens.Black, 20, y, 280, y);
            y += 10;

            // 車次與乘車資訊
            g.DrawString($"乘車日期: " + selectedItem.SubItems[3].Text, contentFont, Brushes.Black, 20, y);
            y += 25;
            g.DrawString($"起訖站: " + selectedItem.SubItems[4].Text + " -> " + selectedItem.SubItems[6].Text, contentFont, Brushes.Black, 20, y);
            y += 25;
            g.DrawString($"        " + selectedItem.SubItems[5].Text + " -> " + selectedItem.SubItems[7].Text, contentFont, Brushes.Black, 20, y);
            y += 25;
            g.DrawString($"車次: " + selectedItem.SubItems[1].Text + " " + selectedItem.SubItems[2].Text + "  座位: " + selectedItem.SubItems[8].Text, contentFont, Brushes.Black, 20, y);
            y += 40;

            // 價格
            g.DrawString("總計: NT$ " + selectedItem.SubItems[9].Text, titleFont, Brushes.Black, 100, y);
            y += 50;


            // 註腳
            g.DrawString("＊限當日當班次有效，逾時不退。", smallFont, Brushes.Black, 20, y + 60);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0] is not ListViewItem)
            {
                MessageBox.Show("未選擇車票");
                return;
            }
            var selectedItem = (ListViewItem)listView1.SelectedItems[0];
            if (selectedItem.SubItems[13].Text != "")
            {
                MessageBox.Show("此車票已付款");
                return;
            }
            if (selectedItem.SubItems[14].Text != "")
            {
                MessageBox.Show("此車票已取消");
                return;
            }
            if (DateTime.Now >= DateTime.ParseExact(selectedItem.SubItems[12].Text, "yyyy-MM-dd", CultureInfo.InvariantCulture))
            {
                MessageBox.Show("此車票已過期");
                return;
            }
            command = new(@"update [bookings] SET paid_at = @now, paid_method = 'test', confirmed_at = @now where bookings_id = @bookings_id", conn);
            command.Parameters.AddWithValue("@now", DateTime.Now);
            command.Parameters.AddWithValue("@bookings_id", Convert.ToInt32(selectedItem.SubItems[0].Text));
            command.ExecuteNonQuery();
            reload_booking();
            MessageBox.Show("付款完成");
        }
        private void reload_booking()
        {
            command = new(@"EXEC dbo.usp_GetUserBookingHistory @UserID = @UserID;", conn);
            command.Parameters.Add("@UserID", SqlDbType.Int);
            command.Parameters["@UserID"].Value = Global.user_id;
            reader = command.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);

            listView1.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                var item = new ListViewItem(row["訂單編號"].ToString());
                item.SubItems.Add(row["車次"].ToString());
                item.SubItems.Add(row["車種"].ToString());
                item.SubItems.Add(DateTime.Parse(row["乘車日期"].ToString()).Date.ToString("yyyy-MM-dd"));
                item.SubItems.Add(row["起點站"].ToString());
                item.SubItems.Add(row["上車時間"].ToString());
                item.SubItems.Add(row["終點站"].ToString());
                item.SubItems.Add(row["下車時間"].ToString());
                item.SubItems.Add(row["座位"].ToString());
                item.SubItems.Add(row["總金額"].ToString());
                item.SubItems.Add(row["訂購時間"].ToString());
                item.SubItems.Add(row["付款方式"].ToString());
                item.SubItems.Add(row["訂票到期時間"].ToString());
                item.SubItems.Add(row["付款時間"].ToString());
                item.SubItems.Add(row["取消時間"].ToString());
                listView1.Items.Add(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0] is not ListViewItem)
            {
                MessageBox.Show("未選擇車票");
                return;
            }
            var selectedItem = (ListViewItem)listView1.SelectedItems[0];
            if (selectedItem.SubItems[14].Text != "")
            {
                MessageBox.Show("此車票已取消");
                return;
            }
            if (DateTime.Now >= DateTime.ParseExact(selectedItem.SubItems[12].Text, "yyyy-MM-dd", CultureInfo.InvariantCulture))
            {
                MessageBox.Show("此車票已過期");
                return;
            }
            command = new(@"update [bookings] SET [cancelled_at] = @now where bookings_id = @bookings_id;
                            DELETE FROM [dbo].[booking_segments] where bookings_id = @bookings_id;", conn);
            command.Parameters.AddWithValue("@now", DateTime.Now);
            command.Parameters.AddWithValue("@bookings_id", Convert.ToInt32(selectedItem.SubItems[0].Text));
            command.ExecuteNonQuery();
            reload_booking();
            MessageBox.Show("訂單取消/退票完成");
        }
    }
}
