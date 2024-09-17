using ProduccionBack.Contratos;
using ProduccionBack.Datos.Utilidades;
using ProduccionBack.Modelos;
using System.Data;

namespace ProduccionBack.Datos
{
    public class ArticulosRepository : IArticulosRepository
    {
        private static ArticulosRepository instancia = new ArticulosRepository();

        private ArticulosRepository() { }

        public static ArticulosRepository ObtenerInstancia() { return instancia; }
        
        public List<Articulo>? ObtenerTodos()
        {
            DataTable? dataTable = DatosHelper.ObtenerInstancia().SPDataTable("ObtenerArticulos");

            if (dataTable is null) return null;

            List<Articulo> listaArticulos = new List<Articulo>();
            foreach (DataRow fila in dataTable.Rows)
            {
                Articulo articulo = new Articulo();
                articulo.IdArticulo = (int)fila["idArticulo"];
                articulo.Nombre = (string)fila["nombre"];
                articulo.PrecioUnitario = (decimal)fila["precioUnitario"];

                listaArticulos.Add(articulo);
            }

            return listaArticulos;
        }

        public Articulo? ObtenerPorId(int idArticulo)
        {
            var parametros = new List<Parametro>() { new Parametro("@idArticulo", idArticulo) };

            DataTable? dataTable = DatosHelper.ObtenerInstancia().SPDataTable("ObtenerArticuloPorId", parametros);

            if (dataTable is null) return null;
            
            Articulo articulo = new Articulo();
            foreach (DataRow fila in dataTable.Rows)
            {
                articulo.IdArticulo = (int)fila["idArticulo"];
                articulo.Nombre = (string)fila["nombre"];
                articulo.PrecioUnitario = (decimal)fila["precioUnitario"];
            }

            return articulo;
        }

        public bool? Crear(Articulo articulo) { 
       
            var parametros = new List<Parametro>() { 
                new Parametro("@nombre", articulo.Nombre),
                new Parametro("precioUnitario", articulo.PrecioUnitario)
            };

            return DatosHelper.ObtenerInstancia().SPNonQuery("CrearArticulo", parametros);
        }

        public bool? Modificar(Articulo articulo)
        {
            var parametros = new List<Parametro>() {
                new Parametro("@idArticulo", articulo.IdArticulo),
                new Parametro("@nombre", articulo.Nombre),
                new Parametro("precioUnitario", articulo.PrecioUnitario)
            };

            return DatosHelper.ObtenerInstancia().SPNonQuery("ModificarArticulo", parametros);
        }

        public bool? Eliminar(int idArticulo)
        {
            var parametros = new List<Parametro>() { new Parametro("@idArticulo", idArticulo) };

            return DatosHelper.ObtenerInstancia().SPNonQuery("EliminarArticulo", parametros);
        }
    }
}
