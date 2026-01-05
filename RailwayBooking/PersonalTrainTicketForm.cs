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

            command = new(@"EXEC dbo.GetUserBookingHistory @QueryUserID = @QueryUserID;", conn);
            command.Parameters.Add("@QueryUserID", SqlDbType.Int);
            command.Parameters["@QueryUserID"].Value = Global.user_id;
            reader = command.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoResizeColumns();
        }

        private void PersonalTrainTicketForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
