using Practico_01.Datos.Utils;
using Practico_01.Dominio;
using System.Data;
using System.Data.SqlClient;


namespace Practico_01.Datos
{
    public class FacturaRepository : IFacturaRepository
    {
        public List<Factura> ObtenerTodas()
        {
            string sp = "ObtenerTodas";
            var dataTable = DatosHelper.ObtenerInstancia().SPDataTable(sp);

            var lista = new List<Factura>();
            foreach (DataRow fila in dataTable.Rows)
            {
                Factura factura = new Factura();
                factura.NroFactura = (int)fila["nroFactura"];
                factura.Fecha = (DateTime)fila["fecha"];
                factura.IdFormaPago = (int)fila["idFormaPago"];

                lista.Add(factura);
            }

            return lista;
        }

        public Factura ObtenerPorId(int id)
        {
            string sp = "ObtenerPorId";
            var parametros = new List<Parametro>();
            parametros.Add(new Parametro("@nroFactura", id));
            
            var dataTable = DatosHelper.ObtenerInstancia().SPDataTable(sp, parametros);

            var factura = new Factura();

            foreach (DataRow fila in dataTable.Rows)
            {
                factura.NroFactura = (int)fila["nroFactura"];
                factura.Fecha = (DateTime)fila["fecha"];
                factura.IdFormaPago = (int)fila["idFormaPago"];
            }


            return factura;
        }

        public bool Guardar(Factura factura)
        {
            SqlTransaction transaccion = DatosHelper.ObtenerInstancia().ObtenerConexion().BeginTransaction();
            try
            { 
                string spFactura = "InsertarFactura";

                var parametrosFactura = new List<Parametro>();
                parametrosFactura.Add(new Parametro("@fecha", factura.Fecha));
                parametrosFactura.Add(new Parametro("@idFormaPago", factura.IdFormaPago));

                int? filasFactura = DatosHelper.ObtenerInstancia().SPNonQuery(spFactura, parametrosFactura);

                string spDetalleFactura = "InsertarDetalleFactura";

                int? filasDetalleFactura = null;

                foreach (DetalleFacturas detalle in factura.Detalles)
                {
                    List<Parametro> parametrosDetalleFactura = new List<Parametro>();
                    parametrosDetalleFactura.Add(new Parametro("@nroFactura", factura.NroFactura));
                    parametrosDetalleFactura.Add(new Parametro("@idArticulo", detalle.IdArticulo));
                    parametrosDetalleFactura.Add(new Parametro("@cantidad", detalle.Cantidad));

                    filasDetalleFactura = DatosHelper.ObtenerInstancia().SPNonQuery(spDetalleFactura, parametrosDetalleFactura, transaccion);
                }

                transaccion.Commit();
                return true;
            }
            catch
            {
                transaccion.Rollback();
                return false;
            }
        }

        public bool BorrarPorId(int id)
        {
            string sp = "BorrarFactura";
            var parametros = new List<Parametro>();

            parametros.Add(new Parametro("@nroFactura", id));

            int? resultado = DatosHelper.ObtenerInstancia().SPNonQuery(sp, parametros);

            if (resultado is null)
            {
                return false;
            }
            return true;
        }
    }
}
