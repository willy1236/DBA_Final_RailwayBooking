using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayBooking
{
    public static class Global
    {
        public static string email { get; set; } = "passenger01@gmail.com";
        public static int user_id { get; set; } = 2;
        public static string conn_str { get; } = @"Data Source=127.0.0.1\SQL2022_1141; Integrated Security=false;user=sqluser;password=123; Initial Catalog=BookTrainTickets";
    }
}
