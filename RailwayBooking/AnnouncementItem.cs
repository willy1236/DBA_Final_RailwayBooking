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
    public partial class AnnouncementItem : UserControl
    {
        public event EventHandler OnItemClick;

        public AnnouncementItem()
        {
            InitializeComponent();

            this.Click += (s, e) => OnItemClick?.Invoke(this, e);
            lblTitle.Click += (s, e) => OnItemClick?.Invoke(this, e);
            lblDate.Click += (s, e) => OnItemClick?.Invoke(this, e);

            this.MouseEnter += Item_MouseEnter;
            lblTitle.MouseEnter += Item_MouseEnter;
            lblDate.MouseEnter += Item_MouseEnter;

            this.MouseLeave += Item_MouseLeave;
            lblTitle.MouseLeave += Item_MouseLeave;
            lblDate.MouseLeave += Item_MouseLeave;
        }

        public string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        public string Date
        {
            get { return lblDate.Text; }
            set { lblDate.Text = value; }
        }

        public int AnnouncementId { get; set; }

        private void Item_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(240, 245, 255); // 淺藍灰色
        }

        private void Item_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
    }
}
