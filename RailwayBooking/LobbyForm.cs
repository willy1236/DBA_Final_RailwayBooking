using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailwayBooking
{
    public partial class LobbyForm : Form
    {
        public LobbyForm()
        {
            InitializeComponent();
            LoadAnnouncements();
        }

        private void LobbyForm_Load(object sender, EventArgs e)
        {
            label1.Text = "歡迎登入 " + Global.email;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetPasswordForm form = new();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BookingTicketsForm form = new();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PersonalTrainTicketForm form = new();
            form.ShowDialog();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            TrainStatusForm form = new();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PersonalInformationForm form = new();
            form.ShowDialog();
        }

        private void LoadAnnouncements()
        {
            // 清空目前的列表
            flowLayoutPanel1.Controls.Clear();

            // 模擬從資料庫取得資料
            var mockData = new List<dynamic>
            {
                new { Id = 1, Date = "2026/01/05", Title = "【系統公告】春節期間訂票時程調整通知" },
                new { Id = 2, Date = "2025/12/30", Title = "【維護】1/10 凌晨系統停機維護公告" },
                new { Id = 3, Date = "2025/12/25", Title = "【優惠】早鳥票折扣方案更新" },
                new { Id = 4, Date = "2025/12/20", Title = "若是看到這則公告，代表介面載入成功！" }
            };

            // 動態產生控制項
            foreach (var data in mockData)
            {
                AnnouncementItem item = new();

                // 填入資料
                item.Title = data.Title;
                item.Date = data.Date;
                item.AnnouncementId = data.Id;

                // 設定寬度跟 FlowLayoutPanel 一樣寬 (扣掉一點捲軸空間)
                item.Width = flowLayoutPanel1.ClientSize.Width - 25;

                // 綁定點擊事件
                item.OnItemClick += Announcement_Clicked;

                // 加入到容器中
                flowLayoutPanel1.Controls.Add(item);
            }
        }

        // 處理點擊事件
        private void Announcement_Clicked(object sender, EventArgs e)
        {
            var item = sender as AnnouncementItem;
            if (item != null)
            {
                MessageBox.Show($"您點擊了 ID: {item.AnnouncementId}\n標題: {item.Title}", "開啟公告內容");
            }
        }

        // 確保調整視窗大小時，公告項目跟著變寬
        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                ctrl.Width = flowLayoutPanel1.ClientSize.Width - 25;
            }
        }
    }
}
