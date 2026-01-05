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
    public partial class TrainStatusForm : Form
    {
        SqlConnection conn = new SqlConnection(Global.conn_str);
        SqlCommand command;
        SqlDataReader reader;
        DataTable dt = new DataTable();
        public TrainStatusForm()
        {
            InitializeComponent();
            conn.Open();
            
            command = new SqlCommand("select * from [station]", conn);
            reader = command.ExecuteReader();
            DataTable staton_dt = new DataTable();
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

        private void button1_Click(object sender, EventArgs e)
        {
            command = new(@"SELECT 
                            t.trips_id AS [班次ID],
                            tr.name AS [車種],
                            sc.train_no AS [車次],
                            st_start.station_name AS [出發站],
                            st_end.station_name AS [抵達站],

                            CONVERT(VARCHAR(5), start_s.dep_time, 108) AS [出發時間],
                            CONVERT(VARCHAR(5), end_s.arr_time, 108) AS [抵達時間],
    
                            DATEDIFF(MINUTE, start_s.dep_time, end_s.arr_time) AS [行駛時間(分)],
    
                            CASE 
                                WHEN t.status = 'Active' THEN '準點'
                                WHEN t.status = 'Delayed' THEN '誤點'
                                WHEN t.status = 'Cancelled' THEN '取消'
                            END AS [狀態]

                        FROM dbo.trips t
                        JOIN dbo.service_calender sc ON t.service_calender_id = sc.service_calender_id
                        JOIN dbo.Train tr ON sc.train_id = tr.train_id

                        JOIN dbo.trip_stops start_s ON t.trips_id = start_s.trips_id
                        JOIN dbo.station st_start ON start_s.station_id = st_start.station_id

                        JOIN dbo.trip_stops end_s ON t.trips_id = end_s.trips_id
                        JOIN dbo.station st_end ON end_s.station_id = st_end.station_id

                        WHERE 
                            t.service_date = @TravelDate
                            AND start_s.station_id = @FromStationID
                            AND end_s.station_id = @ToStationID
    
                            AND start_s.stop_seq < end_s.stop_seq
                        ORDER BY start_s.dep_time;", conn);
            if (comboBox1.SelectedItem is KeyValuePair<string, int> selected1 && comboBox2.SelectedItem is KeyValuePair<string, int> selected2)
            {
                command.Parameters.Add("@FromStationID", SqlDbType.Int);
                command.Parameters["@FromStationID"].Value = selected1.Value;
                command.Parameters.Add("@ToStationID", SqlDbType.Int);
                command.Parameters["@ToStationID"].Value = selected2.Value;
                command.Parameters.Add("@TravelDate", SqlDbType.Date);
                command.Parameters["@TravelDate"].Value = dateTimePicker1.Value;
                reader = command.ExecuteReader();
                dt.Load(reader);
            }
            else
            {
                MessageBox.Show("沒有選擇起訖站");
                return;
            }

            dataGridView1.DataSource = dt;
            dataGridView1.AutoResizeColumns();
        }
    }
}
