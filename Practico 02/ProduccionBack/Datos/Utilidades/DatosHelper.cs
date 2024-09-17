using System.Data;
using System.Data.SqlClient;

namespace ProduccionBack.Datos.Utilidades
{
    public class DatosHelper
    {
        private static DatosHelper instancia = new DatosHelper();
        private SqlConnection conexion = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Facu;Integrated Security=True;");

        private DatosHelper() { }

        public static DatosHelper ObtenerInstancia()
        {
            return instancia;
        }

        public DataTable? SPDataTable(string sp)
        {
            try
            {
                DataTable dataTable = new DataTable();

                conexion.Open();

                SqlCommand cmd = new SqlCommand(sp, conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                dataTable.Load(cmd.ExecuteReader());

                conexion.Close();

                return dataTable;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public DataTable? SPDataTable(string sp, List<Parametro> parametros)
        {
            try
            {
                DataTable dataTable = new DataTable();

                conexion.Open();

                SqlCommand cmd = new SqlCommand(sp, conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (Parametro parametro in parametros)
                {
                    cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);

                }
                dataTable.Load(cmd.ExecuteReader());

                conexion.Close();

                return dataTable;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public bool? SPNonQuery(string sp, List<Parametro>? parametros)
        {
            try
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand(sp, conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parametros is not null)
                {
                    foreach (Parametro parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                    }
                }

                int resultado = cmd.ExecuteNonQuery();

                conexion.Close();

                return resultado == 1;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }
    }
}
