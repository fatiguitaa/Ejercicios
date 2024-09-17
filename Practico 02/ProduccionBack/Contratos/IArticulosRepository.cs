using ProduccionBack.Modelos;

namespace ProduccionBack.Contratos
{
    public interface IArticulosRepository
    {
        public List<Articulo>? ObtenerTodos();

        public Articulo? ObtenerPorId(int idArticulo);
        
        public bool? Crear(Articulo articulo);

        public bool? Modificar(Articulo articulo);

        public bool? Eliminar(int idArticulo);
    }
}
