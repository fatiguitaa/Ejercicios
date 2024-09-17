using ProduccionBack.Contratos;
using ProduccionBack.Datos;
using ProduccionBack.Modelos;

namespace ProduccionBack.Servicios
{
    public class ServicioArticulos : IArticulosRepository
    {
        private ArticulosRepository articulosRepository = new ArticulosRepository();

        public List<Articulo>? ObtenerTodos() { return articulosRepository.ObtenerTodos(); }

        public Articulo? ObtenerPorId(int idArticulo) { return articulosRepository.ObtenerPorId(idArticulo); }

        public bool? Crear(Articulo articulo) { return articulosRepository.Crear(articulo); }

        public bool? Modificar(Articulo articulo) { return articulosRepository.Modificar(articulo); }

        public bool? Eliminar(int idArticulo) { return articulosRepository.Eliminar(idArticulo); }
    }
}
