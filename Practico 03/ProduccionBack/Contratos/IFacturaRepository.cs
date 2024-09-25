using ProduccionBack.Modelos;

namespace ProduccionBack.Contratos
{
    public interface IFacturaRepository
    {
        public List<Factura>? ObtenerTodas();

        public Factura? ObtenerPorId(int idFactura);

        public bool? Crear(Factura factura);

        public bool? Modificar(Factura factura);

        public bool? Eliminar(int idFactura);
    }
}
