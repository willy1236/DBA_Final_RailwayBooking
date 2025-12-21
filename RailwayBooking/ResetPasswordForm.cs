using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace RailwayBooking
{
    public partial class ResetPasswordForm : Form
    {
        SHA256 hash = SHA256.Create();
        byte[] salt;
        byte[] source;
        byte[] crypto;

        SqlConnection conn;
        SqlCommand command;
        SqlDataReader reader;
        DataTable dt = new DataTable();
        public ResetPasswordForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Global.email;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(@"Data Source=127.0.0.1\SQL2022_1141; Integrated Security=false;user=sqluser;password=123; Initial Catalog=BookTrainTickets");
            conn.Open();

            command = new SqlCommand(@"select * from [user] where email = @email", conn);
            command.Parameters.Add("@email", SqlDbType.NVarChar);
            command.Parameters["@email"].Value = textBox1.Text;

            reader = command.ExecuteReader();
            dt.Load(reader);
            reader.Close();
            if (dt.Rows.Count != 1)
            {
                // 0 表未找到
                // >1 可能受到攻擊產生過多資料
                MessageBox.Show("帳號不存在");
                return;
            }
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("密碼不一致");
                return;
            }

            DataRow row = dt.Rows[0];
            salt = Convert.FromBase64String(row["password_salt"].ToString());
            source = Encoding.Default.GetBytes(textBox2.Text);
            crypto = hash.ComputeHash(source.Concat(salt).ToArray());
            int ID = Convert.ToInt32(row["user_id"]);

            command = new SqlCommand(@"Update [user] Set [password_hash] = @password_hash Where [user_id] = @user_id", conn);
            command.Parameters.Add("@password_hash", SqlDbType.NVarChar);
            command.Parameters["@password_hash"].Value = Convert.ToBase64String(crypto);
            command.Parameters.Add("@user_id", SqlDbType.Int);
            command.Parameters["@user_id"].Value = ID;
            int UpdateNo = command.ExecuteNonQuery();
            conn.Close();

            if (UpdateNo == 1)
            {
                MessageBox.Show("修改完成");
                this.Close();
            }
            else
            {
                MessageBox.Show("修改失敗");
            }
        }
    }
}
