using ProduccionBack.Contratos;
using ProduccionBack.Datos;
using ProduccionBack.Modelos;

namespace ProduccionBack.Servicios
{
    public class ServicioArticulos : IArticulosRepository
    {

        public List<Articulo>? ObtenerTodos() { return ArticulosRepository.ObtenerInstancia().ObtenerTodos(); }

        public Articulo? ObtenerPorId(int idArticulo) { return ArticulosRepository.ObtenerInstancia().ObtenerPorId(idArticulo); }

        public bool? Crear(Articulo articulo) { return ArticulosRepository.ObtenerInstancia().Crear(articulo); }

        public bool? Modificar(Articulo articulo) { return ArticulosRepository.ObtenerInstancia().Modificar(articulo); }

        public bool? Eliminar(int idArticulo) { return ArticulosRepository.ObtenerInstancia().Eliminar(idArticulo); }
    }
}
