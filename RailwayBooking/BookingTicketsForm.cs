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

            dateTimePicker1.Value = DateTime.Now;
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
            command = new(@"select t.trips_id, sc.train_no, tr.name as train_name, tss.scheduled_departure from trips t
                            join trip_stops ts ON t.trips_id = ts.trips_id
                            join service_calender sc on t.service_calender_id = sc.service_calender_id
                            join Train tr on sc.train_id = tr.train_id
                            join trip_stop_schedules tss on sc.service_calender_id = tss.service_calender_id and tss.station_id = ts.station_id
                            where t.service_date = @service_date and ts.station_id = @start_station and ts.arr_time is not null and t.status = 'Active'
	                            and EXISTS (
		                            select * from trip_seats tse
		                            where tse.trips_id = t.trips_id and is_reserved = 0
	                            )
	                            and EXISTS (
		                            select * from trip_stops
		                            where trips_id = t.trips_id and station_id = @end_station and stop_seq > ts.stop_seq
	                            )", conn);
            
            command.Parameters.Add("@start_station", SqlDbType.Int);
            command.Parameters["@start_station"].Value = selected1.Value;
            command.Parameters.Add("@end_station", SqlDbType.Int);
            command.Parameters["@end_station"].Value = selected2.Value;
            command.Parameters.Add("@service_date", SqlDbType.Date);
            command.Parameters["@service_date"].Value = dateTimePicker1.Value;
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
                listBox1.Items.Add(new KeyValuePair<string, int>(row["train_no"].ToString() + "車次 " + row["train_name"].ToString() + row["train_no"] + " " + row["scheduled_departure"] + "開", Convert.ToInt32(row["trips_id"])));
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

            // 計算里程
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
            int point_add = lib.CalculateMemberPoint(fare);

            // 新增車票訂單
            command = new(@"INSERT INTO [dbo].[bookings]([user_id],[total_amount],[member_points]) VALUES(@user_id,@total_amount,@member_points)", conn);
            command.Parameters.Add("@user_id", SqlDbType.Int);
            command.Parameters["@user_id"].Value = Global.user_id;
            command.Parameters.Add("@total_amount", SqlDbType.Int);
            command.Parameters["@total_amount"].Value = fare;
            command.Parameters.Add("@member_points", SqlDbType.Int);
            command.Parameters["@member_points"].Value = point_add;
            int booking_id = (int)command.ExecuteScalar();

            // 查詢座位
            command = new(@"EXEC FindAvailableSeat @InputTripID = @InputTripID, @FromStationID = @start_station, @ToStationID = @end_station;", conn);
            command.Parameters.Add("@InputTripID", SqlDbType.Int);
            command.Parameters["@InputTripID"].Value = selected.Value;
            command.Parameters.Add("@start_station", SqlDbType.Int);
            command.Parameters["@start_station"].Value = selected1.Value;
            command.Parameters.Add("@end_station", SqlDbType.Int);
            command.Parameters["@end_station"].Value = selected2.Value;
            reader = command.ExecuteReader();
            DataTable seat_dt = new();
            seat_dt.Load(reader);

            // 新增車票區段
            command = new(@"INSERT INTO [dbo].[booking_segments]
                                   ([bookings_id],[train_id],[trips_id],[coach_no],[seat_no],[from_stop_seq],[to_stop_seq])
                             VALUES
                                   (<bookings_id, int,>,<train_id, int,>,<trips_id, int,>,<coach_no, int,>,<seat_no, varchar(5),>,<from_stop_seq, int,>,<to_stop_seq, int,>)", conn);
        }

    }
}
