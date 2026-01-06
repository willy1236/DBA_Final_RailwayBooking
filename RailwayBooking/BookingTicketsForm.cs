using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailwayBooking
{
    public partial class BookingTicketsForm : Form
    {
        SqlConnection conn = new(Global.conn_str);
        SqlCommand command;
        SqlDataReader reader;

        public BookingTicketsForm()
        {
            InitializeComponent();
            conn.Open();

            command = new("select * from [station]", conn);
            reader = command.ExecuteReader();
            DataTable staton_dt = new();
            staton_dt.Load(reader);
            foreach (DataRow row in staton_dt.Rows)
            {
                comboBox1.Items.Add(new KeyValuePair<string, int>(row["station_name"].ToString(), Convert.ToInt32(row["station_id"])));
                comboBox1.DisplayMember = "Key";
                comboBox1.ValueMember = "Value";
                comboBox2.Items.Add(new KeyValuePair<string, int>(row["station_name"].ToString(), Convert.ToInt32(row["station_id"])));
                comboBox2.DisplayMember = "Key";
                comboBox2.ValueMember = "Value";
            }

            // 測試用 應該為現在時間
            dateTimePicker1.Value = new DateTime(2026, 1, 7, 8, 0, 0);
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is not KeyValuePair<string, int>)
            {
                MessageBox.Show("未選擇班次");
                return;
            }
            if (comboBox1.SelectedItem is not KeyValuePair<string, int> || comboBox2.SelectedItem is not KeyValuePair<string, int>)
            {
                MessageBox.Show("未選擇起訖站");
                return;
            }
            var selected = (KeyValuePair<string, int>)listBox1.SelectedItem;
            var selected1 = (KeyValuePair<string, int>)comboBox1.SelectedItem;
            var selected2 = (KeyValuePair<string, int>)comboBox2.SelectedItem;
            
            command = new(@"EXEC dbo.GetTotalTravelDistance @StartStation = @start_station, @EndStation = @end_station;", conn);
            command.Parameters.Add("@start_station", SqlDbType.Int);
            command.Parameters["@start_station"].Value = selected1.Value;
            command.Parameters.Add("@end_station", SqlDbType.Int);
            command.Parameters["@end_station"].Value = selected2.Value;
            reader = command.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);
             
            int dist_km = Convert.ToInt32(dt.Rows[0]["total_distance"]);
            int fare = lib.CalculateFare(dist_km);
            label3.Text = "總里程：" + dist_km + " 金額：" + fare + " 可獲得" + lib.CalculateMemberPoint(fare) + "點";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is not KeyValuePair<string, int> || comboBox2.SelectedItem is not KeyValuePair<string, int>)
            {
                MessageBox.Show("未選擇起訖站");
                return;
            }
            var selected1 = (KeyValuePair<string, int>)comboBox1.SelectedItem;
            var selected2 = (KeyValuePair<string, int>)comboBox2.SelectedItem;
            
            // 查詢班次
            command = new(@"EXEC dbo.FindAvailableTrips @QueryDateTime = @service_datetime, @FromStationID = @start_station, @ToStationID = @end_station", conn);
            command.Parameters.Add("@start_station", SqlDbType.Int);
            command.Parameters["@start_station"].Value = selected1.Value;
            command.Parameters.Add("@end_station", SqlDbType.Int);
            command.Parameters["@end_station"].Value = selected2.Value;
            command.Parameters.Add("@service_datetime", SqlDbType.DateTime);
            command.Parameters["@service_datetime"].Value = dateTimePicker1.Value;
            reader = command.ExecuteReader();
            DataTable train_dt = new();
            train_dt.Load(reader);

            if (train_dt.Rows.Count == 0)
            {
                MessageBox.Show("選擇的時段與車站沒有班次");
                return;
            }

            listBox1.ClearSelected();
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            foreach (DataRow row in train_dt.Rows)
            {
                listBox1.Items.Add(new KeyValuePair<string, int>(row["車次"].ToString() + "車次 " + row["車種"].ToString() + " " + row["發車時間"] + " ~ " + row["抵達時間"] + " 餘" + row["剩餘座位數"] + "座位", Convert.ToInt32(row["班次ID"])));
            }
            listBox1.DisplayMember = "Key";
            listBox1.ValueMember = "Value";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is not KeyValuePair<string, int>)
            {
                MessageBox.Show("未選擇班次");
                return;
            }
            if (comboBox1.SelectedItem is not KeyValuePair<string, int> || comboBox2.SelectedItem is not KeyValuePair<string, int>)
            {
                MessageBox.Show("未選擇起訖站");
                return;
            }
            var selected = (KeyValuePair<string, int>)listBox1.SelectedItem;
            var selected1 = (KeyValuePair<string, int>)comboBox1.SelectedItem;
            var selected2 = (KeyValuePair<string, int>)comboBox2.SelectedItem;

            try
            {
                // 計算里程與金額
                command = new(@"EXEC dbo.GetTotalTravelDistance @StartStation = @start_station, @EndStation = @end_station;", conn);
                command.Parameters.AddWithValue("@start_station", selected1.Value);
                command.Parameters.AddWithValue("@end_station", selected2.Value);

                int dist_km = Convert.ToInt32(command.ExecuteScalar());
                int fare = lib.CalculateFare(dist_km);
                int point_add = lib.CalculateMemberPoint(fare);

                // 創建訂單
                command = new("[dbo].[CreateBooking]", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserID", Global.user_id);
                command.Parameters.AddWithValue("@TripID", selected.Value);
                command.Parameters.AddWithValue("@StartStation", selected1.Value);
                command.Parameters.AddWithValue("@EndStation", selected2.Value);
                command.Parameters.AddWithValue("@TotalAmount", fare);
                command.Parameters.AddWithValue("@MemberPoints", point_add);

                int booking_id = Convert.ToInt32(command.ExecuteScalar());

                MessageBox.Show($"訂購成功！訂單編號：{booking_id}，請至個人車票查看");
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("訂購失敗：" + ex.Message);
            }
            
        }

    }
}
