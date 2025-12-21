using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace RailwayBooking
{
    public partial class LoginForm : Form
    {
        SHA256 hash = SHA256.Create();
        byte[] salt, source, crypto;

        SqlConnection conn;
        SqlCommand command;
        SqlDataReader reader;
        DataTable dt = new DataTable();
        public LoginForm()
        {
            InitializeComponent();
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
            if (dt.Rows.Count != 1){
                MessageBox.Show("帳號或密碼不正確");
                return;
            }

            DataRow row = dt.Rows[0];
            int ID = Convert.ToInt32(row["user_id"]);
            String pw_hash = row["password_hash"].ToString();
            salt = Convert.FromBase64String(row["password_salt"].ToString());
            source = Encoding.Default.GetBytes(textBox2.Text);
            crypto = hash.ComputeHash(source.Concat(salt).ToArray());

            if (pw_hash != Convert.ToBase64String(crypto)){
                MessageBox.Show("帳號或密碼不正確");
                return;
            }

            Global.email = row["email"].ToString();
            MessageBox.Show(Global.email + " 歡迎登入");

            LobbyForm form = new LobbyForm();
            form.ShowDialog();
            conn.Close();
        }
    }
}
