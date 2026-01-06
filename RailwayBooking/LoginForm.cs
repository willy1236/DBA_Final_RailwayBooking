using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace RailwayBooking
{
    public partial class LoginForm : Form
    {
        SHA256 hash = SHA256.Create();

        SqlConnection conn = new SqlConnection(Global.conn_str);
        SqlCommand command;
        SqlDataReader reader;
        DataTable dt = new DataTable();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();

            command = new SqlCommand(@"select * from [user] where email = @email", conn);
            command.Parameters.Add("@email", SqlDbType.NVarChar);
            command.Parameters["@email"].Value = textBox1.Text;

            reader = command.ExecuteReader();
            dt.Load(reader);
            reader.Close();
            if (dt.Rows.Count != 1){
                MessageBox.Show("帳號或密碼不正確");
                return;
            }

            DataRow row = dt.Rows[0];
            int ID = Convert.ToInt32(row["user_id"]);
            String pw_hash = row["password_hash"].ToString();
            byte[] salt = Convert.FromBase64String(row["password_salt"].ToString());
            byte[] source = Encoding.Default.GetBytes(textBox2.Text);
            byte[] crypto = hash.ComputeHash(source.Concat(salt).ToArray());

            if (pw_hash != Convert.ToBase64String(crypto)){
                MessageBox.Show("帳號或密碼不正確");
                return;
            }

            Global.email = row["email"].ToString();
            Global.user_id = Convert.ToInt32(row["user_id"]);
            MessageBox.Show(Global.email + " 歡迎登入");

            LobbyForm form = new LobbyForm();
            form.ShowDialog();
            this.Close();
        }
    }
}
