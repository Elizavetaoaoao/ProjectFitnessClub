using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFitnessClub
{
    public class ConnectionClass
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-1QKCL2P;Initial Catalog=FITNESS_CLUB;Integrated Security=True;TrustServerCertificate=True");
        public SqlConnection getConnection() { return conn; }
    }
}
