using ProduccionBack.Contratos;
using ProduccionBack.Datos.Utilidades;
using ProduccionBack.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace ProduccionBack.Datos
{
    public class FacturaRepository : IFacturaRepository
    {
        private static FacturaRepository instancia = new FacturaRepository();

        private FacturaRepository() { }

        public static FacturaRepository ObtenerInstancia() => instancia;

        public List<Factura>? ObtenerTodas()
        {
            var dataTableFacturas = DatosHelper.ObtenerInstancia().SPDataTable("ObtenerFacturas");

            if (dataTableFacturas is null) return null;

            var listaFacturas = new List<Factura>();

            foreach (DataRow filaFacturas in dataTableFacturas.Rows)
            {
                var factura = new Factura();
                factura.IdFactura = (int)filaFacturas["idFactura"];
                factura.Fecha = (DateTime)filaFacturas["fecha"];
                factura.IdFormaPago = (int)filaFacturas["idFormaPago"];

                var parametros = new List<Parametro>() { new Parametro("@idFactura", factura.IdFactura) };

                var dataTableDetalles = DatosHelper.ObtenerInstancia().SPDataTable("ObtenerDetalles", parametros);

                foreach (DataRow filaDetalles in dataTableDetalles.Rows)
                {
                    var detalle = new DetalleFactura();

                    detalle.IdDetalle = (int)filaDetalles["idDetalle"];
                    detalle.IdFactura = factura.IdFactura;
                    detalle.IdArticulo = (int)filaDetalles["idArticulo"];
                    detalle.Cantidad = (int)filaDetalles["cantidad"];

                    factura.DetallesFactura.Add(detalle);
                }

                listaFacturas.Add(factura);
            }

            return listaFacturas;
        }

        public Factura? ObtenerPorId(int idFactura)
        {
            var parametrosFactura= new List<Parametro>() { new Parametro("@idFactura", idFactura) };

            var dataTableFacturas = DatosHelper.ObtenerInstancia().SPDataTable("ObtenerFacturaPorId", parametrosFactura);

            if (dataTableFacturas is null) return null;


            var factura = new Factura();
            
            foreach (DataRow filaFactura in dataTableFacturas.Rows)
            {
                factura.IdFactura = (int)filaFactura["idFactura"];
                factura.Fecha = (DateTime)filaFactura["fecha"];
                factura.IdFormaPago = (int)filaFactura["idFormaPago"];

                var parametrosDetalle = new List<Parametro>() { new Parametro("@idFactura", factura.IdFactura) };

                var dataTableDetalles = DatosHelper.ObtenerInstancia().SPDataTable("ObtenerDetalles", parametrosDetalle);

                if (dataTableDetalles is null) return null;

                foreach (DataRow filaDetalles in dataTableDetalles.Rows)
                {
                    var detalle = new DetalleFactura();

                    detalle.IdDetalle = (int)filaDetalles["idDetalle"];
                    detalle.IdFactura = factura.IdFactura;
                    detalle.IdArticulo = (int)filaDetalles["idArticulo"];
                    detalle.Cantidad = (int)filaDetalles["cantidad"];

                    factura.DetallesFactura.Add(detalle);
                }
            }

            return factura;
        }

        public bool? Crear(Factura factura)
        {
            SqlConnection conexion = DatosHelper.ObtenerInstancia().ObtenerConexion();
            conexion.Open();
            SqlTransaction transaccion = transaccion = conexion.BeginTransaction();

            try
            {
                conexion = DatosHelper.ObtenerInstancia().ObtenerConexion();

                var cmdFactura = new SqlCommand("CrearFactura", conexion, transaccion);
                cmdFactura.CommandType = CommandType.StoredProcedure;

                var parametrosFactura = new List<Parametro>()
                {
                    new Parametro("@fecha", factura.Fecha),
                    new Parametro("@idFormaPago", factura.IdFormaPago),
                };

                var salidaComando = new SqlParameter("@idFactura", SqlDbType.Int);
                salidaComando.Direction = ParameterDirection.Output;

                cmdFactura.Parameters.Add(salidaComando);
                foreach (Parametro parametro in parametrosFactura)
                {
                    cmdFactura.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                }

                var resultadoFactura = cmdFactura.ExecuteNonQuery();

                foreach (DetalleFactura detalle in factura.DetallesFactura)
                {
                    var parametrosDetalle = new List<Parametro>()
                    {
                        new Parametro("@idFactura", resultadoFactura),
                        new Parametro("@idArticulo", detalle.IdArticulo),
                        new Parametro("@cantidad", detalle.Cantidad)
                    };

                    var cmdDetalle = new SqlCommand("CrearDetalle", conexion, transaccion);

                    foreach(Parametro parametro in parametrosDetalle)
                    {
                        cmdDetalle.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                    }

                    cmdDetalle.ExecuteNonQuery();
                }

                transaccion.Commit();
                conexion.Close();

                return resultadoFactura == 1;
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
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

        public bool? Modificar(Factura factura)
        {
            var conexion = DatosHelper.ObtenerInstancia().ObtenerConexion();
            conexion.Open();
            var transaccion = conexion.BeginTransaction();

            try
            {
                transaccion = conexion.BeginTransaction();

                var cmdFactura = new SqlCommand("ModificarFactura", conexion, transaccion);
                cmdFactura.CommandType = CommandType.StoredProcedure;

                var parametrosFactura = new List<Parametro>()
                {
                    new Parametro("@idFactura", factura.IdFactura),
                    new Parametro("@fecha", factura.Fecha),
                    new Parametro("@idFormaPago", factura.IdFormaPago),
                };

                foreach (Parametro parametro in parametrosFactura)
                {
                    cmdFactura.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                }

                var resultadoFactura = cmdFactura.ExecuteNonQuery();

                foreach (DetalleFactura detalle in factura.DetallesFactura)
                {
                    var parametrosDetalle = new List<Parametro>()
                    {
                        new Parametro("@idFactura", factura.IdFactura),
                        new Parametro("@idArticulo", detalle.IdArticulo),
                        new Parametro("@cantidad", detalle.Cantidad)
                    };

                    var cmdDetalle = new SqlCommand("CrearDetalle", conexion, transaccion);

                    foreach (Parametro parametro in parametrosDetalle)
                    {
                        cmdDetalle.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                    }

                    cmdDetalle.ExecuteNonQuery();
                }

                transaccion.Commit();
                conexion.Close();

                return resultadoFactura == 1;
            }
            catch
            {
                transaccion.Rollback();
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

        public bool? Eliminar(int idFactura)
        {
            var parametros = new List<Parametro>() { new Parametro("@idFactura", idFactura) };

            return DatosHelper.ObtenerInstancia().SPNonQuery("EliminarFactura", parametros); ;
        }
    }
}
