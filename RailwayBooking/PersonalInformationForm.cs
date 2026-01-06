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
    public partial class PersonalInformationForm : Form
    {
        SqlConnection conn = new(Global.conn_str);
        SqlCommand command;
        SqlDataReader reader;
        public PersonalInformationForm()
        {
            InitializeComponent();
            SetupProfileLayout();
        }
        public void SetupProfileLayout()
        {
            conn.Open();
            command = new(@"select * from [user] where user_id = @user_id", conn);
            command.Parameters.Add("@user_id", SqlDbType.Int);
            command.Parameters["@user_id"].Value = Global.user_id;
            reader = command.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);
            DataRow dr = dt.Rows[0];


            TableLayoutPanel tlp = new();
            tlp.Dock = DockStyle.Top;
            tlp.AutoSize = true;
            tlp.ColumnCount = 2;
            tlp.RowCount = 3;

            // 設定欄位樣式
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // 加入元件
            tlp.Controls.Add(new Label { Text = "電子郵件：", Anchor = AnchorStyles.Left, AutoSize = true, Margin = new Padding(3, 10, 3, 10) }, 0, 0);
            tlp.Controls.Add(new Label { Text = dr["email"].ToString(), Anchor = AnchorStyles.Left, AutoSize = true, Margin = new Padding(3, 10, 3, 10) }, 1, 0);

            tlp.Controls.Add(new Label { Text = "註冊日期：", Anchor = AnchorStyles.Left, AutoSize = true, Margin = new Padding(3, 10, 3, 10) }, 0, 1);
            tlp.Controls.Add(new Label { Text = dr["created_at"].ToString(), Anchor = AnchorStyles.Left, AutoSize = true, Margin = new Padding(3, 10, 3, 10) }, 1, 1);
            
            tlp.Controls.Add(new Label { Text = "會員點數：", Anchor = AnchorStyles.Left, AutoSize = true, Margin = new Padding(3, 10, 3, 10) }, 0, 2);
            tlp.Controls.Add(new Label { Text = dr["member_points"].ToString(), Anchor = AnchorStyles.Left, AutoSize = true, Margin = new Padding(3, 10, 3, 10) }, 1, 2);

            // 將佈局加入表單
            this.Controls.Add(tlp);
            tlp.BringToFront();

        }
    }
}
