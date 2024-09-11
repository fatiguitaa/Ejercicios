using System.Data;
using System.Data.SqlClient;

namespace Practico_01.Datos.Utils
{
    public class DatosHelper
    {
        private static DatosHelper instance;
        private SqlConnection connection;

        private DatosHelper()
        {
            connection = new SqlConnection(Properties.Resources.connectionString); 
        }

        public static DatosHelper ObtenerInstancia()
        {
            if (instance == null)
            {
                instance = new DatosHelper();
            }

            return instance;
        }

        public SqlConnection ObtenerConexion()
        {
            connection.Open();
            return connection;
            connection.Close();
        }

        public DataTable SPDataTable(string sp)
        {
            DataTable dataTable = new DataTable();

            connection.Open();

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            dataTable.Load(cmd.ExecuteReader());
            connection.Close();

            return dataTable;
        }


        public DataTable SPDataTable(string sp, List<Parametro> parametros)
        {
            DataTable dataTable = new DataTable();
            
            connection.Open();

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parametros != null)
            {
                foreach (Parametro param in parametros)
                {
                    cmd.Parameters.AddWithValue(param.Nombre, param.Valor);
                }
            }
            dataTable.Load(cmd.ExecuteReader());
            connection.Close();

            return dataTable;
        }

        public int? SPNonQuery(string sp, List<Parametro>? parametros)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sp, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (Parametro param in parametros)
                    {
                        cmd.Parameters.AddWithValue(param.Nombre, param.Valor);
                    }
                }

                return cmd.ExecuteNonQuery();
            }
            catch
            {
                return null;
            }

        }

        public int? SPNonQuery(string sp, List<Parametro>? parametros, SqlTransaction transaccion)
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand(sp, connection, transaccion);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parametros != null)
            {
                foreach (Parametro param in parametros)
                {
                    cmd.Parameters.AddWithValue(param.Nombre, param.Valor);
                }
            }

            int resultado = cmd.ExecuteNonQuery();

            connection.Close();

            return resultado;
        }
    }
}
