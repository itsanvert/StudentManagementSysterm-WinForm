using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem
{
    internal class cDataConn
    {
        public static string myConnection = "Data Source=VERT\\SQLEXPRESS;Initial Catalog=SMDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public static SqlConnection conn = new SqlConnection(myConnection);
    }
}
