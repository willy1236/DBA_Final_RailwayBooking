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
    }
}
