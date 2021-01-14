using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _100DaysOdCode_WinForms
{
    public class conexion
    {
        protected SqlConnection con;

        public SqlConnection EstablecerConexion()
        {
            string user = "sa";
            string bd = "BD_100DaysOfCode";
            string pass = "Pa$$w0rd138*";
            string server = "JARGS-PC";

            string cs = "User ID= "+ user + "; Password="+pass+";Initial Catalog="+bd+";Server="+server+";";

            con = new SqlConnection(cs);
            return con;
        }

    }
}
